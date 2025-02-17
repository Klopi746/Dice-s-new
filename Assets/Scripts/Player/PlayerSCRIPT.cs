using UnityEngine;

public class PlayerSCRIPT : PlayerPapaSCRIPT
{
    public int playerMoney = 50;

    private void Awake()
    {
        AssignOwnedCubesToArray();
    }
}