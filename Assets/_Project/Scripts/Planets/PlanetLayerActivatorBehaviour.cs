using UnityEngine;
using UnityEngine.UI;

namespace Planets
{
    public class PlanetLayerActivatorBehaviour : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private PlanetLayerBehaviour _target;
        [SerializeField] private PlanetLayerIntensity _intensity;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _target.Show(_intensity);
        }
    }
}