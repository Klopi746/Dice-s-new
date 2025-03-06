using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDiceSelectionSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public int diceId;


    [SerializeField] Image ImageComponent;
    public void OnPointerClick(PointerEventData eventData)
    {
        DiceSelectorSCRIPT.Instance.DisableAll();
        ImageComponent.color = Color.yellow;
        PlayerPrefs.SetInt("DiceInSlot" + DiceSelectorSCRIPT.Instance.curSlotId, diceId);
        ShopManagerSCRIPT.Instance.UpdateSlotDice(DiceSelectorSCRIPT.Instance.curSlotId);
    }
}