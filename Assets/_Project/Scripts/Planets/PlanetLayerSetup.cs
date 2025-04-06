using UnityEngine;

namespace Planets
{
    public class PlanetLayerSetup : MonoBehaviour
    {
        [SerializeField] private Planet _target;
        [SerializeField] private int _layerIndex;
        [SerializeField] private int _intensity;

        private void Start()
        {
            _target.Layers[_layerIndex].Activate((LayerIntensity)_intensity);
        }
    }
}