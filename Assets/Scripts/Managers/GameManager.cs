using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int startValue;
    public static GameManager SharedInstance;
    public bool isGameOver;
    private AudioManager audioManager;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float maxPlayerSize;
    private void Awake()
    {
        if(SharedInstance == null) SharedInstance = this;
        DontDestroyOnLoad(this);
        startValue = -1;
        isGameOver = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world");
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(Constants.BGM, true);
    }

    /// <summary>
    /// Check the size of the player and if its gotten bigger than the max size
    /// </summary>
    public void CheckSize()
    {
        if (player.lossyScale.x >= maxPlayerSize && player.lossyScale.y >= maxPlayerSize) Debug.Log("Winner winner chicken dinner");
    }
}
