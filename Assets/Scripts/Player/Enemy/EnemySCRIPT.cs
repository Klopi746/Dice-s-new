using UnityEngine;
using Newtonsoft.Json;

public class EnemySCRIPT : PlayerPapaSCRIPT
{
    public int enemyId;

    private EnemyData enemyData;
    void Awake()
    {
        // Load Enemy Data
        enemyId = PlayerPrefs.GetInt("EnemyId", 1);

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
        playerName = enemyData.enemyName;
        ownedCubes = enemyData.ownedCubes;
    }
}
