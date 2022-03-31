using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float slimeSpeed;
    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
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
        Vector2 move = new Vector2(xMove, 0);

        if(yMove != 0.0f)
        {
            move = new Vector2(0, yMove);
        }

        cc.Move(move * Time.deltaTime * slimeSpeed);
    }
}
