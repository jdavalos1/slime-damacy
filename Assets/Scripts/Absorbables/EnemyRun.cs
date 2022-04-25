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

    private void RunFromPlayer()
    {
        if (isMoving)
        {
            StopCoroutine(moveCoroutine);
            isMoving = false;
        }

        StopAnimation();
        transform.position = -Vector3.MoveTowards(transform.position,
            player.position, enemyMoveSpeed * Time.deltaTime);
        SetAnimation(-(player.position - transform.position).normalized);
    }
}
