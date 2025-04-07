using System;
using Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Planets
{
    public class PlanetContextMenuBehaviour : MonoBehaviour
    {
        public bool IsAwaitingSecondPlanet => _awaitingSecondPlanet;
        public PlanetBehaviour Current { get; private set; }

        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _buttonHolder;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _compositButton;
        [SerializeField] private Button _destroyButton;

        private PlanetMerger _merger;
        private bool _awaitingSecondPlanet;

        [Inject]
        public void Construct(PlanetMerger merger)
        {
            _merger = merger;
        }

        private void Awake()
        {
            var container = LifetimeScope.Find<PlanetLifetimeScope>().Container;
            container.Inject(this);
        }

        private void OnEnable()
        {
            _moveButton.onClick.AddListener(OnMoveButtonClicked);
            _compositButton.onClick.AddListener(OnCompositButtonClicked);
            _destroyButton.onClick.AddListener(OnDestroyButtonClicked);
        }

        private void OnDisable()
        {
            _moveButton.onClick.RemoveListener(OnMoveButtonClicked);
            _compositButton.onClick.RemoveListener(OnCompositButtonClicked);
            _destroyButton.onClick.RemoveListener(OnDestroyButtonClicked);
        }

        private void Start()
        {
            _canvas.worldCamera = Camera.main;
            Close();
        }

        public void Open(PlanetBehaviour target)
        {
            Current = target;
            transform.position = Current.transform.position;
            _buttonHolder.SetActive(true);
            DisableMergeProcess();
        }

        public void Close()
        {
            Current = null;
            _buttonHolder.SetActive(false);
            DisableMergeProcess();
        }

        public void TryMergeWith(PlanetBehaviour target)
        {
            if (Current != null && target != null)
            {
                _merger.Merge(Current, target);
                Close();
            }
        }
        
        private void OnMoveButtonClicked()
        {
            throw new NotImplementedException();
        }

        private void OnCompositButtonClicked()
        {
            EnableMergeProcess();
        }

        private void OnDestroyButtonClicked()
        {
            Destroy(Current.gameObject);
            Close();
        }

        private void EnableMergeProcess()
        {
            _awaitingSecondPlanet = true;
            _compositButton.interactable = false;
        }

        private void DisableMergeProcess()
        {
            _awaitingSecondPlanet = false;
            _compositButton.interactable = true;
        }
    }
}