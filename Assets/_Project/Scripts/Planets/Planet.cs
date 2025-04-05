using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Planets
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private List<LayerView> _layers;
        [SerializeField] private List<PlanetLayerConflictSO> _conflicts;

        public void FixConflict()
        {
            foreach (var conflict in _conflicts)
            {
                var aView = _layers.FirstOrDefault(l => l.Layer == conflict.A);
                var bView = _layers.FirstOrDefault(l => l.Layer == conflict.B);

                if (aView == null || bView == null) continue;

                var aIntensity = (int)aView.CurrentIntensity;
                var bIntensity = (int)bView.CurrentIntensity;

                if (aIntensity == 0 || bIntensity == 0) continue;

                if (aIntensity == bIntensity)
                {
                    aView.Activate(LayerIntensity.None);
                    bView.Activate(LayerIntensity.None);
                }
                else if (aIntensity > bIntensity)
                {
                    aView.Activate((LayerIntensity)(aIntensity - bIntensity));
                    bView.Activate(LayerIntensity.None);
                }
                else
                {
                    bView.Activate((LayerIntensity)(bIntensity - aIntensity));
                    aView.Activate(LayerIntensity.None);
                }
            }
        }
    }
}