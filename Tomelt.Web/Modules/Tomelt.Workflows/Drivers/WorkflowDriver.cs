using System.Linq;
using Tomelt.ContentManagement.Drivers;
using Tomelt.Core.Common.Models;
using Tomelt.Data;
using Tomelt.Localization;
using Tomelt.Workflows.Models;

namespace Tomelt.Workflows.Drivers {
    public class WorkflowDriver : ContentPartDriver<CommonPart> {
        private readonly IRepository<WorkflowRecord> _workflowRepository;

        public WorkflowDriver(
            ITomeltServices services,
            IRepository<WorkflowRecord> workflowRepository
            ) {
                _workflowRepository = workflowRepository;
            T = NullLocalizer.Instance;
            Services = services;
        }

        public Localizer T { get; set; }
        public ITomeltServices Services { get; set; }

        protected override string Prefix {
            get { return "WorkflowDriver"; }
        }

        protected override DriverResult Display(CommonPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_Workflow_SummaryAdmin", () => {
                var workflows = _workflowRepository.Table.Where(x => x.ContentItemRecord == part.ContentItem.Record).ToList();
                return shapeHelper.Parts_Workflow_SummaryAdmin().Workflows(workflows);
            });
        }
    }
}