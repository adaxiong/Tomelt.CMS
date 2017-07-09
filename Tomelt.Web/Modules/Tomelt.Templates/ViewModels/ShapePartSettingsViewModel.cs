using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tomelt.Templates.Services;

namespace Tomelt.Templates.ViewModels {
    public class ShapePartSettingsViewModel {

        [UIHint("TemplateProcessorPicker")]
        public string Processor { get; set; }
        public IList<ITemplateProcessor> AvailableProcessors { get; set; }
    }
}