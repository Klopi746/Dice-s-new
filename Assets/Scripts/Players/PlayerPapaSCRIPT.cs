using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    protected DicePapaSCRIPT[] cubesScripts = new DicePapaSCRIPT[6];
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
            GameObject gameObject = playerCubesInWorld.GetChild(i).gameObject;
            cubesArray[i] = gameObject;
            cubesScripts[i] = gameObject.GetComponent<DicePapaSCRIPT>();
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


    [SerializeField] Transform cubeMixerTransform;
    protected IEnumerator GetAndDropCubes()
    {
        ResetAllCubes(); // hide cubes

        // bottle animation realization
        cubeMixerTransform.gameObject.SetActive(true);
        yield return StartCoroutine(BottleMixerAnimation());

        RollAllCubes(); // show cubes
        cubeMixerTransform.gameObject.SetActive(false);
        yield return null;
    }
    private IEnumerator BottleMixerAnimation()
    {
        Tween tween = cubeMixerTransform.DOLocalMove(new Vector3(0f,0f,-15f), 0.5f);
        yield return tween.WaitForCompletion();
        tween = cubeMixerTransform.DOShakePosition(2f, strength: new Vector3(0, 0, 2f), vibrato: 5, randomness: 10, fadeOut: false, randomnessMode: ShakeRandomnessMode.Harmonic);
        yield return tween.WaitForCompletion();
        tween = cubeMixerTransform.DOLocalMove(new Vector3(0f,0f,-15f), 0.1f);
        yield return tween.WaitForCompletion();
        tween = cubeMixerTransform.DOLocalMove(new Vector3(0,0,0), 0.5f);
        yield return tween.WaitForCompletion();
        yield return null;
    }
    private void ResetAllCubes()
    {
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            script.ResetDice();
        }
    }
    private void RollAllCubes()
    {
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            script.Roll();
        }
    }
}
