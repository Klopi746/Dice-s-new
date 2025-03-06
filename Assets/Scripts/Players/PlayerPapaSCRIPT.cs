using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


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
    public DicePapaSCRIPT[] cubesScripts = new DicePapaSCRIPT[6];
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

            int diceId = PlayerPrefs.GetInt("DiceInSlot" + i, 0);
            DicePrefabData data = Resources.Load<DicePrefabData>($"Dice/DicePrefab{diceId}");
            gameObject.GetComponent<MeshRenderer>().material = data.diceMat;

            cubesArray[i] = gameObject;
            cubesScripts[i] = gameObject.GetComponent<DicePapaSCRIPT>();

            cubesScripts[i]._faceProbabilities = data._faceProbabilities;
        }
        Debug.Log($"Cubes for {gameObject.name} succesfully initialized");
    }


    public TextMeshProUGUI temporaryScoreText;
    public TextMeshProUGUI scoreText;
    public Transform noComboTextObj;


    /// <summary>
    /// Do PaPa's things, that's the same for each player
    /// </summary>
    protected virtual void Awake()
    {
        AssignOwnedCubesToArray();
    }


    [SerializeField] Transform cubeMixerTransform;
    public Dictionary<string, int> curCombos;
    public bool isFindCombo = false;
    protected IEnumerator GetAndDropCubes()
    {
        if (noComboTextObj.gameObject.activeSelf) noComboTextObj.gameObject.SetActive(false);
        TurnOffCubesOutline();
        ResetAllCubes(); // hide cubes

        // bottle animation realization
        cubeMixerTransform.gameObject.SetActive(true);
        yield return StartCoroutine(BottleMixerAnimation());

        RollAllCubes(); // show cubes
        yield return new WaitForSeconds(0.1f);
        isFindCombo = FindCombos();
        cubeMixerTransform.gameObject.SetActive(false);


        if (GameHandlerSCRIPT.Instance.IsPlayerTurn && isFindCombo)
        {
            EndTurnButtSCRIPT.Instance.ChangeButtInteractable(true);
            PassButtSCRIPT.Instance.ChangeButtInteractable(true);
            CameraControllerSCRIPT.Instance.SetCloseCamView();
        }
        else if (GameHandlerSCRIPT.Instance.IsPlayerTurn)
        {
            Debug.Log("NO COMBO! :(");
            temporaryScoreText.text = "0";
            noComboTextObj.gameObject.SetActive(true);
            EndTurnButtSCRIPT.Instance.ChangeButtInteractable(true);
            CameraControllerSCRIPT.Instance.SetCloseCamView();
        }
        // Change Cam to See enemy here
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
            if (script.enabled) script.ResetDice();
        }
    }
    protected void RollAllCubes()
    {
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            if (script.enabled) script.Roll();
        }
    }
    protected void TurnOffCubesOutline()
    {
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            script.TurnOffDiceOutline();
        }
    }
    protected bool FindCombos()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>();
        for (int i = 0; i < cubesScripts.Length; i++)
        {
            if (cubesScripts[i].enabled) diceValues.Add(i + 1, cubesScripts[i].CurrentNumber);
        }
        curCombos = ComboFinder.Instance.FindAllCombos(diceValues);
        foreach (var combo in curCombos)
        {
            Debug.Log($"combo: {combo.Key}; value: {combo.Value}");
        }

        if (curCombos.Count == 0) return false;
        else return true;
    }


    public virtual void ContinuePlay() // throw cubes again
    {
        EndTurnButtSCRIPT.Instance.ChangeButtInteractable(false);
        TurnOffCubesOutline();
        DisableCubeOnContinueIfClicked();
        CheckThatThereIsNoMoreCubes();
        StartCoroutine(GetAndDropCubes());
    }
    protected void DisableCubeOnContinueIfClicked()
    {
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            script.DisableCubeOnContinueIfClicked();
        }
    }
    protected void CheckThatThereIsNoMoreCubes()
    {
        bool thereIsNoMoreCubes = true;
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            if (script.enabled) thereIsNoMoreCubes = false;
        }
        if (thereIsNoMoreCubes) EnableAllCubes();
    }


    protected void EnableAllCubes()
    {
        foreach (DicePapaSCRIPT script in cubesScripts)
        {
            if (!script.enabled) script.EnableCube();
        }
    }
}
