using UnityEngine;

public class PlayerSCRIPT : PlayerPapaSCRIPT
{
    public int playerLives = 50; // some sort of money)

    public static PlayerSCRIPT Instance;
    override protected void Awake()
    {
        base.Awake();

        playerLives = PlayerPrefs.GetInt("PlayerLives", 50);

        Instance = this;
    }

    void Start()
    {
        // Subscribe to OnTurnChanged event
        GameHandlerSCRIPT.Instance.OnTurnChanged.AddListener(HandleTurnChange);
        GameHandlerSCRIPT.Instance.IsPlayerTurn = true; // the Player always go the first
    }
    private void HandleTurnChange(bool isPlayerTurn)
    {
        if (!isPlayerTurn) return;


        Debug.Log("Player turn started!");
        StartCoroutine(GetAndDropCubes());
    }


    public void ContinuePlay() // throw cubes again
    {
        HandleTurnChange(true);
    }


    public void OnEndButtClick()
    {
        CameraControllerSCRIPT.Instance.SetFarCamView();
    }
}
