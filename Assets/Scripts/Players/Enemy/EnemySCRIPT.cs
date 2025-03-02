using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using Enemy.AI;
using System;

public class EnemySCRIPT : PlayerPapaSCRIPT
{
    public int enemyId; // that's type of enemy - loading from PlayerPrefs
    public static EnemySCRIPT Instance;

    private EnemyData enemyData; // for JSON parse
    override protected void Awake()
    {
        base.Awake();

        Instance = this;

        enemyId = PlayerPrefs.GetInt("EnemyId", 1);

        LoadEnemyData();
    }
    private void LoadEnemyData()
    {
        var enemySetUpFile = Resources.Load<TextAsset>($"Enemy/SetUp{enemyId}");
        if (enemySetUpFile == null) { Debug.LogError($"Failed to load EnemySetUp for ID: {enemyId}"); return; }

        try
        {
            enemyData = JsonConvert.DeserializeObject<EnemyData>(enemySetUpFile.text);

            Debug.Log($"Loaded enemy: {enemyData.enemyName}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to parse JSON: {e.Message}"); // Maybe we need here to load default or Load MainMenu to avoid errors - MOMMY
        }
    }


    int startTemporaryScore = 0;
    AIChooseLogicPapaClass AiLogicScript;
    void Start()
    {
        // assing enemyData to this GameObj - O, maybe use enemyData... I need to think about that - MOMMY
        playerName = enemyData.enemyName;
        ownedCubes = enemyData.ownedCubes;
        if (AiLogicDictionary.AI_TypeMap.TryGetValue(enemyData.aiType, out Type type))
        {
            gameObject.AddComponent(type);
            AiLogicScript = gameObject.GetComponent<AIChooseLogicPapaClass>();
        }
        else Debug.LogWarning("AI не загрузился!");

        // Subscribe to OnTurnChanged event
        GameHandlerSCRIPT.Instance.OnTurnChanged.AddListener(HandleTurnChange);
    }
    private void HandleTurnChange(bool isPlayerTurn)
    {
        if (isPlayerTurn) return;


        Debug.Log("Enemy turn started! ;)");
        StartCoroutine(DoAIthings());
    }
    public bool continuePlay = false;
    private IEnumerator DoAIthings()
    {
        yield return StartCoroutine(GetAndDropCubes());
        yield return StartCoroutine(AiLogicScript.AILogic()); // HERE AI CHOOSE COMBO LOGIC
        if (continuePlay) ContinuePlay();
        else
        {
            OnTurnEnd();
            GameHandlerSCRIPT.Instance.EndTurn();
        }
    }


    public void OnTurnEnd()
    {
        Debug.Log("AI end turn;");
        CameraControllerSCRIPT.Instance.SetFarCamView();

        scoreText.text = (int.Parse(scoreText.text) + int.Parse(temporaryScoreText.text)).ToString();
        temporaryScoreText.text = "0";
        startTemporaryScore = 0;

        GameHandlerSCRIPT.Instance.CheckForWin(int.Parse(scoreText.text), this);

        EnableAllCubes();
    }


    string clickedCubesDigitsSequence = "";
    public void UpdateClickedCubesDigits(string cubeDigit, int AddOrRemove) // 1 - Add, -1 - Remove
    {
        if (AddOrRemove == 1) clickedCubesDigitsSequence += cubeDigit;
        if (AddOrRemove == -1) { clickedCubesDigitsSequence = clickedCubesDigitsSequence.Remove(clickedCubesDigitsSequence.IndexOf(cubeDigit), 1); }

        clickedCubesDigitsSequence = new string(clickedCubesDigitsSequence.OrderBy(c => c).ToArray());
        Debug.Log($"Выбранная AI игроком последовательность: {clickedCubesDigitsSequence}");

        CheckThatClickedSequenceIsCombo(clickedCubesDigitsSequence);
    }
    private void CheckThatClickedSequenceIsCombo(string playerSequence)
    {
        if (curCombos.ContainsKey(playerSequence))
        {
            int temporaryScore = curCombos[playerSequence];
            temporaryScoreText.text = (startTemporaryScore + temporaryScore).ToString();
        }
        else
        {
            temporaryScoreText.text = startTemporaryScore.ToString();
        }
    }


    public override void ContinuePlay()
    {
        Debug.Log("AI continue play");
        startTemporaryScore = int.Parse(temporaryScoreText.text);
        TurnOffCubesOutline();
        DisableCubeOnContinueIfClicked();
        CheckThatThereIsNoMoreCubes();
        StartCoroutine(DoAIthings());
    }
}
