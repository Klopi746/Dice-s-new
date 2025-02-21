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
        float randomPoint = Random.value;
        float cumulative = 0f;

        // Normalize probabilities first
        NormalizeProbabilities();

        for(int i = 0; i < _faceProbabilities.Length; i++)
        {
            cumulative += _faceProbabilities[i];
            if(randomPoint <= cumulative)
            {
                return i + 1;
            }
        }

        return 6;
    }
}
