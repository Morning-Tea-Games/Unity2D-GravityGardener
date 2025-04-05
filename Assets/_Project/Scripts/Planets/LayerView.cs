using UnityEngine;

namespace Planets
{
    public class LayerView : MonoBehaviour
    {
        [field: SerializeField] public PlanetLayerSO Layer { get; private set; }

        public LayerIntensity CurrentIntensity;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Activate(LayerIntensity intensity)
        {
            var layer = Layer.Variations[(int)intensity];
            var sprite = layer.Sprites[Random.Range(0, layer.Sprites.Count)];
            _spriteRenderer.sprite = sprite;
            CurrentIntensity = intensity;
        }
    }
}