using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager SharedInstance;
    private Animator uiAnimator;
    [SerializeField]
    private GameObject endingUI;
    [SerializeField]
    private GameObject openingImg;


    void Awake()
    {
        if (SharedInstance == null) SharedInstance = this;

        uiAnimator = GetComponent<Animator>();
    }

    public void TransitionToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void StopOpeningUI()
    {
        openingImg.SetActive(false);
    }

    public void TransitionToGameOver()
    {
        endingUI.SetActive(true);
        uiAnimator.SetBool("IsDead_b", true);
    }

}
