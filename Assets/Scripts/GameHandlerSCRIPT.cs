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


    // UnUsed for now
    private bool _isGameStarted = false;
    public void StartTheGame()
    {
        if (!_isGameStarted) _isGameStarted = true;
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
}
