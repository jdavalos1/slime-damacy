using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager SharedInstance;
    public GameObject introSlimes;
    private Animator uiAnimator;
    [SerializeField]
    private GameObject endingUI;
    [SerializeField]
    private GameObject openingImg;
    [SerializeField]
    private GameObject[] endingImgs;

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
        uiAnimator.SetLayerWeight(1, 1);
        uiAnimator.SetBool("IsDead_b", true);
    }

    public void SetEndingAnimation()
    {
        int endImgNum = Random.Range(0, endingImgs.Length);

        GameManager.SharedInstance.startValue = endImgNum;
        endingImgs[endImgNum].SetActive(true);
        uiAnimator.SetBool("Pressed_b", true);
        uiAnimator.SetInteger("RestartAnimation", endImgNum);
    }

    public void TransitionToGameWin()
    {
        endingUI.SetActive(true);
        uiAnimator.SetLayerWeight(1, 1);
        uiAnimator.SetBool("WonGame_b", true);
    }

    public void KeyPressed()
    {
        uiAnimator.SetBool("Pressed_b", true);
    }
    
    public void HideSlimes()
    {
        introSlimes.SetActive(false);
    }
}
