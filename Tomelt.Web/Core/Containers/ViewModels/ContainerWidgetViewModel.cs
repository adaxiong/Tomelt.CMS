using System.Web.Mvc;
using Tomelt.Core.Containers.Models;

namespace Tomelt.Core.Containers.ViewModels {
    public class ContainerWidgetViewModel {
        public bool UseFilter { get; set; }
        public SelectList AvailableContainers { get; set; }
        public ContainerWidgetPart Part { get; set; }
    }
}