using UnityEngine;
using Newtonsoft.Json;

public class EnemySCRIPT : PlayerPapaSCRIPT
{
    public int enemyId; // that's type of enemy - loading from PlayerPrefs

    private EnemyData enemyData; // for JSON parse
    override protected void Awake()
    {
        base.Awake();

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


    void Start()
    {
        // assing enemyData to this GameObj - O, maybe use enemyData... I need to think about that - MOMMY
        playerName = enemyData.enemyName;
        ownedCubes = enemyData.ownedCubes;

        // Subscribe to OnTurnChanged event
        GameHandlerSCRIPT.Instance.OnTurnChanged.AddListener(HandleTurnChange);
    }
    private void HandleTurnChange(bool isPlayerTurn)
    {
        if (isPlayerTurn) return;


        Debug.Log("Enemy turn started! ;)");
        GetAndDropCubes();
    }
}
