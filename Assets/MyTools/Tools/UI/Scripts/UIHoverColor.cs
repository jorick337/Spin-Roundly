using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UITools.Colors
{
    public class HighlightColorInUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Core")]
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color originalColor;

        [Header("UI")]
        [SerializeField] private Graphic uiElement;

        public void OnPointerEnter(PointerEventData eventData) => uiElement.color = hoverColor;
        public void OnPointerExit(PointerEventData eventData) => uiElement.color = originalColor;
    }
}