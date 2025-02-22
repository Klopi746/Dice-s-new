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

        curCombos = FindAllCombos(cubesScripts);
        curCombos.ForEach(combo => Debug.Log(combo));

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
    public List<string> FindAllCombos(DicePapaSCRIPT[] cubesScripts)
    {
        List<int> rolledDice = new();
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            rolledDice.Add(script.CurrentNumber);
        }

        // Count occurrences of each number
        Dictionary<int, int> diceCounts = new Dictionary<int, int>();
        foreach (int num in rolledDice)
        {
            if (diceCounts.ContainsKey(num))
                diceCounts[num]++;
            else
                diceCounts[num] = 1;
        }

        List<string> foundCombos = new List<string>();

        foreach (KeyValuePair<string, int> combo in _cubesCombos)
        {
            string key = combo.Key;
            // Check if all characters in the key are the same (count-based combo)
            bool isCountCombo = key.All(c => c == key[0]);
            if (isCountCombo)
            {
                int digit = int.Parse(key[0].ToString());
                int requiredCount = key.Length;
                if (diceCounts.TryGetValue(digit, out int count) && count >= requiredCount)
                {
                    foundCombos.Add(key);
                }
            }
            else
            {
                // Check if the key forms a consecutive sequence
                List<int> digits = key.Select(c => int.Parse(c.ToString())).ToList();
                bool isConsecutive = true;
                for (int i = 1; i < digits.Count; i++)
                {
                    if (digits[i] != digits[i - 1] + 1)
                    {
                        isConsecutive = false;
                        break;
                    }
                }
                if (isConsecutive)
                {
                    // Check if all required numbers are present
                    bool allPresent = true;
                    foreach (int d in digits)
                    {
                        if (!diceCounts.ContainsKey(d) || diceCounts[d] < 1)
                        {
                            allPresent = false;
                            break;
                        }
                    }
                    if (allPresent)
                    {
                        foundCombos.Add(key);
                    }
                }
            }
        }

        return foundCombos;
    }
}
