
using TMPro;
using UnityEngine;

namespace com.Artillery.UI
{
    public class FloatingDamage : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private TextMeshProUGUI _damageText;

        public void DisplayDamage(int damageValue)
        {
            _damageText.text = damageValue.ToString();
            _animator.Play("floatingtextAnimation");
        }
    }
}