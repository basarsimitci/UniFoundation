using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UniFoundation.UI
{
    public class BarView : MonoBehaviour
    {
        [Header("Bar")]
        [SerializeField] private Image barImage;
        [SerializeField] private Color barColour = Color.white;
        [SerializeField] private Ease barAnimationEasing = Ease.Linear;
        [SerializeField] private float barAnimationDuration;
        
        [Header("Value Text")]
        [SerializeField] private bool showValueText;
        [SerializeField] private bool showMaxValue;
        [SerializeField] private TextMeshProUGUI valueText;

        private float maxValue;

        public float MaxValue
        {
            set => maxValue = value;
        }

        public float Value
        {
            set
            {
                if ((barImage != null) && (maxValue > 0))
                {
                    barImage
                        .DOFillAmount(value / maxValue, barAnimationDuration)
                        .SetEase(barAnimationEasing);
                }

                if (showValueText && (valueText != null))
                {
                    string maxValueString = showMaxValue ? $" / {maxValue:F0}" : "";
                    valueText.text = $"{value:F0}{maxValueString}";
                }
            }
        }
        
        private void Awake()
        {
            if (valueText != null)
            {
                valueText.gameObject.SetActive(showValueText);
            }
        }
    }
}