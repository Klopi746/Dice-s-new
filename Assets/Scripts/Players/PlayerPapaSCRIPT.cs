using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Store params that each player have.
/// </summary>
public abstract class PlayerPapaSCRIPT : MonoBehaviour
{
    public string playerName = "SusIsNumberOne";
    public Dictionary<string, int> ownedCubes = new Dictionary<string, int>(); // <cubeId, how much cube of this Id>
    /// <summary>
    /// Transform that contains all cubes.
    /// </summary>
    [SerializeField] protected Transform playerCubesInWorld; // each player has it's own cubes
    /// <summary>
    /// Store gameobjects of all own cubes.
    /// </summary>
    protected GameObject[] cubesArray = new GameObject[6];
    /// <summary>
    /// Must used to assign ownedCubesToArray to operate with each individually.
    /// </summary>
    /// <remarks>
    /// Use cubesArray to get each cube or playerCubesInWorld to get allCubesTransfrom
    /// </remarks>
    protected void AssignOwnedCubesToArray()
    {
        for (int i = 0; i < playerCubesInWorld.childCount; i++)
        {
            cubesArray[i] = playerCubesInWorld.GetChild(i).gameObject;
        }
        Debug.Log($"Cubes for {gameObject.name} succesfully initialized");
    }


    /// <summary>
    /// Do PaPa's things, that's the same for each player
    /// </summary>
    protected virtual void Awake()
    {
        AssignOwnedCubesToArray();
    }

    protected void GetAndDropCubes()
    {
        
    }
}
