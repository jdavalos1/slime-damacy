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
        Movement();
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
        if (collision.transform.CompareTag("Item"))
        {
            ItemAttributes iA = collision.gameObject.GetComponent<ItemAttributes>();
            transform.localScale += new Vector3(iA.PlayerScaleIncrease, iA.PlayerScaleIncrease, 0);
            FindObjectOfType<CameraFollow>().PushCameraBack(iA.CameraScaleIncrease);
            iA.RemoveSpawn();
        }
    }

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

    enum MovementDirection
    {
        Up,
        Down,
        Right,
        Left
    }
}
