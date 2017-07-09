using System.Data;
using System.Web.Mvc;
using NHibernate;
using Tomelt.Mvc.Filters;

namespace Tomelt.Data {
    public interface ITransactionManager : IDependency {
        void Demand();
        void RequireNew();
        void RequireNew(IsolationLevel level);
        void Cancel();

        ISession GetSession();
    }

    public class TransactionFilter : FilterProvider, IExceptionFilter {
        private readonly ITransactionManager _transactionManager;

        public TransactionFilter(ITransactionManager transactionManager) {
            _transactionManager = transactionManager;
        }

        public void OnException(ExceptionContext filterContext) {
            _transactionManager.Cancel();
        }
    }
}
