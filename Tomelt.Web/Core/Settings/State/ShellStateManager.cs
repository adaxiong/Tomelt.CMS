using System.Linq;
using Tomelt.Caching;
using Tomelt.Core.Settings.State.Records;
using Tomelt.Data;
using Tomelt.Environment.State;
using Tomelt.Environment.State.Models;
using Tomelt.Environment.Descriptor;
using Tomelt.Logging;

namespace Tomelt.Core.Settings.State {
    public class ShellStateManager : Component, IShellStateManager {
        private readonly IRepository<ShellStateRecord> _shellStateRepository;
        private readonly IShellDescriptorManager _shellDescriptorManager;
        private readonly ICacheManager _cacheManager;

        public ShellStateManager(
            IRepository<ShellStateRecord> shellStateRepository,
            IShellDescriptorManager shellDescriptorManager,
            ICacheManager cacheManager) {
            _shellStateRepository = shellStateRepository;
            _shellDescriptorManager = shellDescriptorManager;
            _cacheManager = cacheManager;
        }

        public ShellState GetShellState() {
            var stateRecord = GetExistingOrNewShellStateRecord();
            var descriptor = _shellDescriptorManager.GetShellDescriptor();
            var extraFeatures = descriptor == null ? Enumerable.Empty<string>() : descriptor.Features
                .Select(r => r.Name)
                .Except(stateRecord.Features.Select(r => r.Name));

            return new ShellState {
                Features = stateRecord.Features
                    .Select(featureStateRecord => new ShellFeatureState {
                        Name = featureStateRecord.Name,
                        EnableState = featureStateRecord.EnableState,
                        InstallState = featureStateRecord.InstallState
                    })
                    .Concat(extraFeatures.Select(name => new ShellFeatureState {
                        Name = name
                    }))
                    .ToArray(),
            };
        }
        private ShellStateRecord GetExistingOrNewShellStateRecord() {
            
            var shellStateRecordId = _cacheManager.Get("ShellStateRecordId", ctx => {
                var shellState = _shellStateRepository.Table.FirstOrDefault();

                if (shellState == null) {
                    shellState = new ShellStateRecord();
                    _shellStateRepository.Create(shellState);
                }
               return shellState.Id;
               });
            
                return _shellStateRepository.Get(shellStateRecordId);
        }

        private ShellFeatureStateRecord FeatureRecord(string name) {
            var stateRecord = GetExistingOrNewShellStateRecord();
            var record = stateRecord.Features.SingleOrDefault(x => x.Name == name);
            if (record == null) {
                record = new ShellFeatureStateRecord { Name = name };
                stateRecord.Features.Add(record);
            }
            if (stateRecord.Id == 0) {
                _shellStateRepository.Create(stateRecord);
            }
            return record;
        }

        public void UpdateEnabledState(ShellFeatureState featureState, ShellFeatureState.State value) {
            Logger.Debug("Feature {0} EnableState changed from {1} to {2}",
                         featureState.Name, featureState.EnableState, value);

            var featureStateRecord = FeatureRecord(featureState.Name);
            if (featureStateRecord.EnableState != featureState.EnableState) {
                Logger.Warning("Feature {0} prior EnableState was {1} when {2} was expected",
                               featureState.Name, featureStateRecord.EnableState, featureState.EnableState);
            }
            featureStateRecord.EnableState = value;
            featureState.EnableState = value;
        }


        public void UpdateInstalledState(ShellFeatureState featureState, ShellFeatureState.State value) {
            Logger.Debug("Feature {0} InstallState changed from {1} to {2}",
                         featureState.Name, featureState.InstallState, value);

            var featureStateRecord = FeatureRecord(featureState.Name);
            if (featureStateRecord.InstallState != featureState.InstallState) {
                Logger.Warning("Feature {0} prior InstallState was {1} when {2} was expected",
                               featureState.Name, featureStateRecord.InstallState, featureState.InstallState);
            }
            featureStateRecord.InstallState = value;
            featureState.InstallState = value;
        }
    }

}