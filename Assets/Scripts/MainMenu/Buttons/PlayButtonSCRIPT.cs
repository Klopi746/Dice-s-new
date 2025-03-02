using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSCRIPT : MonoBehaviour
{
    [SerializeField] GameObject chooseEnemyPanel;


    private Button button;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }


    public void OnButtonClicked()
    {
        chooseEnemyPanel.SetActive(true);
    }
}
