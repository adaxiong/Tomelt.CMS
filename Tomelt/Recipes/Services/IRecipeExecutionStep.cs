﻿using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Localization;
using Tomelt.Recipes.Models;

namespace Tomelt.Recipes.Services {
    public interface IRecipeExecutionStep : IDependency {
        string Name { get; }
        IEnumerable<string> Names { get; }
        LocalizedString DisplayName { get; }
        LocalizedString Description { get; }
        dynamic BuildEditor(dynamic shapeFactory);
        dynamic UpdateEditor(dynamic shapeFactory, IUpdateModel updater);
        void Configure(RecipeExecutionStepConfigurationContext context);
        void UpdateStep(UpdateRecipeExecutionStepContext context);
        void Execute(RecipeExecutionContext context);
        
    }
}