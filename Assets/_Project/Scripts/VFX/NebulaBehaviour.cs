using UnityEngine;
using UnityEngine.UI;

namespace VFX
{
    public class NebulaBehaviour : MonoBehaviour
    {
        [SerializeField] private Image _spriteRenderer;
        [SerializeField] private Sprite[] _enabled;

        private void Awake()
        {
            Change();
        }

        public void Change()
        {
            Sprite target;

            do
            {
                target = _enabled[Random.Range(0, _enabled.Length)];
            } while (_spriteRenderer.sprite == target);

            _spriteRenderer.sprite = target;
        }
    }
}