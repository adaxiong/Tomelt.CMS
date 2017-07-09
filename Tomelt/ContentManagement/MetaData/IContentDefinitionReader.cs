using System.Xml.Linq;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;

namespace Tomelt.ContentManagement.MetaData {
    public interface IContentDefinitionReader : IDependency {
        void Merge(XElement source, ContentTypeDefinitionBuilder builder);
        void Merge(XElement source, ContentPartDefinitionBuilder builder);
    }

    public static class ContentDefinitionReaderExtensions {
        public static ContentTypeDefinition Import(this IContentDefinitionReader reader, XElement source) {
            var target = new ContentTypeDefinitionBuilder();
            reader.Merge(source, target);
            return target.Build();
        }
    }
}