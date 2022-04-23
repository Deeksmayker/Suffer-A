using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemy
{

    // Start is called before the first frame update
    public int GenerateStandingMob(float distanceToPlayer, float agroDistance, float playerPosition, float enemyPosition, float distanceStopMove)
    {
        int motionController = 0;
        if (distanceToPlayer < agroDistance )
        {
            motionController = StartHunting(motionController, playerPosition, enemyPosition, distanceStopMove);
        }

        return motionController;
    }

    int StartHunting(int motionController, float playerPosition, float enemyPosition, float distanceStopMove)
    {
        if (playerPosition + distanceStopMove < enemyPosition)
        {
            motionController = 1;
        }
        else if (playerPosition - distanceStopMove > enemyPosition)
        {
            motionController = -1;
        }

        return motionController;
    }
}
