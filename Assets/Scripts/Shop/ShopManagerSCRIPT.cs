using UnityEngine;
using System.Collections.Generic;

public class ShopManagerSCRIPT : MonoBehaviour
{
    public static ShopManagerSCRIPT Instance { get; private set; }
    public ShopItemSCRIPT CurrentlySelectedItem { get; private set; }

    private List<ShopItemSCRIPT> items = new List<ShopItemSCRIPT>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void RegisterItem(ShopItemSCRIPT item)
    {
        if (!items.Contains(item))
            items.Add(item);
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
}