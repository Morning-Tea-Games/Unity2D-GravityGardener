using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    public class PlanetContextMenuInvoker : MonoBehaviour
    {
        [SerializeField] private Planet _main;
        [SerializeField] private PlanetConnector _connector;

        private PlanetContextMenu _menu;

        private void Start()
        {
            _menu = PlanetContextMenu.Instance;
        }

        private Planet GetClickedPlanet()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out Planet planet))
            {
                return planet;
            }

            return null;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(1))
            {
                Planet clickedPlanet = GetClickedPlanet();

                if (clickedPlanet == _main)
                {
                    _menu.SetTarget(_main);
                    _menu.transform.position = _main.transform.position;
                    _menu.gameObject.SetActive(true);

                    if (PlanetBuffer.Instance.PlanetA == null && !_menu.WaitingConnect)
                    {
                        PlanetBuffer.Instance.SetA(_main);
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Planet clickedPlanet = GetClickedPlanet();

                if (_menu.WaitingConnect &&
                    PlanetBuffer.Instance.PlanetA != null &&
                    clickedPlanet != null &&
                    clickedPlanet != PlanetBuffer.Instance.PlanetA)
                {
                    PlanetBuffer.Instance.SetB(clickedPlanet);
                    _connector.Connect();
                    _menu.WaitingConnect = false;
                    _menu.SetActiveCompositButton(true);
                }
                else
                {
                    _menu.SetActiveCompositButton(true);
                    _menu.gameObject.SetActive(false);
                    PlanetBuffer.Instance.SetA(null);
                    PlanetBuffer.Instance.SetB(null);
                    _menu.WaitingConnect = false;
                }
            }
        }
    }
}