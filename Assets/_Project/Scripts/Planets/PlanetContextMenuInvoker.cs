using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    public class PlanetContextMenuInvoker : MonoBehaviour
    {
        [SerializeField] private GameObject _contextMenuObject;
        [SerializeField] private Planet _main;
        [SerializeField] private PlanetContextMenu _menu;
        [SerializeField] private PlanetConnector _connector;

        private void Awake() => _contextMenuObject.SetActive(false);

        private Planet GetClickedPlanet()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent<Planet>(out Planet planet))
            {
                return planet;
            }

            return null;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(1))
            {
                Planet clickedPlanet = GetClickedPlanet();

                if (clickedPlanet == _main)
                {
                    _menu.SetTarget(_main);
                    _contextMenuObject.transform.position = _main.transform.position;
                    _contextMenuObject.SetActive(true);
                    
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
                    _contextMenuObject.SetActive(false);
                    PlanetBuffer.Instance.SetA(null);
                    PlanetBuffer.Instance.SetB(null);
                    _menu.WaitingConnect = false;
                }
            }
        }
    }
}