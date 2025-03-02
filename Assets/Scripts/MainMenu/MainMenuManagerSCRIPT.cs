using UnityEngine;

public class MainMenuManagerSCRIPT : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("ChoosedBet", 10);
    }
}
