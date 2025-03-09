using System;
using System.Collections.Generic;


namespace Enemy.AI
{
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
        public string aiType;
        public string spritePath;
    }


    public class AiLogicDictionary
    {
        public static Dictionary<string, Type> AI_TypeMap = new Dictionary<string, Type>()
        {
            {"Stupid", typeof(AIStupidLogicSCRIPT)},
            {"Clever", typeof(AICleverLogicSCRIPT)},
            {"Normal", typeof(AINormalLogicSCRIPT)},
        };
    }
}
