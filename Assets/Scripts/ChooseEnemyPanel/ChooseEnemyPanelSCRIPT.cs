using TMPro;
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
        for (int i = 0; i < EnemiesImageComponents.Length; i++)
        {
            if (enemiesOpen < 1) break;
            EnemiesItemScripts[i].canFightToggle.isOn = true;
            enemiesOpen -= 1;
        }
        // Load Data to enemyItem
        for (int i = 0; i < EnemiesImageComponents.Length; i++)
        {
            EnemiesItemScripts[i].LoadEnemyData(i+1);
        }

        dropDownComp.onValueChanged.AddListener(CheckThatBetLessThanLivesThenSaveIt);
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
        for (int i = 0; i < EnemiesImageComponents.Length; i++)
        {
            if (EnemiesImageComponents[i].color == Color.red)
            {
                PlayerPrefs.SetInt("EnemyId", i+1);
                break;
            }
        }
    }


    int choosedBet = 10;
    [SerializeField] TMP_Dropdown dropDownComp;
    public void CheckThatBetLessThanLivesThenSaveIt(int value)
    {
        GeneralSoundManagerSCRIPT.Instance.PlayBuySound();

        AssignChoosedDropDownButtToBetValue(value);
        if (choosedBet > MainMenuManagerSCRIPT.Instance.Lives)
        {
            dropDownComp.value = 0;
            MainMenuManagerSCRIPT.Instance.ShowErrorOnLives();
            return;
        } // PlaySound();
        else PlayerPrefs.SetInt("ChoosedBet", choosedBet);
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
