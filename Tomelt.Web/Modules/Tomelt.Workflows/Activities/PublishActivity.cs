using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Localization;
using Tomelt.Workflows.Models;
using Tomelt.Workflows.Services;

namespace Tomelt.Workflows.Activities {
    public class PublishActivity : Task {
        private readonly IContentManager _contentManager;

        public PublishActivity(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        public Localizer T { get; set; }

        public override bool CanExecute(WorkflowContext workflowContext, ActivityContext activityContext) {
            return true;
        }

        public override IEnumerable<LocalizedString> GetPossibleOutcomes(WorkflowContext workflowContext, ActivityContext activityContext) {
            return new[] { T("Published") };
        }

        public override IEnumerable<LocalizedString> Execute(WorkflowContext workflowContext, ActivityContext activityContext) {
            _contentManager.Publish(workflowContext.Content.ContentItem);
            yield return T("Published");
        }

        public override string Name {
            get { return "Publish"; }
        }

        public override LocalizedString Category {
            get { return T("Content Items"); }
        }

        public override LocalizedString Description {
            get { return T("Publish the content item."); }
        }
    }
}