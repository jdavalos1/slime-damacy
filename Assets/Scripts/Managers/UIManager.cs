using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager SharedInstance;

    void Awake()
    {
        if (SharedInstance == null) SharedInstance = this;
    }

    public void TransitionToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
