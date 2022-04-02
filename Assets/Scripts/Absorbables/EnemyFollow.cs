using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    private Vector2 maxDistMove;
    [SerializeField]
    private float distThreshhold;
    [SerializeField]
    private float enemyMoveSpeed;
    [SerializeField]
    private float movementTime;
    [SerializeField]
    private float pauseDuration;
    [SerializeField]
    private float XThreshhold;

    private Animator enemyAnimator;
    private Coroutine moveCoroutine;
    private Transform player;
    private Movement currentMove;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        currentMove = Movement.Down;
        enemyAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
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

    /// <summary>
    /// Move in random direction if the player can't be seen
    /// </summary>
    private void MoveRandom()
    {
        if (!isMoving)
        {
            isMoving = true;
            moveCoroutine = StartCoroutine(Move(new Vector2(Random.Range(-1, 2),
                                                            Random.Range(-1, 2))));
        }
    }


    /// <summary>
    /// Move the player in a direction
    /// </summary>
    /// <param name="dirMask">Mask the direction the player moves</param>
    /// <returns></returns>
    private IEnumerator Move(Vector2 dirMask)
    {
        StopAnimation();
        SetAnimation(dirMask);
        // Handle the animation direction

        Vector3 startPos = transform.position;
        Vector3 randomLocation = new Vector3(
             Random.Range(0, maxDistMove.x) * dirMask.x ,
             Random.Range(0, maxDistMove.y) * dirMask.y,
             0);

        Vector3 endPos = randomLocation + startPos;
        float elapseTime = 0f;

        // Allow enemy to move over time
        while(elapseTime < movementTime)
        {
            elapseTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, enemyMoveSpeed * elapseTime / movementTime);
            yield return null;
        }

        // Pause the animation since there won't be anymore movement
        StopAnimation();

        // Wait in order to create a realistic movement
        yield return new WaitForSeconds(pauseDuration);

        isMoving = false;
    }

    public void SetAnimation(Vector2 dir)
    {
        // Handle the animation direction
        if (dir.x > XThreshhold)
        {
            enemyAnimator.SetBool("RightMove_b", true);
            currentMove = Movement.Right;
        }
        else if (dir.x < -XThreshhold)
        {
            enemyAnimator.SetBool("LeftMove_b", true);
            currentMove = Movement.Left;
        }
        else
        {
            if (dir.y > 0.0f)
            {
                enemyAnimator.SetBool("UpMove_b", true);
                currentMove = Movement.Up;
            }
            else if (dir.y < 0.0f)
            {
                enemyAnimator.SetBool("DownMove_b", true);
                currentMove = Movement.Down;
            }
        }
    }

    /// <summary>
    /// Stop the animation by setting it to false
    /// </summary>
    void StopAnimation()
    {
        switch (currentMove)
        {
            case Movement.Up:
                enemyAnimator.SetBool("UpMove_b", false);
                break;
            case Movement.Down:
                enemyAnimator.SetBool("DownMove_b", false);
                break;
            case Movement.Right:
                enemyAnimator.SetBool("RightMove_b", false);
                break;
            case Movement.Left:
                enemyAnimator.SetBool("LeftMove_b", false);
                break;
        }
    }

    enum Movement
    {
        Up,
        Down,
        Left,
        Right
    }
}