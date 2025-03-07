using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameHandlerSCRIPT : MonoBehaviour
{
    public static GameHandlerSCRIPT Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        curBet = PlayerPrefs.GetInt("ChoosedBet", 10);
        SetGoalFromBet(curBet);
    }
    private void Start()
    {
        GeneralSoundManagerSCRIPT.Instance.PlayMusicWithDelay(1f);
    }


    private bool _isPlayerTurn = false;
    public UnityEvent<bool> OnTurnChanged;
    public bool IsPlayerTurn
    {
        get => _isPlayerTurn;
        set
        {
            if (_isPlayerTurn != value)
            {
                _isPlayerTurn = value;
                OnTurnChanged.Invoke(_isPlayerTurn);
            }
        }
    }
    public void EndTurn()
    {
        IsPlayerTurn = !IsPlayerTurn;
    }


    public TextMeshProUGUI goalTextPro;
    public int curBet = 10;
    public int goalScore = 1500;
    private void SetGoalFromBet(int betValue)
    {
        switch (betValue)
        {
            case 10:
                goalScore = 1500;
                break;
            case 50:
                goalScore = 2000;
                break;
            case 100:
                goalScore = 3000;
                break;
            case 300:
                goalScore = 4500;
                break;
            default:
                goalScore = 1500;
                break;
        }
        goalTextPro.text = goalScore.ToString();
    }


    [SerializeField] TextMeshProUGUI winTextPro;
    private bool someoneWinned = false;
    public void CheckForWin(int score, MonoBehaviour caller)
    {
        if (score >= goalScore && someoneWinned == false)
        {
            someoneWinned = true;

            string winnerName = (caller is PlayerSCRIPT) ? "Player" : EnemySCRIPT.Instance.playerName;
            winTextPro.text = $"В тяжелой борьбе победил {winnerName}";
            winTextPro.gameObject.SetActive(true);

            AudioManager_SCRIPT.Instance.StopAllLoopingSounds();

            int playerLives = PlayerPrefs.GetInt("Lives");
            if (winnerName == "Player") { PlayerPrefs.SetInt("Lives", playerLives + curBet); GeneralSoundManagerSCRIPT.Instance.PlayVictorySound(); OpenNewEnemy(EnemySCRIPT.Instance.enemyId); }
            else { PlayerPrefs.SetInt("Lives", playerLives - curBet); GeneralSoundManagerSCRIPT.Instance.PlayDefeatSound(); DecreaseRealLives(); }

            StartCoroutine(LoadMainMenu());
        }
    }
    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(3f);
        AudioManager_SCRIPT.Instance.StopAllCoroutines();
        LoadSceneManagerSCRIPT.Instance.LoadNewScene(0);
    }
    private void OpenNewEnemy(int value)
    {
        int curOpenEnemies = PlayerPrefs.GetInt("EnemiesOpen", 1);
        if (curOpenEnemies == value)
        {
            PlayerPrefs.SetInt("EnemiesOpen", curOpenEnemies + 1);
        }
        if (curOpenEnemies + 1 == 6) PlayerPrefs.SetInt("RealWin", 1);
    }
    private void DecreaseRealLives()
    {
        int curRealLives = PlayerPrefs.GetInt("RealLives", 10);
        PlayerPrefs.SetInt("RealLives", curRealLives - 1);
        if (curRealLives - 1 == 0) PlayerPrefs.SetInt("RealWin", -1);
    }


    // UnUsed for now
    private bool _isGameStarted = false;
    public void StartTheGame()
    {
        if (!_isGameStarted) _isGameStarted = true;
        CameraControllerSCRIPT.Instance.actionOverride = false;
    }
    public bool isGameStarted()
    {
        return _isGameStarted;
    }


    private void OnDestroy()
    {
        if (Instance != null)
        {
            Instance.OnTurnChanged.RemoveAllListeners();
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            goalScore = 0;
        }
    }
#endif
}
