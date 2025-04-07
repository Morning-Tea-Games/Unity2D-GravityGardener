using Planets;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Game Rules/New Game Rules")]
    public class GameRules : ScriptableObject
    {
        [field: Header("Planet physics")]
        [field: SerializeField] public float PlanetDrag { get; private set; }
        [field: SerializeField] public float PlanetSizeProgressionModificator { get; private set; }
        [field: SerializeField] public PlanetLayerIntensity MinPlanetLayerIntensity { get; private set; }
        [field: SerializeField] public PlanetLayerIntensity MaxPlanetLayerIntensity { get; private set; }
        [field: SerializeField] public PlanetSize[] PlanetSizes { get; private set; }
        [field: SerializeField] public PlanetLayerConflictSO[] PlanetLayerConflicts { get; private set; }

        [field: Header("Planets generator")]
        [field: SerializeField] public float MinGeneratedPlanetSize { get; private set; }
        [field: SerializeField] public float MaxGeneratedSize { get; private set; }
        [field: SerializeField] public PlanetBehaviour PlanetPrefab { get; private set; }
        [field: SerializeField] public PlanetLayerSO[] EnabledLayers { get; private set; }
        
        [field: Header("Planet Layers Configuration")]
        [field: SerializeField] public PlanetLayerSO[] DisabledForComet { get; private set; }
        [field: SerializeField] public PlanetLayerSO[] DisabledForSatellite { get; private set; }
        [field: SerializeField] public PlanetLayerSO[] DisabledForPlanet { get; private set; }
        [field: SerializeField] public PlanetLayerSO[] DisabledForGasGigant { get; private set; }
        [field: SerializeField] public PlanetLayerSO[] DisabledForStar { get; private set; }
        [field: SerializeField] public PlanetLayerSO[] DisabledForBlackHole { get; private set; }
    }
}