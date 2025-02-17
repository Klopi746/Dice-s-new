using System.Collections.Generic;

/// <summary>
/// Contains EnemyData to load from JSON.
/// </summary>
/// <remarks>
/// Variable names need to be like in a JSON!!!
/// </remarks>
public class EnemyData
{
    public string enemyName;
    public Dictionary<string, int> ownedCubes = new Dictionary<string, int>();
    public string description;
}