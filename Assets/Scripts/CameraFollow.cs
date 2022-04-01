using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed;
    [SerializeField]
    private Transform player;
    private float oldXPos;
    private float oldYPos;

    private void Start()
    {
        FollowPlayer();
    }
    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        float newX = Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * cameraSpeed);
        float newY = Mathf.Lerp(transform.position.y, player.position.y, Time.deltaTime * cameraSpeed);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    public void PushCameraBack(float scaleIncrease)
    {
        GetComponent<Camera>().orthographicSize += scaleIncrease;
    }
}
