using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Planets
{
    public class PlanetContextMenuInvokerBehaviour : MonoBehaviour
    {
        [SerializeField] private PlanetBehaviour _planet;

        private PlanetContextMenuBehaviour _contextMenu;

        [Inject]
        public void Construct(PlanetContextMenuBehaviour contextMenu)
        {
            _contextMenu = contextMenu;
        }

        private void Awake()
        {
            var container = LifetimeScope.Find<PlanetLifetimeScope>().Container;
            container.Inject(this);
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }  

            HandleLeftClick();
            HandleRightClick();
        }

        private void HandleRightClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                PlanetBehaviour clickedPlanet = GetClickedPlanet();

                if (clickedPlanet == _planet)
                {
                    _contextMenu.Open(_planet);
                }
                else if (clickedPlanet == null)
                {
                    _contextMenu.Close();
                }
            }
        }

        private void HandleLeftClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var clickedPlanet = GetClickedPlanet();

                if (clickedPlanet == null || clickedPlanet == _contextMenu.Current)
                {
                    _contextMenu.Close();
                }
                else if (_contextMenu.IsAwaitingSecondPlanet)
                {
                    _contextMenu.TryMergeWith(clickedPlanet);
                }
            }
        }

        private PlanetBehaviour GetClickedPlanet()
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(position, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out PlanetBehaviour planet))
            {
                return planet;
            }

            return null;
        }
    }
}