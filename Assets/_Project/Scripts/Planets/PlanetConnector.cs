using UnityEngine;

namespace Planets
{
    public class PlanetConnector : MonoBehaviour
    {
        [SerializeField] private float _addSizeModifier;

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

            if (a.transform.localScale.y < b.transform.localScale.y)
            {
                main = b;
                target = a;
            }
            else if (a.transform.localScale.y > b.transform.localScale.y)
            {
                main = a;
                target = b;
            }
            else
            {
                main = Random.value >= 0.5f ? b : a;
                target = main == a ? b : a;
            }

            main.Add(target.Layers, b.transform.localScale * _addSizeModifier);
            Destroy(target.gameObject);
        }
    }
}