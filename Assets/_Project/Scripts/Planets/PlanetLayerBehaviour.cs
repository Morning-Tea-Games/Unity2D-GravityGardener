using Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Planets
{
    public class PlanetLayerBehaviour : MonoBehaviour
    {
        [field: SerializeField] public PlanetLayerSO Layer { get; private set; }
        [field: SerializeField] public PlanetLayerIntensity CurrentIntensity { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private GameRules _rules;

        [Inject]
        public void Construct(GameRules rules)
        {
            _rules = rules;
        }

        private void Awake()
        {
            var container = LifetimeScope.Find<PlanetLifetimeScope>().Container;
            container.Inject(this);
        }

        public void Show(PlanetLayerIntensity intensity)
        {
            // TODO: Из-за такой системы приходится всегда делать по 4 элемента, иначе ошибка
            var layer = Layer.Variations[Mathf.Clamp(
                (int)intensity,
                (int)_rules.MinPlanetLayerIntensity,
                (int)_rules.MaxPlanetLayerIntensity
                )];

            var variation = layer.Sprites[Random.Range(0, layer.Sprites.Count)];
            _spriteRenderer.sprite = variation;
            CurrentIntensity = intensity;
        }
    }
}