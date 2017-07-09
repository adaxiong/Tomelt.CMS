using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.DisplayManagement.Implementation;
using Tomelt.Templates.Models;

namespace Tomelt.Templates.Services {
    public interface ITemplateService : IDependency {
        string Execute<TModel>(string template, string name, string processorName, TModel model = default(TModel));
        string Execute<TModel>(string template, string name, string processorName, DisplayContext context, TModel model = default(TModel));
    }
}