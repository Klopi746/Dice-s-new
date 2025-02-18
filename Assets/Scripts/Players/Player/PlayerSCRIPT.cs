using UnityEngine;

public class PlayerSCRIPT : PlayerPapaSCRIPT
{
    public int playerLives = 50;

    override protected void Awake()
    {
        base.Awake();

        playerLives = PlayerPrefs.GetInt("PlayerLives", 50);
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
    }
}
