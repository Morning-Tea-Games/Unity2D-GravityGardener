using VContainer;
using VContainer.Unity;
using UnityEngine;
using Planets;
using VFX;

namespace Core
{
    public class PlanetLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlanetContextMenuBehaviour _contextMenu;
        [SerializeField] private GameRules _rules;
        [SerializeField] private PlanetSystemManagerBehaviour _planetManager;
        [SerializeField] private NebulaBehaviour _nebula;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_contextMenu);
            builder.RegisterInstance(_rules);
            builder.RegisterInstance(_planetManager);
            builder.RegisterInstance(_nebula);

            builder.Register<PlanetMerger>(Lifetime.Singleton);
            builder.Register<PlanetGenerator>(Lifetime.Singleton);
            builder.Register<PlanetLayerConflictResolver>(Lifetime.Singleton);
            builder.Register<PlanetTypeIdentifier>(Lifetime.Singleton);
        }
    }
}