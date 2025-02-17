using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerPapaSCRIPT : MonoBehaviour
{
    public string playerName = "SusIsNumberOne";
    public int playerMoney = 50;
    public Dictionary<int,int> ownedCubes = new Dictionary<int, int>();
    [SerializeField] protected Transform playerCubesInWorld;
}
