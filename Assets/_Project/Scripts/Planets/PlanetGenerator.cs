using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Planets
{
    public class PlanetGenerator : MonoBehaviour
    {
        [SerializeField] private Planet _prefab;
        [SerializeField] private List<PlanetLayerSO> _enabledLayers;
        [SerializeField] private Button _generateButton;
        [SerializeField] private float _randomSize;

        private void OnEnable()
        {
            _generateButton.onClick.AddListener(GenerateButton);
        }

        private void OnDisable()
        {
            _generateButton.onClick.RemoveListener(GenerateButton);
        }

        private void GenerateButton()
        {
            Generate();
        }

        public Planet Generate()
        {
            var instance = Instantiate(_prefab.gameObject).GetComponent<Planet>();
            var layers = _prefab.Layers;

            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Activate((LayerIntensity)Random.Range((int)LayerIntensity.None, (int)LayerIntensity.High + 1));
            }

            var size = Random.Range(0f, _randomSize);
            instance.Add(layers, Vector3.zero + new Vector3(size, size, size));

            return instance;
        }
    }
}