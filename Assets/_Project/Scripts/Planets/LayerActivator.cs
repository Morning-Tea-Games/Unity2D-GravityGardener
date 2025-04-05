using UnityEngine;
using UnityEngine.UI;

namespace Planets
{
    public class LayerActivator : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private LayerView _target;
        [SerializeField] private LayerIntensity _intensity;

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
            _target.Activate(_intensity);
        }
    }
}