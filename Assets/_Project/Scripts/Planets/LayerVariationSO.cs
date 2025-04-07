using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    [CreateAssetMenu(fileName = "New Variation", menuName = "Planets/New Layer Variation")]
    public class LayerVariationSO : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> Sprites { get; private set; }
    }
}