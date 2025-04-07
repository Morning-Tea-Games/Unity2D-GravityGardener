using System.Collections.Generic;
using Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Planets
{
    public class PlanetBehaviour : MonoBehaviour
    {
        [field: SerializeField] public List<PlanetLayerBehaviour> Layers { get; private set; }
        [field: SerializeField] public PlanetType CurrentType { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public int PlanetSystemLayer { get; private set; }

        private PlanetTypeIdentifier _identifier;

        [Inject]
        public void Construct(PlanetTypeIdentifier identifier)
        {
            _identifier = identifier;
        }

        private void Awake()
        {
            var container = LifetimeScope.Find<PlanetLifetimeScope>().Container;
            container.Inject(this);
        }

        public void DefineType()
        {
            if (_identifier.TryDefine(this, out var type))
            {
                CurrentType = type;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetPlanetSystemLayer(int layer)
        {
            PlanetSystemLayer = layer;
        }
    }
}