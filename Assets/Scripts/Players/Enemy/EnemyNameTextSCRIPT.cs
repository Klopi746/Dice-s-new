using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyNameTextSCRIPT : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshPro textMeshProComponent;
    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshProComponent.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshProComponent.enabled = false;
    }
}
