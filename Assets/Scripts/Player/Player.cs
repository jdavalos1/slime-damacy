using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float slimeSpeed;
    private Animator playerAnim;
    MovementDirection currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        currentDirection = MovementDirection.Down;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.SharedInstance.isGameOver) Movement();
    }

    private void Movement()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(xMove, 0, 0);

        StopDirection();
        if (xMove > 0.0f)
        {
            playerAnim.SetBool("RightMove_b", true);
            currentDirection = MovementDirection.Right;
        }
        else if (xMove < 0.0f)
        {
            playerAnim.SetBool("LeftMove_b", true);
            currentDirection = MovementDirection.Left;
        }
        else
        {
            move = new Vector3(0, yMove, 0);
            if (yMove > 0.0f)
            {
                playerAnim.SetBool("UpMove_b", true);
                currentDirection = MovementDirection.Up;
            }
            else if(yMove < 0.0f)
            {
                playerAnim.SetBool("DownMove_b", true);
                currentDirection = MovementDirection.Down;
            }
        }

        transform.position += move * Time.deltaTime * slimeSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ItemAttributes iA = collision.gameObject.GetComponent<ItemAttributes>();
        if (collision.transform.CompareTag("Enemy"))
        {
            // Determine what gets happens when an enemy is of different size 
            if(iA.itemScale.x > transform.lossyScale.x)
            {
                //GameManager.SharedInstance.isGameOver = true;
                playerAnim.SetBool("IsEaten_b", true);
                Debug.Log("DEAD");
            }
            else if(iA.itemScale.x <= transform.lossyScale.x)
            {
                ScaleSize(collision);
                collision.gameObject.GetComponent<EnemyFollow>().RemoveSpawn();
            }
            /*else
            {
                EnemyFollow ef = iA.gameObject.GetComponent<EnemyFollow>();
                Vector3 resultingForce = ef.enemyForce * ef.DirectionFacing();

                transform.GetComponent<Rigidbody2D>()
                    .AddForce(resultingForce, ForceMode2D.Impulse);
            }*/
        }
        else if (collision.transform.CompareTag("Item"))
        {
            if(iA.itemScale.x <= transform.lossyScale.x || iA.itemScale.y <= transform.lossyScale.y)
            {
                ScaleSize(collision);
            }
            collision.gameObject.SetActive(false);
        }

        GameManager.SharedInstance.CheckSize();
    }

    private void ScaleSize(Collision2D collision)
    {
        if (collision == null) return;
        ItemAttributes iA = collision.gameObject.GetComponent<ItemAttributes>();
        transform.localScale += new Vector3(iA.PlayerScaleIncrease, iA.PlayerScaleIncrease, 0);
        FindObjectOfType<CameraFollow>().PushCameraBack(iA.CameraScaleIncrease);
    }

    /// <summary>
    /// Stops the current direction of the animation
    /// </summary>
    private void StopDirection()
    {
        switch (currentDirection)
        {
            case MovementDirection.Up:
                playerAnim.SetBool("UpMove_b", false);
                break;
            case MovementDirection.Down:
                playerAnim.SetBool("DownMove_b", false);
                break;
            case MovementDirection.Right:
                playerAnim.SetBool("RightMove_b", false);
                break;
            case MovementDirection.Left:
                playerAnim.SetBool("LeftMove_b", false);
                break;
        }
    }

    public void StartDeath()
    {
        StartCoroutine(Shrink());
    }

    public IEnumerator Shrink()
    {
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale = new Vector3(
                transform.localScale.x / 2,
                transform.localScale.y / 2,
                transform.localScale.z);

            yield return new WaitForSeconds(0.01f);
        }
    }

    enum MovementDirection
    {
        Up,
        Down,
        Right,
        Left
    }
}
