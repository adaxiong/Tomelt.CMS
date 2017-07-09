using System;
using System.Collections.Generic;
using Tomelt.Workflows.Models;
using Tomelt.Workflows.Services;

namespace Tomelt.Workflows.ViewModels {
    public class AdminEditViewModel {
        public string LocalId { get; set; }
        public bool IsLocal { get; set; }
        public IEnumerable<IActivity> AllActivities { get; set; }
        public WorkflowDefinitionViewModel WorkflowDefinition { get; set; }
        public WorkflowRecord Workflow { get; set; } 
    }

    [Serializable]
    public class UpdatedActivityModel {
        public string ClientId { get; set; }
        public string Data { get; set; }
    }
}