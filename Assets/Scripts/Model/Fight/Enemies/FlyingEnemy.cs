using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy
{
    // Start is called before the first frame update
    public int GenerateFlyingMob(float enemyPosition, float startPointWalk, float endPointWalk, int motionController)
    {
        motionController = EnemyFlying(enemyPosition, motionController, startPointWalk, endPointWalk);
        return motionController;
    }

    private int EnemyFlying(float enemyPosition, int motionController, float startPointWalk, float endPointWalk)
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
}
