using Tomelt.Environment.Extensions;
using Tomelt.Tasks;

namespace Tomelt.Alias.Implementation.Updater {
    [TomeltFeature("Tomelt.Alias.Updater")]
    public class AliasUpdaterBackgroundTask : IBackgroundTask {

        private readonly IAliasHolderUpdater _aliasHolderUpdater;

        public AliasUpdaterBackgroundTask(IAliasHolderUpdater aliasHolderUpdater) {
            _aliasHolderUpdater = aliasHolderUpdater;
        }

        public void Sweep() {
            _aliasHolderUpdater.Refresh();
        }
    }

}