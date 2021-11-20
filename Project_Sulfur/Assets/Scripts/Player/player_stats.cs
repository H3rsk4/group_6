using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_stats : stats
{
    public override void Death()
    {
        isDead = true;
        //Debug.Log("player died, OOF!");
    }
}
