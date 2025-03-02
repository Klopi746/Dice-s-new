using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class EnemyItemSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canFightToggle.isOn) return;
        ChooseEnemyPanelSCRIPT.Instance.DisableAll();
        image.color = Color.red;
    }


    [SerializeField] Image image;
    public Toggle canFightToggle;
}
