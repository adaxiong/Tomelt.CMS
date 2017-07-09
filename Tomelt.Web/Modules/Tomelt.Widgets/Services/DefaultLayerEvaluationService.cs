using System;
using System.Collections.Generic;
using Tomelt.Conditions.Services;
using Tomelt.Localization;
using Tomelt.Logging;
using Tomelt.Widgets.Models;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Utilities;

namespace Tomelt.Widgets.Services{
    public class DefaultLayerEvaluationService : ILayerEvaluationService {
        private readonly IConditionManager _conditionManager;
        private readonly ITomeltServices _tomeltServices;

        private readonly LazyField<int[]> _activeLayerIDs; 

        public DefaultLayerEvaluationService(IConditionManager conditionManager, ITomeltServices tomeltServices) {
            _conditionManager = conditionManager;
            _tomeltServices = tomeltServices;

            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;

            _activeLayerIDs = new LazyField<int[]>();
            _activeLayerIDs.Loader(PopulateActiveLayers);
        }

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }

        /// <summary>
        /// Retrieves every Layer from the Content Manager and evaluates each one.
        /// </summary>
        /// <returns>
        /// A collection of integers that represents the Ids of each active Layer
        /// </returns>
        public int[] GetActiveLayerIds() {
            return _activeLayerIDs.Value;
        }

        private int[] PopulateActiveLayers() {
            // Once the Condition Engine is done:
            // Get Layers and filter by zone and rule
            // NOTE: .ForType("Layer") is faster than .Query<LayerPart, LayerPartRecord>()
            var activeLayers = _tomeltServices.ContentManager.Query<LayerPart>().ForType("Layer").List();

            var activeLayerIds = new List<int>();
            foreach (var activeLayer in activeLayers) {
                // ignore the rule if it fails to execute
                try {
                    if (_conditionManager.Matches(activeLayer.LayerRule)) {
                        activeLayerIds.Add(activeLayer.ContentItem.Id);
                    }
                }
                catch (Exception e) {
                    Logger.Warning(e, T("An error occurred during layer evaluation on: {0}", activeLayer.Name).Text);
                }
            }

            return activeLayerIds.ToArray();
        }
    }
}
