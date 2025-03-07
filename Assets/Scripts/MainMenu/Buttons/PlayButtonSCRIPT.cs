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
        if (!RealLivesManager.Instance.LivesShown) return;
        GeneralSoundManagerSCRIPT.Instance.PlayButtSound();
        bool activate = !chooseEnemyPanel.activeSelf;
        chooseEnemyPanel.SetActive(activate);
    }
}
