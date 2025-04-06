using UnityEngine;

namespace Planets
{
    public class GrabbableObject : MonoBehaviour
    {
        [SerializeField] private float _releaseDrag;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Camera _camera;
        private Vector3 _offset;

        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody.gravityScale = 0f;
        }

        private void OnMouseDown()
        {
            _rigidbody.drag = 0f;
            _rigidbody.velocity = Vector2.zero;

            Vector3 mouseWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;

            _offset = transform.position - mouseWorld;
        }

        private void OnMouseDrag()
        {
            Vector3 mouseWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;

            Vector2 target = mouseWorld + _offset;
            Vector2 delta = (target - (Vector2)transform.position) / Time.fixedDeltaTime;

            _rigidbody.velocity = delta;
        }

        private void OnMouseUp()
        {
            _rigidbody.drag = _releaseDrag;
        }
    }
}