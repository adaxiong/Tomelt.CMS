using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Data;
using Tomelt.Workflows.Models;

namespace Tomelt.Workflows.Handlers {

    public class WorkflowHandler : ContentHandler {

        public WorkflowHandler(
            IRepository<WorkflowRecord> workflowRepository
            ) {

            // Delete any pending workflow related to a deleted content item
            OnRemoving<ContentPart>(
                (context, part) => {
                    var workflows = workflowRepository.Table.Where(x => x.ContentItemRecord == context.ContentItemRecord).ToList();

                    foreach (var item in workflows) {
                        workflowRepository.Delete(item);
                    }
                });
        }
    }
}