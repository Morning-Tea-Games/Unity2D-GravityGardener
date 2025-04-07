using System.Collections.Generic;
using System.Linq;
using Core;
using VContainer;

namespace Planets
{
    public class PlanetLayerConflictResolver
    {
        private readonly GameRules _rules;

        [Inject]
        public PlanetLayerConflictResolver(GameRules rules)
        {
            _rules = rules;
        }

        public void Resolve(IReadOnlyList<PlanetLayerBehaviour> layers)
        {
            for (int i = 0; i < _rules.PlanetLayerConflicts.Length; i++)
            {
                var conflictA = layers.FirstOrDefault(l => l.Layer == _rules.PlanetLayerConflicts[i].A);
                var conflictB = layers.FirstOrDefault(l => l.Layer == _rules.PlanetLayerConflicts[i].B);

                if (conflictA == null || conflictB == null)
                {
                    continue;
                }

                int intensityA = (int)conflictA.CurrentIntensity;
                int intensityB = (int)conflictB.CurrentIntensity;

                if (intensityA == 0 || intensityB == 0)
                {
                    continue;
                }

                if (intensityA == intensityB)
                {
                    conflictA.Show(PlanetLayerIntensity.None);
                    conflictB.Show(PlanetLayerIntensity.None);
                }
                else if (intensityA > intensityB)
                {
                    conflictA.Show((PlanetLayerIntensity)(intensityA - intensityB));
                    conflictB.Show(PlanetLayerIntensity.None);
                }
                else
                {
                    conflictB.Show((PlanetLayerIntensity)(intensityB - intensityA));
                    conflictA.Show(PlanetLayerIntensity.None);
                }
            }
        }
    }
}