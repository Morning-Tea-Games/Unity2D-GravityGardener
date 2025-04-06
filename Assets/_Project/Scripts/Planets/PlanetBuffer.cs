using UnityEngine;

namespace Planets
{
    public class PlanetBuffer : MonoBehaviour
    {
        public static PlanetBuffer Instance;

        public Planet PlanetA { get; private set; }
        public Planet PlanetB { get; private set; }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void SetA(Planet a)
        {
            PlanetA = a;
        }

        public void SetB(Planet b)
        {
            PlanetB = b;
        }
    }
}