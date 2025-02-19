using UnityEngine;

public class DicePapaSCRIPT : MonoBehaviour
{
    private static readonly Vector3[] _numberRotations = {
        Vector3.zero, // 1
        new Vector3(-90, 0, 0), // 2
        new Vector3(90, 0, 0), // 3
        new Vector3(0, 0, 90), // 4
        new Vector3(180, 0, 0), // 5
        new Vector3(0, 0, -90) // 6
    };


    [Header("Вероятности выпадения граней (1-6)")]
    [Range(0f, 1f)] 
    public float[] faceProbabilities = new float[6] { 
        0.1666f, // 1
        0.1666f, // 2
        0.1666f, // 3
        0.1666f, // 4
        0.1666f, // 5
        0.1666f  // 6
    };


    public int CurrentNumber { get; private set; } = 1;
    public virtual void Roll()
    {
        CurrentNumber = Random.Range(1, 7);
        transform.localRotation = Quaternion.Euler(_numberRotations[CurrentNumber - 1]);
    }
    public void ResetDice()
    {
        CurrentNumber = 1;
        transform.localRotation = Quaternion.identity;
    }


    private void Awake()
    {
        ResetDice();
        Roll();
        Debug.Log($"{this.name}: rolled {CurrentNumber}");
    }
}