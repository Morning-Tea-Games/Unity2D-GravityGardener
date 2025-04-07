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

        private void Update()
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
    }
}