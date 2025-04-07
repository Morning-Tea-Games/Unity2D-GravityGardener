using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    [CreateAssetMenu(fileName = "Planet Layer", menuName = "Planets/New Layer")]
    public class PlanetLayerSO : ScriptableObject
    {
        [field: SerializeField] public List<LayerVariationSO> Variations { get; private set; }
    }
}