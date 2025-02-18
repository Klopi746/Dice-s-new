using UnityEngine;
using UnityEngine.Events;

public class GameHandlerSCRIPT : MonoBehaviour
{
    public static GameHandlerSCRIPT Instance;
    private void Awake()
    {
        Instance = this;
    }


    private bool _isPlayerTurn = true;
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


    private bool _isGameStarted = false;
    public void StartTheGame()
    {
        if (!_isGameStarted) _isGameStarted = true;
    }
    public bool isGameStarted()
    {
        return _isGameStarted;
    }
}
