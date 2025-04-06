using UnityEngine;

namespace Planets
{
    [CreateAssetMenu(fileName = "Planet Sizes", menuName = "Planets/Planet Sizes")]
    public class PlanetSizeSO : ScriptableObject
    {
        [field: SerializeField] public PlanetSize[] Configuration { get; private set; }
    }
}