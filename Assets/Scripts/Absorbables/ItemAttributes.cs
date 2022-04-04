using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
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

    /// <summary>
    /// Scale of item used to check the item versus the player in order to absorb
    /// </summary>
    public Vector2 itemScale;

    void Start()
    {
        PlayerScaleIncrease = _playerScaleIncrease;
        CameraScaleIncrease = _cameraScaleIncrease;
    }
}
