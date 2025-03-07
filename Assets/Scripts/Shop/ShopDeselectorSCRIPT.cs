using UnityEngine;
using UnityEngine.EventSystems;

public class ShopDeselectorSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ShopManagerSCRIPT.Instance.DeselectEverything();
    }
}
