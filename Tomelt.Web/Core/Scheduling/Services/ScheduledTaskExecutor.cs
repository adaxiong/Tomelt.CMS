﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.Core.Scheduling.Models;
using Tomelt.Data;
using Tomelt.Logging;
using Tomelt.Services;
using Tomelt.Tasks;
using Tomelt.Tasks.Scheduling;
using Tomelt.Exceptions;

namespace Tomelt.Core.Scheduling.Services {
    public class ScheduledTaskExecutor : IBackgroundTask {
        private readonly IClock _clock;
        private readonly IRepository<ScheduledTaskRecord> _repository;
        private readonly IEnumerable<IScheduledTaskHandler> _handlers;
        private readonly IContentManager _contentManager;
        private readonly ITransactionManager _transactionManager;

        public ScheduledTaskExecutor(
            IClock clock,
            IRepository<ScheduledTaskRecord> repository,
            IEnumerable<IScheduledTaskHandler> handlers,
            IContentManager contentManager,
            ITransactionManager transactionManager) {
            _clock = clock;
            _repository = repository;
            _handlers = handlers;
            _contentManager = contentManager;
            _transactionManager = transactionManager;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Sweep() {
            var taskEntries = _repository.Fetch(x => x.ScheduledUtc <= _clock.UtcNow)
                .Select(x => new { x.Id, Action = x.TaskType })
                .ToArray();

            foreach (var taskEntry in taskEntries) {
                _transactionManager.RequireNew();

                try {
                    // fetch the task
                    var taskRecord = _repository.Get(taskEntry.Id);

                    // another server or thread has performed this work before us
                    if (taskRecord == null) {
                        continue;
                    }

                    // removing record first helps avoid concurrent execution
                    _repository.Delete(taskRecord);

                    var context = new ScheduledTaskContext {
                        Task = new Task(_contentManager, taskRecord)
                    };

                    // dispatch to standard or custom handlers
                    foreach (var handler in _handlers) {
                        handler.Process(context);
                    }
                }
                catch (Exception ex) {
                    if (ex.IsFatal()) {
                        throw;
                    }
                    Logger.Warning(ex, "Unable to process scheduled task #{0} of type {1}", taskEntry.Id, taskEntry.Action);
                    _transactionManager.Cancel();
                }
            }
        }
    }
}
