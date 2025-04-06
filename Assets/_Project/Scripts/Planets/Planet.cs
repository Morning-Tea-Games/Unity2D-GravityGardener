using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Planets
{
    public class Planet : MonoBehaviour
    {
        [field: SerializeField] public CircleCollider2D Collider { get; private set; }
        [field: SerializeField] public List<LayerView> Layers { get; private set; }

        [SerializeField] private List<PlanetLayerConflictSO> _conflicts;

        public void Add(List<LayerView> layers, float sizeRatio)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].CurrentIntensity == LayerIntensity.None)
                {
                    continue;
                }

                if (Layers[i] == layers[i] && Layers[i].CurrentIntensity <= LayerIntensity.High)
                {
                    Layers[i].Activate(layers[i].CurrentIntensity + 1);
                }
                else
                {
                    Layers[i].Activate(layers[i].CurrentIntensity);
                }

                FixConflict();
            }
        }

        public void FixConflict()
        {
            foreach (var conflict in _conflicts)
            {
                var aView = Layers.FirstOrDefault(l => l.Layer == conflict.A);
                var bView = Layers.FirstOrDefault(l => l.Layer == conflict.B);

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