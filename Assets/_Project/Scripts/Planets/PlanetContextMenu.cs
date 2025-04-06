using System;
using UnityEngine;
using UnityEngine.UI;

namespace Planets
{
    public class PlanetContextMenu : MonoBehaviour
    {
        public bool WaitingConnect;

        [SerializeField] private Button _informationButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _compositButton;
        [SerializeField] private Button _destroyButton;

        private Planet _target;

        private void OnEnable()
        {
            _informationButton.onClick.AddListener(OnInformationButtonClicked);
            _moveButton.onClick.AddListener(OnMoveButtonClicked);
            _compositButton.onClick.AddListener(OnCompositButtonClicked);
            _destroyButton.onClick.AddListener(OnDestroyButtonClicked);
        }

        private void OnDisable()
        {
            _informationButton.onClick.RemoveListener(OnInformationButtonClicked);
            _moveButton.onClick.RemoveListener(OnMoveButtonClicked);
            _compositButton.onClick.RemoveListener(OnCompositButtonClicked);
            _destroyButton.onClick.RemoveListener(OnDestroyButtonClicked);
        }

        public void SetTarget(Planet target)
        {
            _target = target;
        }

        public void SetActiveCompositButton(bool active)
        {
            _compositButton.interactable = active;
        }
        
        private void OnMoveButtonClicked()
        {
            throw new NotImplementedException();
        }

        private void OnCompositButtonClicked()
        {
            SetActiveCompositButton(false);
            WaitingConnect = true;
            gameObject.SetActive(false);
        }

        private void OnDestroyButtonClicked()
        {
            Destroy(_target.gameObject);
            gameObject.SetActive(false);
        }

        private void OnInformationButtonClicked()
        {
            throw new NotImplementedException();
        }
    }
}