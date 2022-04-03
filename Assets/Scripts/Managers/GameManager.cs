using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    public bool isGameOver;
    private AudioManager audioManager;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float maxPlayerSize;
    private void Awake()
    {
        SharedInstance = this;
        DontDestroyOnLoad(this);
        isGameOver = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("BGM-1", true);
    }
    
    public void CheckSize()
    {
        if (player.lossyScale.x >= maxPlayerSize && player.lossyScale.y >= maxPlayerSize) Debug.Log("Winner winner chicken dinner");
    }
}