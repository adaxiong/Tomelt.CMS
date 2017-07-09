using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.DisplayManagement.Implementation;
using Tomelt.Templates.Models;

namespace Tomelt.Templates.Services {
    public class DefaultTemplateService : ITemplateService {

        public const string TemplatesSignal = "Tomelt.Templates";

        private readonly IEnumerable<ITemplateProcessor> _processors;

        public DefaultTemplateService(IEnumerable<ITemplateProcessor> processors) {
            _processors = processors;
        }

        public string Execute<TModel>(string template, string name, string processorName, TModel model = default(TModel)) {
            return Execute(template, name, processorName, null, model);
        }

        public string Execute<TModel>(string template, string name, string processorName, DisplayContext context, TModel model = default(TModel)) {
            var processor = _processors.FirstOrDefault(x => String.Equals(x.Type, processorName, StringComparison.OrdinalIgnoreCase)) ?? _processors.First();
            return processor.Process(template, name, context, model);
        }
        
    }
}