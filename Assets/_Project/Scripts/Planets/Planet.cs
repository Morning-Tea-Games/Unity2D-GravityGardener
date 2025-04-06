using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Planets
{
    public class Planet : MonoBehaviour
    {
        [field: SerializeField] public List<LayerView> Layers { get; private set; }
        [SerializeField] private List<PlanetLayerConflictSO> _conflicts;
        [SerializeField] private PlanetSizeSO _maxSizes;

        private PlanetType _currentType;

        private void Start()
        {
            DefineType();
        }

        public void Add(List<LayerView> layers, Vector3 sizeRatio)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                int a = (int)Layers[i].CurrentIntensity;
                int b = (int)layers[i].CurrentIntensity;
                int sum = Mathf.Clamp(a + b, 0, (int)LayerIntensity.High);
                Layers[i].Activate((LayerIntensity)sum);
            }

            transform.localScale += sizeRatio / transform.localScale.y;
            FixConflict();
            DefineType();
        }

        public void FixConflict()
        {
            foreach (var conflict in _conflicts)
            {
                var aView = Layers.FirstOrDefault(l => l.Layer == conflict.A);
                var bView = Layers.FirstOrDefault(l => l.Layer == conflict.B);

                if (aView == null || bView == null) continue;

                int a = (int)aView.CurrentIntensity;
                int b = (int)bView.CurrentIntensity;

                if (a == 0 || b == 0) continue;

                if (a == b)
                {
                    aView.Activate(LayerIntensity.None);
                    bView.Activate(LayerIntensity.None);
                }
                else if (a > b)
                {
                    aView.Activate((LayerIntensity)(a - b));
                    bView.Activate(LayerIntensity.None);
                }
                else
                {
                    bView.Activate((LayerIntensity)(b - a));
                    aView.Activate(LayerIntensity.None);
                }
            }
        }

        private void DefineType()
        {
            float size = transform.localScale.y;

            for (int i = 0; i < _maxSizes.Configuration.Length; i++)
            {
                if (size <= _maxSizes.Configuration[i].MaxSize)
                {
                    _currentType = _maxSizes.Configuration[i].Type;
                    return;
                }
            }

            throw new System.Exception("No valid planet type");
        }
    }
}