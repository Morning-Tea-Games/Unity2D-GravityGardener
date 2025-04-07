using Core;
using UnityEngine;
using VContainer;

namespace Planets
{
    public class PlanetMerger
    {
        private readonly PlanetLayerConflictResolver _conflictResolver;
        private readonly GameRules _rules;

        [Inject]
        public PlanetMerger(PlanetLayerConflictResolver conflictResolver, GameRules rules)    
        {
            _conflictResolver = conflictResolver;
            _rules = rules;
        }

        public void Merge(PlanetBehaviour a, PlanetBehaviour b)
        {
            if (a.gameObject.layer != b.gameObject.layer)
            {
                return;
            }
            
            for (int i = 0; i < b.Layers.Count; i++)
            {
                var newIntensity = (int)b.Layers[i].CurrentIntensity + (int)a.Layers[i].CurrentIntensity;

                b.Layers[i].Show((PlanetLayerIntensity)Mathf.Clamp(
                    newIntensity,
                    (int)PlanetLayerIntensity.None,
                    (int)PlanetLayerIntensity.High
                    ));
            }

            _conflictResolver.Resolve(b.Layers);

            Vector3 totalScale;

            if (b.transform.localScale.magnitude > a.transform.localScale.magnitude)
            {
                totalScale = b.transform.localScale + (a.transform.localScale * _rules.PlanetSizeProgressionModificator);
            }
            else
            {
                totalScale = (_rules.PlanetSizeProgressionModificator * b.transform.localScale) + a.transform.localScale;
            }

            b.transform.localScale = totalScale;
            b.DefineType();
            Object.Destroy(a.gameObject);
        }
    }
}