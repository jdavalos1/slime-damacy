using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Force when colliding with the enemy
    /// </summary>
    public Vector2 enemyForce;

    /// <summary>
    /// Max distance to move the enemy
    /// </summary>
    [SerializeField]
    private Vector2 maxDistMove;
   
    /// <summary>
    /// Distance threshhold to follow player
    /// </summary>
    [SerializeField]
    protected float distThreshhold;

    /// <summary>
    /// Max time to move from to a random position
    /// </summary>
    [SerializeField]
    protected float movementTime;

    /// <summary>
    /// Duration of wait between movement
    /// </summary>
    [SerializeField]
    private float pauseDuration;

    /// <summary>
    /// Threshhold before animation changes to 
    /// </summary>
    [SerializeField]
    private float XThreshhold;

    /// <summary>
    /// Speed at which the enemy follows the player
    /// </summary>
    [SerializeField]
    protected float enemyMoveSpeed;

    protected Animator enemyAnimator;
    protected Coroutine moveCoroutine;
    protected Transform player;
    private Movement currentMove;
    protected bool isMoving;
    protected float elapsedTime;

    public SpawnManager spawnOwner;
    
    public float EnemyMoveSpeed { get {return enemyMoveSpeed; } }
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        currentMove = Movement.Down;
        enemyAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
    }

    /// <summary>
    /// Stop the animation by setting it to false
    /// </summary>
    protected void StopAnimation()
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

    /// <summary>
    /// Sets the animation of the enemy as it follows the player
    /// </summary>
    /// <param name="dir"></param>
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
    /// Move in random direction if the player can't be seen
    /// </summary>
    protected void MoveRandom()
    {
        Vector3 min = GameManager.SharedInstance.minBoundaries;
        Vector3 max = GameManager.SharedInstance.maxBoundaries;
        if (transform.position.x <= min.x ||
            transform.position.y <= min.y ||
            transform.position.x >= max.x ||
            transform.position.y >= max.y)
        {
            transform.gameObject.SetActive(false);
            spawnOwner.currentlyActive--;
            return;
        }
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
    protected IEnumerator Move(Vector2 dirMask)
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
        elapsedTime = 0.0f;

        // Allow enemy to move over time
        while(elapsedTime < movementTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, enemyMoveSpeed * elapsedTime / movementTime);
            yield return null;
        }

        // Pause the animation since there won't be anymore movement
        StopAnimation();

        // Wait in order to create a realistic movement
        yield return new WaitForSeconds(pauseDuration);

        isMoving = false;
    }

    /// <summary>
    /// Removes the spawn from the game
    /// </summary>
    public void RemoveSpawn()
    {
        transform.gameObject.SetActive(false);
        spawnOwner.currentlyActive--;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ItemAttributes iA = gameObject.GetComponent<ItemAttributes>();
        if (collision.gameObject.CompareTag("Item"))
        {

            elapsedTime = movementTime;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (FindObjectOfType<Player>().CanSwallowItem(iA))
            {
                RemoveSpawn();
            }
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
