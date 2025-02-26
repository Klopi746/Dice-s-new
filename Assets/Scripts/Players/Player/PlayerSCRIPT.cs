using System.Linq;
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


    public void OnEndButtClick()
    {
        CameraControllerSCRIPT.Instance.SetFarCamView();
    }


    string clickedCubesDigitsSequence = "";
    public void UpdateClickedCubesDigits(string cubeDigit, int AddOrRemove) // 1 - Add, -1 - Remove
    {
        if (AddOrRemove == 1) clickedCubesDigitsSequence += cubeDigit;
        if (AddOrRemove == -1) {clickedCubesDigitsSequence = clickedCubesDigitsSequence.Remove(clickedCubesDigitsSequence.IndexOf(cubeDigit), 1);}

        clickedCubesDigitsSequence = new string(clickedCubesDigitsSequence.OrderBy(c => c).ToArray());
        Debug.Log($"Выбранная игроком последовательность: {clickedCubesDigitsSequence}");
        
        CheckThatClickedSequenceIsCombo(clickedCubesDigitsSequence);
    }
    private void CheckThatClickedSequenceIsCombo(string playerSequence)
    {
        if (curCombos.ContainsKey(playerSequence))
        {
            int temporaryScore = curCombos[playerSequence];
            temporaryScoreText.text = temporaryScore.ToString();
        }
        else
        {
            temporaryScoreText.text = "0";
        }
    }
}
