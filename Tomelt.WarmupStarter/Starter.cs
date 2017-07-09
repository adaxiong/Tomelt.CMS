using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Tomelt.WarmupStarter
{
    public class Starter<T> where T : class
    {
        private readonly Func<HttpApplication, T> _initialization;
        private readonly Action<HttpApplication, T> _beginRequest;
        private readonly Action<HttpApplication, T> _endRequest;
        private readonly object _synLock = new object();


        /// <summary>
        /// 初始化队列工作项的结果
        /// 初始化完成时无错误设置。
        /// </summary>
        private volatile T _initializationResult;
        /// <summary>
        /// 由初始化线程引发的（潜在）错误。这是一个“一次性”错误信号，这样我们可以在另一个请求到来时重新启动初始化。
        /// </summary>
        private volatile Exception _error;
        /// <summary>
        /// The (potential) error from the previous initiazalition. We need to
        /// 保持此错误活动，直到下一个初始化完成，
        /// 这样我们就可以继续报告所有传入请求的错误。
        /// </summary>
        private volatile Exception _previousError;

        public Starter(Func<HttpApplication, T> initialization, Action<HttpApplication, T> beginRequest, Action<HttpApplication, T> endRequest)
        {
            _initialization = initialization;
            _beginRequest = beginRequest;
            _endRequest = endRequest;
        }

        public void OnApplicationStart(HttpApplication application)
        {
            LaunchStartupThread(application);
        }

        public void OnBeginRequest(HttpApplication application)
        {
            // Initialization resulted in an error
            if (_error != null)
            {
                // Save error for next requests and restart async initialization.
                // Note: The reason we have to retry the initialization is that the 
                //       application environment may change between requests,
                //       e.g. App_Data is made read-write for the AppPool.
                bool restartInitialization = false;

                lock (_synLock)
                {
                    if (_error != null)
                    {
                        _previousError = _error;
                        _error = null;
                        restartInitialization = true;
                    }
                }

                if (restartInitialization)
                {
                    LaunchStartupThread(application);
                }
            }

            // Previous initialization resulted in an error (and another initialization is running)
            if (_previousError != null)
            {
                throw new ApplicationException("Error during application initialization", _previousError);
            }

            // Only notify if the initialization has successfully completed
            if (_initializationResult != null)
            {
                _beginRequest(application, _initializationResult);
            }
        }

        public void OnEndRequest(HttpApplication application)
        {
            // Only notify if the initialization has successfully completed
            if (_initializationResult != null)
            {
                _endRequest(application, _initializationResult);
            }
        }

        /// <summary>
        /// Run the initialization delegate asynchronously in a queued work item
        /// </summary>
        public void LaunchStartupThread(HttpApplication application)
        {
            // Make sure incoming requests are queued
            WarmupHttpModule.SignalWarmupStart();

            ThreadPool.QueueUserWorkItem(
                state => {
                    try
                    {
                        var result = _initialization(application);
                        _initializationResult = result;
                    }
                    catch (Exception ex)
                    {
                        lock (_synLock)
                        {
                            _error = ex;
                            _previousError = null;
                        }
                    }
                    finally
                    {
                        // Execute pending requests as the initialization is over
                        WarmupHttpModule.SignalWarmupDone();
                    }
                });
        }
    }
}
