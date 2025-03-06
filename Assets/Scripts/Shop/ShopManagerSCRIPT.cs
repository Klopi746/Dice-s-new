using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class ShopManagerSCRIPT : MonoBehaviour
{
    public static ShopManagerSCRIPT Instance { get; private set; }
    public ShopItemSCRIPT CurrentlySelectedItem { get; private set; }

    public List<DiceData> diceDataset;
    public List<Sprite> diceSideIcons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        DeselectEverything();
    }

    #region Items
    public List<ShopItemSCRIPT> items = new List<ShopItemSCRIPT>(6);
    public void RegisterItem(ShopItemSCRIPT item)
    {
        items[item.diceId] = item;
    }
    public int CheckAmountOfBuyedDiceId(int diceId)
    {
        return items[diceId].GetCurAmountOfDice();
    }

    public void SelectItem(ShopItemSCRIPT selectedItem)
    {
        if (CurrentlySelectedItem == selectedItem)
            return;

        ShopManagerSCRIPT.Instance.DeselectEverything();

        if (CurrentlySelectedItem != null)
            CurrentlySelectedItem.SetSelected(false);

        CurrentlySelectedItem = selectedItem;
        CurrentlySelectedItem.SetSelected(true);
    }

    public void DeselectCurrentItem()
    {
        if (CurrentlySelectedItem != null)
        {
            CurrentlySelectedItem.SetSelected(false);
            CurrentlySelectedItem = null;
        }
    }
    #endregion

    #region Inventory
    [SerializeField] public List<InventoryDiceSCRIPT> inventoryDiceSlots = new List<InventoryDiceSCRIPT>(6);

    public void AssignInventoryDiceSlots(InventoryDiceSCRIPT inventoryDiceSlot)
    {
        inventoryDiceSlots[inventoryDiceSlot.slotId] = inventoryDiceSlot;
        int slotDice = PlayerPrefs.GetInt("DiceInSlot" + inventoryDiceSlot.slotId, 0);
        inventoryDiceSlot.AssignDice(diceDataset[slotDice]);
        inventoryDiceSlot.UpdateSlotDice(slotDice);
    }
    public void UpdateSlotDice(int slotId)
    {
        int slotDice = PlayerPrefs.GetInt("DiceInSlot" + slotId, 0);
        inventoryDiceSlots[slotId].UpdateSlotDice(slotDice);
        inventoryDiceSlots[slotId].AssignDice(diceDataset[slotDice]);
    }
    public int CheckAmountOfAssignDiceId(int diceId)
    {
        int amount = 0;
        foreach (var script in inventoryDiceSlots)
        {
            if (script.slotDice == diceId) amount += 1;
        }
        return amount;
    }
    #endregion

    #region DiceSelection
    [SerializeField] GameObject diceSelectionGO;


    public void OpenInventoryMenu(GameObject inventorySlot)
    {
        diceSelectionGO.transform.DOMove(new Vector3(diceSelectionGO.transform.position.x, inventorySlot.transform.position.y, diceSelectionGO.transform.position.z), 0.2f);
    }
    #endregion

    public void DeselectEverything()
    {
        diceSelectionGO.transform.DOMove(new Vector3(diceSelectionGO.transform.position.x, -700, diceSelectionGO.transform.position.z), 0.2f);
        DeselectCurrentItem();
    }
}
