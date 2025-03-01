using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDiceSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public int slotId;
    public Image cubeIcon;
    public DiceData diceData;


    private void Awake()
    {

    }

    private void Start()
    {
        ShopManagerSCRIPT.Instance.AssignInventoryDiceSlots(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShopManagerSCRIPT.Instance.DeselectEverything();
        ShopManagerSCRIPT.Instance.OpenInventoryMenu(this.gameObject);
    }

    public void AssignDice(DiceData newDiceData)
    {
        diceData = newDiceData;
        cubeIcon.sprite = diceData.diceIcon;
    }
}
