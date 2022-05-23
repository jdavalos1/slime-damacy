using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy 
{
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.SharedInstance.isGameOver)
        {
            if (Vector3.Distance(player.position, transform.position) <= distThreshhold)
            {
                FollowPlayer();
            }
            else
            {
                MoveRandom();
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// Follow the player if they are close enough
    /// </summary>
    private void FollowPlayer()
    {
        if (isMoving)
        {
            StopCoroutine(moveCoroutine);
            isMoving = false;
        }
        StopAnimation();
        transform.position = Vector3.MoveTowards(transform.position, player.position, enemyMoveSpeed * Time.deltaTime);
        // Just want a way to determine the direction of the animation
        SetAnimation((player.position - transform.position).normalized);
    }

    public Vector3 DirectionFacing()
    {
        Vector3 towardsPlayer = Vector3.MoveTowards(transform.position, player.position, enemyMoveSpeed * Time.deltaTime);
        return towardsPlayer.normalized;
    }

    enum Movement
    {
        Up,
        Down,
        Left,
        Right
    }
}
