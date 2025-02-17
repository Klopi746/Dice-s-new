using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerPapaSCRIPT : MonoBehaviour
{
    public string playerName = "SusIsNumberOne";
    public Dictionary<string,int> ownedCubes = new Dictionary<string, int>();
    [SerializeField] protected Transform playerCubesInWorld;
}
