using UnityEngine;
using UnityEngine.UI;

public class EnemyPanelPlayButtSCRIPT : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }


    public void OnButtonClicked()
    {
        ChooseEnemyPanelSCRIPT.Instance.FindEnemyAndSaveIt();
        LoadSceneManagerSCRIPT.Instance.LoadNewScene(1);
    }
}
