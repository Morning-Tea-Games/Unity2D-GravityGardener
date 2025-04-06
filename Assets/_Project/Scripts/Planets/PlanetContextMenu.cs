using System;
using UnityEngine;
using UnityEngine.UI;

namespace Planets
{
    public class PlanetContextMenu : MonoBehaviour
    {
        [SerializeField] private Button _informationButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _compositButton;
        [SerializeField] private Button _destroyButton;

        [SerializeField] private Planet _planet;
        [SerializeField] private PlanetConnector _connector;

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
        
        private void OnMoveButtonClicked()
        {
            throw new NotImplementedException();
        }

        private void OnCompositButtonClicked()
        {
            _connector.Connect();
        }

        private void OnDestroyButtonClicked()
        {
            Destroy(_planet.gameObject);
        }

        private void OnInformationButtonClicked()
        {
            throw new NotImplementedException();
        }
    }
}