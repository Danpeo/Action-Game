using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _сurrentImage;

        public void SetValue(float current, float max) => 
            _сurrentImage.fillAmount = current / max;
    }
}