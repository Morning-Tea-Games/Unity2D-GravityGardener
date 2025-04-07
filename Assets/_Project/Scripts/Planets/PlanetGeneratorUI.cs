using Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Planets
{
    public class PlanetGeneratorUI : MonoBehaviour
    {
        [SerializeField] private Button _generateButton;
        [SerializeField] private Transform _parent;
        
        private PlanetSystemManagerBehaviour _planetManager;

        private PlanetGenerator _generator;

        [Inject]
        public void Construct(PlanetGenerator generator, PlanetSystemManagerBehaviour planetManager)
        {
            _generator = generator;
            _planetManager = planetManager;
        }

        private void Awake()
        {
            var container = LifetimeScope.Find<PlanetLifetimeScope>().Container;
            container.Inject(this);
        }

        private void OnEnable()
        {
            _generateButton.onClick.AddListener(GeneratePlanet);
        }

        private void OnDisable()
        {
            _generateButton.onClick.RemoveListener(GeneratePlanet);
        }

        private void GeneratePlanet()
        {
            var planet = Instantiate(_generator.Generate(), _parent.position, Quaternion.identity, _parent);
            planet.GetComponent<CircleCollider2D>().enabled = true; // TODO: разобраться с аномалией выключения компонентов при спавне
            planet.GetComponent<PlanetContextMenuInvokerBehaviour>().enabled = true;
            planet.GetComponent<PlanetBehaviour>().enabled = true;
            planet.gameObject.SetActive(true);
            _planetManager.Add(planet);
        }
    }
}