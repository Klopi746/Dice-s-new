using UnityEngine;

public class GameHandlerSCRIPT : MonoBehaviour
{
    public static GameHandlerSCRIPT Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsPlayerTurn { get; set; } = true;

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
