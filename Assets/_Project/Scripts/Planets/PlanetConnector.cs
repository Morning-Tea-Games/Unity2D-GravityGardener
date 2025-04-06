using UnityEngine;

namespace Planets
{
    public class PlanetConnector : MonoBehaviour
    {
        private PlanetBuffer _buffer;

        private void Start()
        {
            _buffer = PlanetBuffer.Instance;
        }

        public void Connect()
        {
            var a = _buffer.PlanetA;
            var b = _buffer.PlanetB;

            Planet main;
            Planet target;

            if (a.Collider.radius < b.Collider.radius)
            {
                main = b;
                target = a;
            }
            else if (a.Collider.radius > b.Collider.radius)
            {
                main = a;
                target = b;
            }
            else
            {
                main = Random.value >= 0.5f ? b : a;
                target = main == a ? b : a;
            }

            main.Add(target.Layers, 1);
        }
    }
}