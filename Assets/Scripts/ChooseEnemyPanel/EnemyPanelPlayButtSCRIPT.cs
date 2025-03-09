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
        if (!RealLivesManager.Instance.LivesShown) return;

        GeneralSoundManagerSCRIPT.Instance.PlayButtSound();
        AudioManager_SCRIPT.Instance.StopAllLoopingSounds();

        ChooseEnemyPanelSCRIPT.Instance.FindEnemyAndSaveIt();

        PlayerPrefs.SetInt("Lives", MainMenuManagerSCRIPT.Instance.Lives);

        LoadSceneManagerSCRIPT.Instance.LoadNewScene(1);
    }
}
