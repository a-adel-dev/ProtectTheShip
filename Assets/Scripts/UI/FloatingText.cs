
using com.ARTillery.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.Artillery.UI
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private TextMeshPro _floatingText;

        public void DisplayFloatingText(int textValue, FloatingTextType textType)
        {
            Color textColor = ConfigureTextColor(textType);
            _floatingText.color = textColor;
            _floatingText.text = textValue.ToString();

            
            _animator.PlayInFixedTime("UpnFade", -1 , 0);
            
        }

        public void ResetAnimation()
        {
            _animator.Update(0);
        }
        
        private Color ConfigureTextColor(FloatingTextType textType)
        {
            Color textColor = new Color();

            switch (textType)
            {
                case FloatingTextType.Damage:
                    textColor = Color.red;
                    break;
                case FloatingTextType.Ore:
                    textColor =Color.yellow;
                    break;
                case FloatingTextType.OrganicMatter:
                    textColor = new Color(128, 64, 0);
                    break;
                default:
                    textColor = Color.white;
                    break;
            }

            return textColor;
        }
    }
}