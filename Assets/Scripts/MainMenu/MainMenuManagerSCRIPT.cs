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


    public int Lives = 80;
    public int RealLives = 10;
    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("ChoosedBet", 10);
        Lives = PlayerPrefs.GetInt("Lives", 80);
        LivesTextPro.text = $"Money: {Lives}";

        RealLives = PlayerPrefs.GetInt("RealLives", 10);
    }
    public void UpdateLivesTo(int newValue)
    {
        Lives = newValue;
        LivesTextPro.text = $"Money: {Lives}";
    }


    public void ShowErrorOnLives()
    {
        LivesTextPro.transform.DOPunchScale(new Vector3(1, 1, 0), 1f, 4);
    }


    public int RealWin = 0;
    [SerializeField] private Button InstantMoneyButt;
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
    private void DoOnRealWin()
    {
        InstantMoneyButt.gameObject.SetActive(true);
    }
    private IEnumerator LooseGame()
    {
        PlayerPrefs.DeleteAll();
        GeneralSoundManagerSCRIPT.Instance.PlayMusicWithDelay(1f);
        LivesTextPro.text = "ПОРАЖЕНИЕ";
        LivesTextPro.color = Color.red;
        Tween tween = LivesTextPro.transform.DOJump(Vector2.zero,100,10,5f);
        yield return tween.WaitForCompletion();
        LoadSceneManagerSCRIPT.Instance.LoadNewScene();
    }
}
