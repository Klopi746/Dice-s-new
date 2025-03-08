using UnityEngine;
using UnityEngine.UI;

public class PassButtSCRIPT : MonoBehaviour
{
    private Button button;
    public static PassButtSCRIPT Instance;
    private void Awake()
    {
        Instance = this;
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        button.interactable = false;
    }
    private void Start()
    {
        GameHandlerSCRIPT.Instance.OnTurnChanged.AddListener(HandleTurnChange);
    }


    public void OnButtonClicked()
    {
        GeneralSoundManagerSCRIPT.Instance.PlayButtSound();
        button.interactable = false;
        int playerLives = PlayerPrefs.GetInt("Lives");
        int curBet = GameHandlerSCRIPT.Instance.curBet;
        PlayerPrefs.SetInt("Lives", playerLives - curBet);

        int curRealLives = PlayerPrefs.GetInt("RealLives", 10);
        PlayerPrefs.SetInt("RealLives", curRealLives - 1);
        if (curRealLives - 1 == 0) PlayerPrefs.SetInt("RealWin", -1);

        AudioManager_SCRIPT.Instance.StopAllLoopingSounds();

        LoadSceneManagerSCRIPT.Instance.LoadNewScene();
    }


    private void HandleTurnChange(bool isPlayerTurn)
    {
        if (!isPlayerTurn) button.interactable = false;
    }


    public void ChangeButtInteractable(bool value)
    {
        button.interactable = value;
    }
}
