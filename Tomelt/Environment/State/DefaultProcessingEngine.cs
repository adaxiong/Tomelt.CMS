﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.Data;
using Tomelt.Environment.Configuration;
using Tomelt.Environment.ShellBuilders;
using Tomelt.Environment.Descriptor.Models;
using Tomelt.Events;
using Tomelt.Logging;

namespace Tomelt.Environment.State {
    public class DefaultProcessingEngine : Component, IProcessingEngine {
        private readonly IShellContextFactory _shellContextFactory;
        private readonly Func<ITomeltHost> _tomeltHost;

        private readonly ContextState<IList<Entry>> _entries;

        public DefaultProcessingEngine(IShellContextFactory shellContextFactory, Func<ITomeltHost> tomeltHost) {
            _shellContextFactory = shellContextFactory;
            _tomeltHost = tomeltHost;

            _entries = new ContextState<IList<Entry>>("DefaultProcessingEngine.Entries", () => new List<Entry>());
        }

        public string AddTask(ShellSettings shellSettings, ShellDescriptor shellDescriptor, string eventName, Dictionary<string, object> parameters) {

            var entry = new Entry {
                ShellSettings = shellSettings,
                ShellDescriptor = shellDescriptor,
                MessageName = eventName,
                EventData = parameters,
                TaskId = Guid.NewGuid().ToString("n"),
                ProcessId = Guid.NewGuid().ToString("n"),
            };
            Logger.Information("Adding event {0} to process {1} for shell {2}",
                eventName,
                entry.ProcessId,
                shellSettings.Name);
            _entries.GetState().Add(entry);
            return entry.ProcessId;
        }


        public class Entry {
            public string ProcessId { get; set; }
            public string TaskId { get; set; }

            public ShellSettings ShellSettings { get; set; }
            public ShellDescriptor ShellDescriptor { get; set; }
            public string MessageName { get; set; }
            public Dictionary<string, object> EventData { get; set; }
        }


        public bool AreTasksPending() {
            return _entries.GetState().Any();
        }

        public void ExecuteNextTask() {

            Entry entry;
            if (!_entries.GetState().Any())
                return;
            entry = _entries.GetState().First();
            _entries.GetState().Remove(entry);
            Execute(entry);
        }

        private void Execute(Entry entry) {
            // Force reloading extensions if there were extensions installed
    
            if (entry.MessageName == "IRecipeSchedulerEventHandler.ExecuteWork") {
                var ctx = _tomeltHost().GetShellContext(entry.ShellSettings);
            }

            var shellContext = _shellContextFactory.CreateDescribedContext(entry.ShellSettings, entry.ShellDescriptor);
            using (shellContext.LifetimeScope) {
                using (var standaloneEnvironment = shellContext.LifetimeScope.CreateWorkContextScope()) {

                    ITransactionManager transactionManager;
                    if (!standaloneEnvironment.TryResolve(out transactionManager))
                        transactionManager = null;

                    try {
                        var eventBus = standaloneEnvironment.Resolve<IEventBus>();
                        Logger.Information("Executing event {0} in process {1} for shell {2}",
                                           entry.MessageName,
                                           entry.ProcessId,
                                           entry.ShellSettings.Name);
                        eventBus.Notify(entry.MessageName, entry.EventData);
                    }
                    catch {
                        // any database changes in this using(env) scope are invalidated
                        if (transactionManager != null)
                            transactionManager.Cancel();
                        throw;
                    }
                }
            }
        }
    }
}
