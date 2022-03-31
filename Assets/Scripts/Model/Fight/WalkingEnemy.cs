using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy
{
    // Start is called before the first frame update
    public int GenerateWalkingMob(float distanceToPlayer, float agroDistance, float playerPosition, float enemyPosition, float distanceStopMove, float startPointWalk, float endPointWalk, int motionController)
    {
        if (distanceToPlayer < agroDistance)
        {
            motionController = StartHunting(motionController, playerPosition, enemyPosition, distanceStopMove);
        }
        else
        {
            motionController = EnemyWalk(enemyPosition, motionController, startPointWalk, endPointWalk);
        }

        return motionController;
    }

    private int EnemyWalk(float enemyPosition, int motionController, float startPointWalk, float endPointWalk)
    {
        if (enemyPosition > startPointWalk)
        {
            motionController = -1;
        }
        else if (enemyPosition < endPointWalk)
        {
            motionController = 1;
        }

        return motionController;
    }

    int StartHunting(int motionController, float playerPosition, float enemyPosition, float distanceStopMove)
    {
        if (playerPosition + distanceStopMove < enemyPosition)
        {
            motionController = -1;
        }
        else if (playerPosition - distanceStopMove > enemyPosition)
        {
            motionController = 1;
        }
        else motionController = 0;

        return motionController;
    }
}
