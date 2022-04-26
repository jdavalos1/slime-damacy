using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRun : Enemy 
{

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.SharedInstance.isGameOver && Vector3.Distance(player.position, transform.position) <= distThreshhold)
        {
            RunFromPlayer();
        }
        else
        {
            MoveRandom();
        }
 
    }

    /// <summary>
    /// Moves away from the player.
    /// </summary>
    private void RunFromPlayer()
    {
        if (isMoving)
        {
            StopCoroutine(moveCoroutine);
            isMoving = false;
        }

        StopAnimation();
        Vector3 moveTowards = Vector3.MoveTowards(
                                        transform.position,
                                        player.position,
                                        -enemyMoveSpeed * Time.deltaTime);
        transform.position = new Vector3(moveTowards.x,
                                         moveTowards.y,
                                         player.position.x);
        SetAnimation(-(player.position - transform.position).normalized);
    }
}
