using UnityEngine;

namespace Planets
{
    [System.Serializable]
    public class PlanetSize
    {
        [field: SerializeField] public PlanetType Type { get; private set; }
        [field: SerializeField] public float MaxSize { get; private set; }
    }
}