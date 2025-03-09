using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerSCRIPT : PlayerPapaSCRIPT
{
    public int playerLives = 50; // some sort of money)

    public static PlayerSCRIPT Instance;
    override protected void Awake()
    {
        base.Awake();
        LoadChoosedDices();

        playerLives = PlayerPrefs.GetInt("PlayerLives", 50);

        Instance = this;
    }
    private void LoadChoosedDices()
    {
        for (int i = 0; i < cubesArray.Length; i++)
        {

            int diceId = PlayerPrefs.GetInt("DiceInSlot" + i, 0);
            DicePrefabData data = Resources.Load<DicePrefabData>($"Dice/DicePrefab{diceId}");

            cubesArray[i].GetComponent<MeshRenderer>().material = data.diceMat;
            cubesScripts[i]._faceProbabilities = data._faceProbabilities;
        }
    }

    void Start()
    {
        // Subscribe to OnTurnChanged event
        GameHandlerSCRIPT.Instance.OnTurnChanged.AddListener(HandleTurnChange);
        StartCoroutine(StartGameWithDelay(1f));
    }
    private IEnumerator StartGameWithDelay(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        GameHandlerSCRIPT.Instance.IsPlayerTurn = true; // the Player always go the first
    }
    int startTemporaryScore = 0;
    private void HandleTurnChange(bool isPlayerTurn)
    {
        if (!isPlayerTurn) return;


        Debug.Log("Player turn started!");
        StartCoroutine(GetAndDropCubes());
    }


    public void OnEndButtClick()
    {
        PutCubesAsideOnTurnEnd();

        CameraControllerSCRIPT.Instance.SetFarCamView();

        scoreText.text = (int.Parse(scoreText.text) + int.Parse(temporaryScoreText.text)).ToString();
        temporaryScoreText.text = "0";
        startTemporaryScore = 0;

        scoreText.transform.DOComplete();
        scoreText.transform.DOPunchScale(new Vector3(1, 1, 0), 1f, 4);

        GameHandlerSCRIPT.Instance.CheckForWin(int.Parse(scoreText.text), this);

        EnableAllCubes();
    }


    string clickedCubesDigitsSequence = "";
    public void UpdateClickedCubesDigits(string cubeDigit, int AddOrRemove) // 1 - Add, -1 - Remove
    {
        if (AddOrRemove == 1) clickedCubesDigitsSequence += cubeDigit;
        if (AddOrRemove == -1) { clickedCubesDigitsSequence = clickedCubesDigitsSequence.Remove(clickedCubesDigitsSequence.IndexOf(cubeDigit), 1); }

        clickedCubesDigitsSequence = new string(clickedCubesDigitsSequence.OrderBy(c => c).ToArray());
        Debug.Log($"Выбранная игроком последовательность: {clickedCubesDigitsSequence}");

        if (!noComboTextObj.gameObject.activeSelf) CheckThatClickedSequenceIsCombo(clickedCubesDigitsSequence);
    }
    private void CheckThatClickedSequenceIsCombo(string playerSequence)
    {
        if (curCombos.ContainsKey(playerSequence))
        {
            int temporaryScore = curCombos[playerSequence];
            temporaryScoreText.text = (startTemporaryScore + temporaryScore).ToString();
            ContinueButtSCRIPT.Instance.ChangeButtInteractable(true);
            ContinueButtSCRIPT.Instance.transform.DOComplete();
            ContinueButtSCRIPT.Instance.transform.DOPunchPosition(new Vector3(-200, 0, 0), 0.8f, 1);
        }
        else
        {
            temporaryScoreText.text = startTemporaryScore.ToString();
            ContinueButtSCRIPT.Instance.ChangeButtInteractable(false);
        }
        temporaryScoreText.transform.DOComplete();
        temporaryScoreText.transform.DOPunchScale(new Vector3(1, 1, 0), 1f, 4);
    }


    public override void ContinuePlay()
    {
        startTemporaryScore = int.Parse(temporaryScoreText.text);
        base.ContinuePlay();
    }
}
