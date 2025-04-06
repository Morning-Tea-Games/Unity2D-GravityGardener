using UnityEngine;

namespace Planets
{
    public class LayerView : MonoBehaviour
    {
        [field: SerializeField] public PlanetLayerSO Layer { get; private set; }

        public LayerIntensity CurrentIntensity { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Activate(LayerIntensity intensity)
        {
            var layer = Layer.Variations[Mathf.Clamp((int)intensity, (int)LayerIntensity.None, (int)LayerIntensity.High)];
            var sprite = layer.Sprites[Random.Range(0, layer.Sprites.Count)];
            _spriteRenderer.sprite = sprite;
            CurrentIntensity = intensity;
        }
    }
}