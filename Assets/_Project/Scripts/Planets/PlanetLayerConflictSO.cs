using UnityEngine;

namespace Planets
{
    [CreateAssetMenu(fileName = "Layer Conflict", menuName = "Planets/New Conflict")]
    public class PlanetLayerConflictSO : ScriptableObject
    {
        [field: SerializeField] public PlanetLayerSO A { get; private set; }
        [field: SerializeField] public PlanetLayerSO B { get; private set; }
    }
}