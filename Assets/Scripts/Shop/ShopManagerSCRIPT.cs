using UnityEngine;
using System.Collections.Generic;

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

    #region Items
    private List<ShopItemSCRIPT> items = new List<ShopItemSCRIPT>();
    public void RegisterItem(ShopItemSCRIPT item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            items[items.Count - 1].Visualize(diceDataset[items.Count - 1]);
        }
    }

    public void SelectItem(ShopItemSCRIPT selectedItem)
    {
        if (CurrentlySelectedItem == selectedItem)
            return;

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
    [SerializeField] public List<InventoryDiceSCRIPT> inventoryDiceSlots;

    public void AssignInventoryDiceSlots(InventoryDiceSCRIPT inventoryDiceSlot)
    {
        inventoryDiceSlots.Add(inventoryDiceSlot);
        int slotDice = PlayerPrefs.GetInt("DiceInSlot" + inventoryDiceSlots.Count,0);
        inventoryDiceSlot.AssignDice(diceDataset[slotDice]);
    }
    #endregion

    #region DiceSelection
    [SerializeField] GameObject diceSelectionGO;
    #endregion
}