using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Environment.Extensions;
using Tomelt.Localization;
using Tomelt.Users.Events;
using Tomelt.Users.Models;
using Tomelt.Workflows.Models;
using Tomelt.Workflows.Services;

namespace Tomelt.Users.Activities {
    [TomeltFeature("Tomelt.Users.Workflows")]
    public class ApproveUserActivity : Task {
        private readonly IUserEventHandler _userEventHandlers;

        public ApproveUserActivity(IUserEventHandler userEventHandlers) {
            _userEventHandlers = userEventHandlers;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public override string Name {
            get { return "ApproveUser"; }
        }

        public override LocalizedString Category {
            get { return T("User"); }
        }

        public override LocalizedString Description {
            get { return T("If the content item is a user, that user will be approved."); }
        }

        public override bool CanExecute(WorkflowContext workflowContext, ActivityContext activityContext) {
            return workflowContext.Content != null && workflowContext.Content.Is<UserPart>();
        }

        public override IEnumerable<LocalizedString> GetPossibleOutcomes(WorkflowContext workflowContext, ActivityContext activityContext) {
            return new[] { T("Done") };
        }

        public override IEnumerable<LocalizedString> Execute(WorkflowContext workflowContext, ActivityContext activityContext) {
            var user = workflowContext.Content.As<UserPart>();

            user.RegistrationStatus = UserStatus.Approved;
            user.EmailStatus = UserStatus.Approved;
            _userEventHandlers.Approved(user);

            yield return T("Done");
        }
    }
}