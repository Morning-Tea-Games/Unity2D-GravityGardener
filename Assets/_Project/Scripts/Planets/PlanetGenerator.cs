using Core;
using UnityEngine;
using VContainer;

namespace Planets
{
    public class PlanetGenerator
    {
        private readonly PlanetLayerConflictResolver _conflictResolver;
        private readonly GameRules _rules;

        [Inject]
        public PlanetGenerator(PlanetLayerConflictResolver conflictResolver, GameRules rules)
        {
            _conflictResolver = conflictResolver;
            _rules = rules;
        }

        public PlanetBehaviour Generate()
        {
            var planet = Object.Instantiate(_rules.PlanetPrefab);

            for (int i = 0; i < _rules.EnabledLayers.Length; i++)
            {
                planet.Layers[i].Show((PlanetLayerIntensity)Random.Range(
                    (int)_rules.MinPlanetLayerIntensity,
                    (int)_rules.MaxPlanetLayerIntensity
                    ));
            }

            _conflictResolver.Resolve(planet.Layers);

            float size = Random.Range(_rules.MinGeneratedPlanetSize, _rules.MaxGeneratedSize);
            planet.transform.localScale = Vector3.one * size;

            try
            {
                return planet;
            }
            finally
            {
                Object.Destroy(planet.gameObject);
            }
        }
    }
}