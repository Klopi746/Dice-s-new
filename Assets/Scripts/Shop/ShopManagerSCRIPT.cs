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
    private List<ShopItemSCRIPT> items = new List<ShopItemSCRIPT>();
    public void RegisterItem(ShopItemSCRIPT item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
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


    public void OpenInventoryMenu(GameObject inventorySlot)
    {
        diceSelectionGO.transform.DOMove(new Vector3(diceSelectionGO.transform.position.x, inventorySlot.transform.position.y, diceSelectionGO.transform.position.z),0.2f);
    }
    #endregion

    public void DeselectEverything()
    {
        diceSelectionGO.transform.DOMove(new Vector3(diceSelectionGO.transform.position.x,-700, diceSelectionGO.transform.position.z), 0.2f);
        DeselectCurrentItem();
    }
}