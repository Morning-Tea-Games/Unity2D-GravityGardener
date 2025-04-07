using System.Collections.Generic;
using PhysicsFX;
using UnityEngine;
using UnityEngine.UI;

namespace Planets
{
    public class PlanetSystemManagerBehaviour : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private LayerMask[] _layerMasks;
        [SerializeField] private float _transparency;

        private Dictionary<LayerMask, List<PlanetBehaviour>> _planets;
        private Dictionary<PlanetBehaviour, SpriteRenderer[]> _planetRenderers;

        private void Start()
        {
            _planets = new Dictionary<LayerMask, List<PlanetBehaviour>>();
            for (int i = 0; i < _layerMasks.Length; i++)
            {
                _planets.Add(_layerMasks[i], new List<PlanetBehaviour>());
            }

            _planetRenderers = new Dictionary<PlanetBehaviour, SpriteRenderer[]>();
            UpdateTransparency();
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(delegate { UpdateTransparency(); });
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(delegate { UpdateTransparency(); });
        }

        private void UpdateTransparency()
        {
            int active = Mathf.RoundToInt(_slider.value);

            var toRemove = new List<PlanetBehaviour>();

            foreach (var kv in _planetRenderers)
            {
                PlanetBehaviour planet = kv.Key;
                if (planet == null)
                {
                    toRemove.Add(planet);
                    continue;
                }

                float alpha = (planet.PlanetSystemLayer == active) ? 1f : _transparency;
                bool isActive = planet.PlanetSystemLayer == active;

                foreach (var sr in kv.Value)
                {
                    if (sr != null)
                    {
                        Color c = sr.color;
                        c.a = alpha;
                        sr.color = c;
                    }
                }

                if (planet.TryGetComponent<PlanetContextMenuInvokerBehaviour>(out var invoker))
                    invoker.enabled = isActive;

                if (planet.TryGetComponent<DragBehaviour>(out var drag))
                    drag.IsActive = isActive;
            }

            foreach (var p in toRemove)
                _planetRenderers.Remove(p);
        }


        public void Add(PlanetBehaviour planet)
        {
            int idx = Mathf.RoundToInt(_slider.value) - 1;
            if (idx < 0 || idx >= _layerMasks.Length) return;

            LayerMask currentLayerMask = _layerMasks[idx];
            planet.gameObject.layer = (int)Mathf.Log(currentLayerMask.value, 2);

            if (!_planets[currentLayerMask].Contains(planet))
                _planets[currentLayerMask].Add(planet);

            planet.SetPlanetSystemLayer((int)_slider.value);

            if (!_planetRenderers.ContainsKey(planet))
                _planetRenderers.Add(planet, planet.GetComponentsInChildren<SpriteRenderer>());

            UpdateTransparency();
        }
    }
}