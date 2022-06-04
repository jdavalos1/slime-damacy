using System;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField]
    private TextMeshProUGUI sizeIncreaseText;
    [SerializeField]
    private TextMeshProUGUI currentSizeText;
    [SerializeField]
    private Image lastAbsorbed;
    [SerializeField]
    private GameObject inGameUI;
    
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
        inGameUI.SetActive(false);
        endingUI.SetActive(true);
        uiAnimator.SetLayerWeight(1, 1);
        uiAnimator.SetBool("IsDead_b", true);
    }

    public void SetEndingAnimation()
    {
        int endImgNum = Random.Range(0, endingImgs.Length);

        endingImgs[endImgNum].SetActive(true);
        uiAnimator.SetBool("Pressed_b", true);
        uiAnimator.SetInteger("RestartAnimation", endImgNum);
    }

    public void TransitionToGameWin()
    {
        inGameUI.SetActive(false);
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

    public void IncreaseSize(float sizeIncrease, float currentSize)
    {
        uiAnimator.SetBool("ItemObtained_b", true);
        sizeIncreaseText.gameObject.SetActive(true);
        sizeIncreaseText.text = $"+{sizeIncrease}";
        currentSizeText.text = $"{(currentSize + sizeIncrease):0.00} m";

    }
    
    public void UpdateAbsorbedImage(ItemAttributes iA)
    {
        lastAbsorbed.sprite = iA.itemBaseImage;
    }
}
