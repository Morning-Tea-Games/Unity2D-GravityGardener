using UnityEngine;

namespace PhysicsFX
{
    [ExecuteAlways]
    [RequireComponent(typeof(Camera))]
    public class CameraBoundsCollider : MonoBehaviour
    {
        [SerializeField] private float thickness = 1f;

        private BoxCollider2D[] borders = new BoxCollider2D[4]; // Left, Right, Top, Bottom

        private void OnEnable()
        {
            CreateOrAssignColliders();
            UpdateColliders();
        }

        private void Update()
        {
    #if UNITY_EDITOR
            if (!Application.isPlaying)
                UpdateColliders();
    #endif
        }

        private void CreateOrAssignColliders()
        {
            for (int i = 0; i < 4; i++)
            {
                if (borders[i] == null)
                {
                    GameObject go = new GameObject("Border_" + i);
                    go.transform.parent = transform;
                    go.layer = gameObject.layer;
                    borders[i] = go.AddComponent<BoxCollider2D>();
                }
            }
        }

        private void UpdateColliders()
        {
            Camera cam = GetComponent<Camera>();
            if (!cam.orthographic)
            {
                Debug.LogWarning("Camera must be orthographic!");
                return;
            }

            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            Vector3 camPos = cam.transform.position;

            // Left
            borders[0].transform.position = camPos + new Vector3(-width / 2 - thickness / 2, 0, 0);
            borders[0].size = new Vector2(thickness, height + 2 * thickness);

            // Right
            borders[1].transform.position = camPos + new Vector3(width / 2 + thickness / 2, 0, 0);
            borders[1].size = new Vector2(thickness, height + 2 * thickness);

            // Top
            borders[2].transform.position = camPos + new Vector3(0, height / 2 + thickness / 2, 0);
            borders[2].size = new Vector2(width + 2 * thickness, thickness);

            // Bottom
            borders[3].transform.position = camPos + new Vector3(0, -height / 2 - thickness / 2, 0);
            borders[3].size = new Vector2(width + 2 * thickness, thickness);
        }
    }   
}