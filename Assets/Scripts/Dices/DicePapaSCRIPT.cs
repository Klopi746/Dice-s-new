using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DicePapaSCRIPT : MonoBehaviour
{
    protected static readonly Vector3[] _numberRotations = {
        Vector3.zero, // 1
        new Vector3(-90, 0, 0), // 2
        new Vector3(90, 0, 0), // 3
        new Vector3(0, 0, 90), // 4
        new Vector3(180, 0, 0), // 5
        new Vector3(0, 0, -90) // 6
    };


    [Header("Face Probabilities (1-6)")]
    [Tooltip("Probabilities should sum to 1")]
    [SerializeField]
    protected float[] _faceProbabilities = new float[6] {
        0.5f, 0f, 0f, 0f, 0f, 0.5f // 50% for 1, 50% for 6
    };


    // Validate and normalize probabilities in editor
    private void OnValidate()
    {
        NormalizeProbabilities();
    }
    private void NormalizeProbabilities()
    {
        float total = 0f;
        foreach (float prob in _faceProbabilities)
        {
            total += prob;
        }

        // Avoid division by zero
        if (total <= Mathf.Epsilon) return;

        // Normalize probabilities
        for (int i = 0; i < _faceProbabilities.Length; i++)
        {
            _faceProbabilities[i] /= total;
        }
    }
#if UNITY_EDITOR
    [ContextMenu("Test Roll")]
    public void TestRoll()
    {
        Roll();
        Debug.Log($"Rolled: {CurrentNumber}");
        Debug.Log($"Current probabilities: {string.Join(", ", _faceProbabilities)}");
    }

    [ContextMenu("Print Probability Sum")]
    public void PrintProbabilitySum()
    {
        float sum = 0f;
        foreach (float p in _faceProbabilities) sum += p;
        Debug.Log($"Probability sum: {sum}");
    }
#endif


    public int CurrentNumber { get; protected set; } = 1;
    public virtual void Roll()
    {
        CurrentNumber = Random.Range(1, 7);
        transform.localRotation = Quaternion.Euler(_numberRotations[CurrentNumber - 1]);
        gameObject.SetActive(true);
    }
    public void ResetDice()
    {
        CurrentNumber = 1;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
    }


    private void Start()
    {
        ResetDice();
    }
}
