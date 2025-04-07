using VContainer;
using VContainer.Unity;
using UnityEngine;
using Core;
using Planets;

namespace Core
{
    public class PlanetLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlanetContextMenuBehaviour _contextMenu;
        [SerializeField] private GameRules _rules;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_contextMenu);
            builder.RegisterInstance(_rules);

            builder.Register<PlanetMerger>(Lifetime.Singleton);
            builder.Register<PlanetGenerator>(Lifetime.Singleton);
            builder.Register<PlanetLayerConflictResolver>(Lifetime.Singleton);
            builder.Register<PlanetTypeIdentifier>(Lifetime.Singleton);
        }
    }
}