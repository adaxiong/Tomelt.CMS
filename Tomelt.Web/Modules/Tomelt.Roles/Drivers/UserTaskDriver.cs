using System.Collections.Generic;
using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.Data;
using Tomelt.Environment.Extensions;
using Tomelt.Forms.Services;
using Tomelt.Localization;
using Tomelt.Mvc;
using Tomelt.Roles.Activities;
using Tomelt.Security;
using Tomelt.UI.Notify;
using Tomelt.Workflows.Models;
using Tomelt.Workflows.Services;

namespace Tomelt.Roles.Drivers {
    [TomeltFeature("Tomelt.Roles.Workflows")]
    public class UserTaskDriver : ContentPartDriver<ContentPart> {
        private readonly IWorkflowManager _workflowManager;
        private readonly IRepository<AwaitingActivityRecord> _awaitingActivityRepository;
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserTaskDriver(
            ITomeltServices services,
            IWorkflowManager workflowManager,
            IRepository<AwaitingActivityRecord> awaitingActivityRepository,
            IWorkContextAccessor workContextAccessor,
            IHttpContextAccessor httpContextAccessor
            ) {
            _workflowManager = workflowManager;
            _awaitingActivityRepository = awaitingActivityRepository;
            _workContextAccessor = workContextAccessor;
            _httpContextAccessor = httpContextAccessor;
            T = NullLocalizer.Instance;
            Services = services;
        }

        public Localizer T { get; set; }
        public ITomeltServices Services { get; set; }

        protected override string Prefix {
            get { return "UserTaskDriver"; }
        }

        protected override DriverResult Editor(ContentPart part, dynamic shapeHelper) {
            var results = new List<DriverResult> {
                ContentShape("UserTask_ActionButton", () => {
                    if (part.ContentItem.Record == null) {
                        return null;
                    }

                    var workContext = _workContextAccessor.GetContext();
                    var user = workContext.CurrentUser;

                    var awaiting = _awaitingActivityRepository.Table.Where(x => x.WorkflowRecord.ContentItemRecord == part.ContentItem.Record && x.ActivityRecord.Name == "UserTask").ToList();
                    var actions = awaiting.Where(x => UserIsInRole(x, user)).SelectMany(ListAction).ToList();

                    return shapeHelper.UserTask_ActionButton().Actions(actions);
                })
            };

            return Combined(results.ToArray());
        }

        // returns all the actions associated with a specific state
        private static IEnumerable<string> ListAction(AwaitingActivityRecord x) {
            var state = FormParametersHelper.FromJsonString(x.ActivityRecord.State);
            string actionState = state.Actions ?? "";
            return actionState.Split(',').Select(action => action.Trim());
        }

        // whether a user is in an accepted role for this state
        private static bool UserIsInRole(AwaitingActivityRecord x, IUser user) {
            var state = FormParametersHelper.FromJsonString(x.ActivityRecord.State);
            string rolesState = state.Roles ?? "";

            // "Any" if string is empty
            if (string.IsNullOrWhiteSpace(rolesState)) {
                return true;
            }
            var roles = rolesState.Split(',').Select(role => role.Trim());
            return UserTaskActivity.UserIsInRole(user, roles);
        }

        protected override DriverResult Editor(ContentPart part, IUpdateModel updater, dynamic shapeHelper) {
            var httpContext = _httpContextAccessor.Current();
            var name = httpContext.Request.Form["submit.Save"];
            if (!string.IsNullOrEmpty(name) && name.StartsWith("usertask-")) {
                name = name.Substring("usertask-".Length);

                var user = Services.WorkContext.CurrentUser;

                var awaiting = _awaitingActivityRepository.Table.Where(x => x.WorkflowRecord.ContentItemRecord == part.ContentItem.Record && x.ActivityRecord.Name == "UserTask").ToList();
                var actions = awaiting.Where(x => UserIsInRole(x, user)).SelectMany(ListAction).ToList();

                if (!actions.Contains(name)) {
                    Services.Notifier.Error(T("Not authorized to trigger {0}.", name));
                }
                else {
                    _workflowManager.TriggerEvent("UserTask", part, () => new Dictionary<string, object> { { "Content", part.ContentItem}, { "UserTask.Action", name } });
                }
            }

            return Editor(part, shapeHelper);
        }
    }
}