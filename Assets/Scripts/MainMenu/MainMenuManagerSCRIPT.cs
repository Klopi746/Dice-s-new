using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManagerSCRIPT : MonoBehaviour
{
    public static MainMenuManagerSCRIPT Instance;


    [SerializeField] TextMeshProUGUI LivesTextPro;

    public int firstStart = 1;


    public int Lives = 290;
    public int RealLives = 10;
    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("ChoosedBet", 10);
        Lives = PlayerPrefs.GetInt("Lives", 190);
        LivesTextPro.text = $"Money: {Lives}";

        RealLives = PlayerPrefs.GetInt("RealLives", 10);

        firstStart = PlayerPrefs.GetInt("FirstStart", 1);
        if (firstStart == 1) PlayerPrefs.SetInt("FirstStart", 0);
    }
    public void UpdateLivesTo(int newValue)
    {
        Lives = newValue;
        LivesTextPro.text = $"Money: {Lives}";
        ShowErrorOnLives();
    }


    private Coroutine currentCoroutine;
    private Color originalLivesColor = Color.black;
    public void ShowErrorOnLives()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            LivesTextPro.transform.DOComplete();
            LivesTextPro.color = originalLivesColor;
        }
        currentCoroutine = StartCoroutine(ShowErrorOnLivesCoroutine());
    }

    private IEnumerator ShowErrorOnLivesCoroutine()
    {
        LivesTextPro.transform.DOComplete();
        LivesTextPro.color = Color.yellow;

        Tween tween = LivesTextPro.transform.DOPunchScale(new Vector3(1, 1, 0), 1f, 4);
        yield return tween.WaitForCompletion();

        LivesTextPro.color = originalLivesColor;
        currentCoroutine = null;
    }


    public int RealWin = 0;
    public void CheckWinLoose()
    {
        StartCoroutine(CheckWinLooseRoutine());
    }
    private IEnumerator CheckWinLooseRoutine()
    {
        yield return new WaitForSeconds(1f);
        RealWin = PlayerPrefs.GetInt("RealWin", 0);
        if (RealWin == 1) DoOnRealWin();
        else if (RealWin == -1) StartCoroutine(LooseGame());
    }

    [SerializeField] private Button InstantMoneyButt;
    private void DoOnRealWin()
    {
        InstantMoneyButt.gameObject.SetActive(true);
    }

    [SerializeField] GameObject LoosePanelObj;
    private IEnumerator LooseGame()
    {
        RealLivesManager.Instance.LivesShown = false;
        PlayerPrefs.DeleteAll();
        yield return null;
        LoosePanelObj.SetActive(true);
    }
}
