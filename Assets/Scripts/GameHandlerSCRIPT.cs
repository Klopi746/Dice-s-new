using UnityEngine;

public class GameHandlerSCRIPT : MonoBehaviour
{
    public static GameHandlerSCRIPT Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsPlayerTurn { get; set; } = true;
}
