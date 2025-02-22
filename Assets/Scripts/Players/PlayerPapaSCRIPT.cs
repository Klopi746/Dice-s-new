using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    protected List<string> curCombos;
    protected IEnumerator GetAndDropCubes()
    {
        ResetAllCubes(); // hide cubes

        // bottle animation realization
        cubeMixerTransform.gameObject.SetActive(true);
        yield return StartCoroutine(BottleMixerAnimation());

        RollAllCubes(); // show cubes
        yield return new WaitForSeconds(0.1f);
        cubeMixerTransform.gameObject.SetActive(false);

        // curCombos = FindAllCombos(cubesScripts);
        // curCombos.ForEach(combo => Debug.Log(combo));

        if (GameHandlerSCRIPT.Instance.IsPlayerTurn) EndTurnButtSCRIPT.Instance.ChangeButtInteractable(true);
        yield return null;
    }
    protected IEnumerator BottleMixerAnimation()
    {
        Tween tween = cubeMixerTransform.DOLocalMove(new Vector3(0f, 0f, -15f), 0.5f);
        yield return tween.WaitForCompletion();
        tween = cubeMixerTransform.DOShakePosition(2f, strength: new Vector3(0, 0, 2f), vibrato: 5, randomness: 10, fadeOut: false, randomnessMode: ShakeRandomnessMode.Harmonic);
        yield return tween.WaitForCompletion();
        tween = cubeMixerTransform.DOLocalMove(new Vector3(0f, 0f, -15f), 0.1f);
        yield return tween.WaitForCompletion();
        tween = cubeMixerTransform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
        yield return tween.WaitForCompletion();
        yield return null;
    }
    protected void ResetAllCubes()
    {
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            script.ResetDice();
        }
    }
    protected void RollAllCubes()
    {
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            script.Roll();
        }
    }


    protected static readonly Dictionary<string, int> _cubesCombos = new Dictionary<string, int>(){
        {"1",100},
        {"5",50},
        {"111",1000},
        {"1111",2000},
        {"11111",4000},
        {"111111",8000},
        {"222",200},
        {"2222",400},
        {"22222",800},
        {"222222",1600},
        {"333",300},
        {"3333",600},
        {"33333",1200},
        {"333333",2400},
        {"444",400},
        {"4444",800},
        {"44444",1600},
        {"444444",3200},
        {"555",500},
        {"5555",1000},
        {"55555",2000},
        {"555555",4000},
        {"666",600},
        {"6666",1200},
        {"66666",2400},
        {"666666",4800},
        {"12345",500},
        {"23456",750},
        {"123456",1500}
    };
    // public List<string> FindAllCombos(DicePapaSCRIPT[] cubesScripts)
    // {
    //     List<int> rolledDice = new();
    //     foreach (DicePapaSCRIPT script in cubesScripts)
    //     {
    //         rolledDice.Add(script.CurrentNumber);
    //     }
    //     rolledDice.Sort();
    //     string rolledDiceString = rolledDice.ToString();

    //     List<string> foundCombos = new List<string>();

    //     return foundCombos;
    // }

    public Dictionary<string, int> FindAllCombos(Dictionary<int, int> diceValues)
    {
        List<int> rolledDice = new();
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            rolledDice.Add(script.CurrentNumber);
        }
        rolledDice.Sort();
        string rolledDiceString = rolledDice.ToString();

        Dictionary<string, int> foundCombos = new Dictionary<string, int>(){
            { "5", 50 }, { "1", 100 }, { "15", 150 }
        };

        return foundCombos;
    }
}
