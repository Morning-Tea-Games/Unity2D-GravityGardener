using UnityEngine;

namespace Planets
{
    public class PlanetLayerSetupBehaviour : MonoBehaviour
    {
        [SerializeField] private PlanetBehaviour _target;
        [SerializeField] private int _layerIndex;
        [SerializeField] private int _intensity;

        private void Start()
        {
            _target.Layers[_layerIndex].Show((PlanetLayerIntensity)_intensity);
        }
    }
}