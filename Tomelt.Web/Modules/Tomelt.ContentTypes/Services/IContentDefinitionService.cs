﻿using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentTypes.ViewModels;

namespace Tomelt.ContentTypes.Services {
    public interface IContentDefinitionService : IDependency {
        IEnumerable<EditTypeViewModel> GetTypes();
        EditTypeViewModel GetType(string name);
        ContentTypeDefinition AddType(string name, string displayName);
        void AlterType(EditTypeViewModel typeViewModel, IUpdateModel updater);
        void RemoveType(string name, bool deleteContent);
        void AddPartToType(string partName, string typeName);
        void RemovePartFromType(string partName, string typeName);
        string GenerateContentTypeNameFromDisplayName(string displayName);
        string GenerateFieldNameFromDisplayName(string partName, string displayName);

        IEnumerable<EditPartViewModel> GetParts(bool metadataPartsOnly);
        EditPartViewModel GetPart(string name);
        EditPartViewModel AddPart(CreatePartViewModel partViewModel);
        void AlterPart(EditPartViewModel partViewModel, IUpdateModel updater);
        void RemovePart(string name);

        IEnumerable<ContentFieldInfo> GetFields();
        void AddFieldToPart(string fieldName, string fieldTypeName, string partName);
        void AddFieldToPart(string fieldName, string displayName, string fieldTypeName, string partName);
        void RemoveFieldFromPart(string fieldName, string partName);
        void AlterField(EditPartViewModel partViewModel, EditFieldNameViewModel fieldViewModel);
    }
}