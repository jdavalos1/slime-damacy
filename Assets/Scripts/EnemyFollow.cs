using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
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

    private Animator enemyAnimator;
    private Coroutine moveCoroutine;
    private Movement currentMove;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        currentMove = Movement.Down;
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= distThreshhold)
        {
            StopCoroutine(moveCoroutine);
            StopAnimation();
            Debug.Log("Grr");
        }
        else
        {
            MoveRandom();
        }
    }

    private void MoveRandom()
    {
        if (!isMoving)
        {
            isMoving = true;
            moveCoroutine = StartCoroutine(Move(new Vector2(Random.Range(-1, 2),
                                                            Random.Range(-1, 2))));
        }
    }

    private void MoveTowards()
    {
    }

    /// <summary>
    /// Move the player in a direction
    /// </summary>
    /// <param name="dirMask">Mask the direction the player moves</param>
    /// <returns></returns>
    private IEnumerator Move(Vector2 dirMask)
    {
        StopAnimation();
        // Handle the animation direction
        if (dirMask.x > 0.0f)
        {
            enemyAnimator.SetBool("RightMove_b", true);
            currentMove = Movement.Right;
        }
        else if (dirMask.x < 0.0f)
        {
            enemyAnimator.SetBool("LeftMove_b", true);
            currentMove = Movement.Left;
        }
        else
        {
            if (dirMask.y > 0.0f)
            {
                enemyAnimator.SetBool("UpMove_b", true);
                currentMove = Movement.Up;
            }
            else if (dirMask.y < 0.0f)
            {
                enemyAnimator.SetBool("DownMove_b", true);
                currentMove = Movement.Down;
            }
        }

        Vector3 startPos = transform.position;
        Vector3 randomLocation = new Vector3(
             Random.Range(0, maxDistMove.x) * dirMask.x ,
             Random.Range(0, maxDistMove.y) * dirMask.y,
             0);

        Vector3 endPos = randomLocation + startPos;
        float elapseTime = 0f;
        Debug.Log(endPos);

        while(elapseTime < movementTime)
        {
            elapseTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, enemyMoveSpeed * elapseTime / movementTime);
            yield return null;
        }

        StopAnimation();
        yield return new WaitForSeconds(pauseDuration);

        isMoving = false;
    }

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
