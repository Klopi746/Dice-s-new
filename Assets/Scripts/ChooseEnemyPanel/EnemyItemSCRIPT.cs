using Enemy.AI;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class EnemyItemSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canFightToggle.isOn) return;
        ChooseEnemyPanelSCRIPT.Instance.DisableAll();
        image.color = Color.red;
    }


    [SerializeField] Image image;
    public Toggle canFightToggle;

    private EnemyData enemyData; // for JSON parse
    public void LoadEnemyData(int enemyId)
    {
        var enemySetUpFile = Resources.Load<TextAsset>($"Enemy/SetUp{enemyId}");
        if (enemySetUpFile == null) { Debug.LogError($"Failed to load EnemySetUp for ID: {enemyId}"); return; }

        try
        {
            enemyData = JsonConvert.DeserializeObject<EnemyData>(enemySetUpFile.text);

            Debug.Log($"Loaded enemy: {enemyData.enemyName}");
            AssignData(enemyData);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to parse JSON: {e.Message}"); // Maybe we need here to load default or Load MainMenu to avoid errors - MOMMY
        }
    }
    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] TextMeshProUGUI enemyDesc;
    private void AssignData(EnemyData data)
    {
        enemyName.text = enemyData.enemyName;
        enemyDesc.text = enemyData.description;
    }
}
