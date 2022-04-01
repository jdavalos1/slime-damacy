using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float slimeSpeed;

    // Start is called before the first frame update
    void Start()
    {
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

        if (xMove == 0.0f) move = new Vector3(0, yMove, 0);

        transform.position += move * Time.deltaTime * slimeSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Item"))
        {
            ItemAttributes iA = collision.gameObject.GetComponent<ItemAttributes>();
            Destroy(collision.gameObject);
            transform.localScale += new Vector3(iA.PlayerScaleIncrease, iA.PlayerScaleIncrease, 0);
            FindObjectOfType<CameraFollow>().PushCameraBack(iA.CameraScaleIncrease);
        }
    }
}
