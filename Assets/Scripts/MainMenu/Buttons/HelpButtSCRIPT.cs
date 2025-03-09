using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HelpButtSCRIPT : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI helpPanel;


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
        bool activate = !helpPanel.enabled;
        helpPanel.enabled = activate;
    }
}