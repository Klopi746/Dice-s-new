using UnityEngine;
using UnityEngine.UI;

public class InventoryDiceSCRIPT : MonoBehaviour
{
    public Image cubeIcon;
    public DiceData diceData;


    private void Awake()
    {
    }

    private void Start()
    {
        ShopManagerSCRIPT.Instance.AssignInventoryDiceSlots(this);
    }

    public void AssignDice(DiceData newDiceData)
    {
        diceData = newDiceData;
        cubeIcon.sprite = diceData.diceIcon;
    }
}
