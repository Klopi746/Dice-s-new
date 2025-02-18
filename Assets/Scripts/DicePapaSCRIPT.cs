using UnityEngine;

public class DicePapaSCRIPT : MonoBehaviour
{
    private static readonly Vector3[] _numberRotations = {
        Vector3.zero,
        new Vector3(0, 180, 0),
        new Vector3(90, 0, 0),
        new Vector3(-90, 0, 0),
        new Vector3(0, -90, 0),
        new Vector3(0, 90, 0)
    };

    public int CurrentNumber { get; private set; } = 1;

    public void Roll()
    {
        CurrentNumber = Random.Range(1, 7);
        transform.localRotation = Quaternion.Euler(_numberRotations[CurrentNumber - 1]);
    }

    public void ResetDice()
    {
        CurrentNumber = 1;
        transform.localRotation = Quaternion.identity;
    }
}