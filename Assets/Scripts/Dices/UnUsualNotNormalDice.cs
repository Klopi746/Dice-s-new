using UnityEngine;

public class UnUsualNotNormalDice : DicePapaSCRIPT
{
    public override void Roll()
    {
        CurrentNumber = GetWeightedRandomFace();

        transform.localRotation = Quaternion.Euler(_numberRotations[CurrentNumber - 1]);
        gameObject.SetActive(true);
    }
    private int GetWeightedRandomFace()
    {
        int cumulative = 0;
        for (int i = 0; i < _faceProbabilities.Length; i++)
        {
            cumulative += _faceProbabilities[i];
        }


        int randomPoint = Random.Range(0, cumulative);
        cumulative = 0;
        for (int i = 0; i < _faceProbabilities.Length; i++)
        {
            cumulative += _faceProbabilities[i];
            if (randomPoint < cumulative)
            {
                return i + 1;
            }
        }

        return 6; // if sum !=1, but < 1, so randomPoint > MAXcumulative
    }
}
