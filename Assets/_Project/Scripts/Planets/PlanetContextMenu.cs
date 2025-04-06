using System;
using UnityEngine;
using UnityEngine.UI;

namespace Planets
{
    public class PlanetContextMenu : MonoBehaviour
    {
        public static PlanetContextMenu Instance { get; private set; }
        public bool WaitingConnect;

        [SerializeField] private Button _informationButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _compositButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private Canvas _canvas;

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

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _canvas.worldCamera = Camera.main;
        }

        private void Start()
        {
            gameObject.SetActive(false);
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