using VContainer;
using VContainer.Unity;
using UnityEngine;
using Planets;

namespace Core
{
    public class PlanetLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlanetContextMenuBehaviour _contextMenu;
        [SerializeField] private GameRules _rules;
        [SerializeField] private PlanetSystemManagerBehaviour _planetManager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_contextMenu);
            builder.RegisterInstance(_rules);
            builder.RegisterInstance(_planetManager);

            builder.Register<PlanetMerger>(Lifetime.Singleton);
            builder.Register<PlanetGenerator>(Lifetime.Singleton);
            builder.Register<PlanetLayerConflictResolver>(Lifetime.Singleton);
            builder.Register<PlanetTypeIdentifier>(Lifetime.Singleton);
        }
    }
}