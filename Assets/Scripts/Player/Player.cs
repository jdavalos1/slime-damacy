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

        transform.position += slimeSpeed * Time.deltaTime * move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ItemAttributes iA = collision.gameObject.GetComponent<ItemAttributes>();
        if (collision.transform.CompareTag("Item"))
        {
            if(iA.itemScale.x <= transform.lossyScale.x || iA.itemScale.y <= transform.lossyScale.y)
            {
                ScaleSize(iA);
                collision.gameObject.SetActive(false);
            }
        }

        GameManager.SharedInstance.CheckSize();
    }
    public bool CanSwallowItem(ItemAttributes iA)
    {
        if(iA.itemScale.x > transform.lossyScale.x)
        {
            playerAnim.SetBool("IsEaten_b", true);
            AudioManager.SharedInstance.Stop(Constants.BGM);
            AudioManager.SharedInstance.Play(Constants.SoftBGM, true);
            return false;
        }
        else if(iA.itemScale.x <= transform.lossyScale.x)
        {
            ScaleSize(iA);
        }
        /*else
            {
                EnemyFollow ef = iA.gameObject.GetComponent<EnemyFollow>();
                Vector3 resultingForce = ef.enemyForce * ef.DirectionFacing();

                transform.GetComponent<Rigidbody2D>()
                    .AddForce(resultingForce, ForceMode2D.Impulse);
            }*/
        return true;
    }

    private void ScaleSize(ItemAttributes iA)
    {
        if (iA == null) return;
        UIManager.SharedInstance.IncreaseSize(iA.itemScale.x, transform.lossyScale.x);
        UIManager.SharedInstance.UpdateAbsorbedImage(iA);
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

    /// <summary>
    /// Start the death of the enemy
    /// </summary>
    public void StartDeath()
    {
        StartCoroutine(Shrink());
    }

    /// <summary>
    /// Shrink the player over time
    /// </summary>
    /// <returns></returns>
    public IEnumerator Shrink()
    {
        GameManager.SharedInstance.isGameOver = true;
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale = new Vector3(
                transform.localScale.x / 2,
                transform.localScale.y / 2,
                transform.localScale.z);

            yield return new WaitForSeconds(0.5f);
        }

        UIManager.SharedInstance.TransitionToGameOver();
        transform.gameObject.SetActive(false);
    }


    enum MovementDirection
    {
        Up,
        Down,
        Right,
        Left
    }
}
