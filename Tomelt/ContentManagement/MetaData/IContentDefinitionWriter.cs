using System.Xml.Linq;
using Tomelt.ContentManagement.MetaData.Models;

namespace Tomelt.ContentManagement.MetaData {
    public interface IContentDefinitionWriter : IDependency{
        XElement Export(ContentTypeDefinition typeDefinition);
        XElement Export(ContentPartDefinition partDefinition);
    }
}
