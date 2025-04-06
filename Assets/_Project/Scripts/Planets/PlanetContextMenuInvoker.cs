using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    // NOTE: Requires any 2D collider
    public class PlanetContextMenuInvoker : MonoBehaviour
    {
        [SerializeField] private GameObject _contextMenuObject;
        [SerializeField] private Planet _target;

        private void Awake()
        {
            _contextMenuObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (hit.collider.TryGetComponent<Planet>(out var _))
                    {
                        _contextMenuObject.SetActive(true);
                        PlanetBuffer.Instance.SetA(_target);
                        PlanetBuffer.Instance.SetB(_target);

                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                _contextMenuObject.SetActive(false);
            }
        }
    }
}