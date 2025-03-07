using UnityEngine;

[CreateAssetMenu(fileName = "DicePrefabData", menuName = "Custom/DicePrefabData", order = 1)]
public class DicePrefabData : ScriptableObject
{
    [Header("Game info")]
    public int[] _faceProbabilities = new int[6] {
        50, 0, 0, 0, 0, 50 // 50% for 1, 50% for 6
    };

    [Header("material info")]
    public Material diceMat;
}
