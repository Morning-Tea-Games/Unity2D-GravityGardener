using Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PhysicsFX
{
    public class DragBehaviour : MonoBehaviour
    {
        public bool IsActive = true;

        [SerializeField] private Rigidbody2D _rigidbody;
        
        private GameRules _rules;
        private Camera _camera;
        private Vector3 _offset;

        [Inject]
        public void Construct(GameRules rules)
        {
            _rules = rules;
            _camera = Camera.main;
        }

        private void Awake()
        {
            var container = LifetimeScope.Find<PlanetLifetimeScope>().Container;
            container.Inject(this);
        }

        private void OnMouseDown()
        {
            if (!IsActive)
            {
                return;
            }
            
            _rigidbody.drag = 0f;
            _rigidbody.velocity = Vector2.zero;

            Vector3 mouseWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;

            _offset = transform.position - mouseWorld;
        }

        private void OnMouseDrag()
        {
            if (!IsActive)
            {
                return;
            }
            
            Vector3 mouseWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;

            Vector2 target = mouseWorld + _offset;
            Vector2 delta = (target - (Vector2)transform.position) / Time.fixedDeltaTime;

            _rigidbody.velocity = delta;
        }

        private void OnMouseUp()
        {
            _rigidbody.drag = _rules.PlanetDrag;
        }
    }
}