// 4. Сделать через выбор первой планеты ко второй
// 1. Подогнать размер планет под референс

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
            for(int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].CurrentIntensity == LayerIntensity.None && layers[i].CurrentIntensity == LayerIntensity.None) continue;
                Layers[i].Activate(Layers[i].CurrentIntensity + (int)layers[i].CurrentIntensity);
            }

            transform.localScale += sizeRatio;
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

        private void DefineType()
        {
            var size = transform.localScale.y;

            for (int i = 0; i < _maxSizes.Configuration.Length; i++)
            {
                if (size > _maxSizes.Configuration[i].MaxSize)
                {
                    continue;
                }

                _currentType = _maxSizes.Configuration[i].Type;
                return;
            }

            throw new System.Exception($"There was no planet with the right size");
        }
    }
}