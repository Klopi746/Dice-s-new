using UnityEngine;
using UnityEngine.UI;

public class ChooseEnemyPanelSCRIPT : MonoBehaviour
{
    public static ChooseEnemyPanelSCRIPT Instance;


    [SerializeField] EnemyItemSCRIPT[] EnemiesItemScripts;
    private void Awake()
    {
        Instance = this;
        int enemiesOpen = PlayerPrefs.GetInt("EnemiesOpen", 1);
        for (int i = 1; i <= EnemiesImageComponents.Length; i++)
        {
            if (enemiesOpen < 1) break;
            EnemiesItemScripts[EnemiesImageComponents.Length - i].canFightToggle.isOn = true;
            enemiesOpen -= 1;
        }
    }


    [SerializeField] Image[] EnemiesImageComponents;
    public void DisableAll()
    {
        foreach (var image in EnemiesImageComponents)
        {
            image.color = Color.white;
        }
    }
    public void FindEnemyAndSaveIt()
    {
        for (int i = 1; i <= EnemiesImageComponents.Length; i++)
        {
            if (EnemiesImageComponents[EnemiesImageComponents.Length - i].color == Color.red)
            {
                PlayerPrefs.SetInt("ChoosedEnemyId", i);
                break;
            }
        }
    }


    int choosedBet = 10;
    public void SaveChoosedBet(int value)
    {
        AssignChoosedDropDownButtToBetValue(value);
        PlayerPrefs.SetInt("ChoosedBet", choosedBet);
    }
    private void AssignChoosedDropDownButtToBetValue(int choosedValue)
    {
        switch (choosedValue)
        {
            case 0:
                choosedBet = 10;
                break;
            case 1:
                choosedBet = 50;
                break;
            case 2:
                choosedBet = 100;
                break;
            case 3:
                choosedBet = 300;
                break;
            default:
                choosedBet = 10;
                break;
        }
    }
}
