using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDiceSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public int slotId;
    public Image cubeIcon;
    public DiceData diceData;
    public int slotDice;

    public void LoadDataOfSlot(int SlotId)
    {
        slotDice = PlayerPrefs.GetInt("DiceInSlot" + SlotId, 0);
    }

    private void Start()
    {
        ShopManagerSCRIPT.Instance.AssignInventoryDiceSlots(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShopManagerSCRIPT.Instance.DeselectEverything();
        ShopManagerSCRIPT.Instance.OpenInventoryMenu(this.gameObject);
        DiceSelectorSCRIPT.Instance.curSlotId = slotId;
        DiceSelectorSCRIPT.Instance.EnableCubeFromInventorySlot(slotDice);
        GeneralSoundManagerSCRIPT.Instance.PlayShopWindClickSound();
    }

    public void AssignDice(DiceData newDiceData)
    {
        diceData = newDiceData;
        cubeIcon.sprite = diceData.diceIcon;
    }
    public void UpdateSlotDice(int slotDice)
    {
        this.slotDice = slotDice;
    }
}
