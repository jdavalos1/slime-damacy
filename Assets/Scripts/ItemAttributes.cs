using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    public SpawnManager spawnOwner;
    [SerializeField]
    private float _playerScaleIncrease;

    /// <summary>
    /// Scale at which the player increases for absorbing the item
    /// </summary>
    public float PlayerScaleIncrease { get; private set; }

    [SerializeField]
    private float _cameraScaleIncrease;

    /// <summary>
    /// Scale at which the camera increases for absorbing the item
    /// </summary>
    public float CameraScaleIncrease { get; private set; }

    void Start()
    {
        PlayerScaleIncrease = _playerScaleIncrease;
        CameraScaleIncrease = _cameraScaleIncrease;
    }

    public void RemoveSpawn()
    {
        transform.gameObject.SetActive(false);
        //spawnOwner.currentlyActive--;
    }
}
