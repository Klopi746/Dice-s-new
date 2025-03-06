using UnityEngine;
using UnityEngine.UI;

public class DiceSelectorSCRIPT : MonoBehaviour
{
    public static DiceSelectorSCRIPT Instance;
    private void Awake()
    {
        Instance = this;
    }


    public int curSlotId;


    [SerializeField] Image[] DiceImageComponents;
    public void DisableAll()
    {
        foreach (var image in DiceImageComponents)
        {
            image.color = Color.white;
        }
    }
    public void EnableCubeFromInventorySlot(int cubeId)
    {
        DisableAll();
        DiceImageComponents[cubeId].color = Color.yellow;
    }
}