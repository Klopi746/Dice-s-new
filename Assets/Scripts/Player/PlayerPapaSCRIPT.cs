using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Store params that each player have.
/// </summary>
public abstract class PlayerPapaSCRIPT : MonoBehaviour
{
    public string playerName = "SusIsNumberOne";
    public Dictionary<string, int> ownedCubes = new Dictionary<string, int>(); // <cubeId, how much cube of this Id>
    [SerializeField] protected Transform playerCubesInWorld; // each player has it's own cubes
}
