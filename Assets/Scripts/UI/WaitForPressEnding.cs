using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPressEnding : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(WaitForPlayerInput());
    }

    private IEnumerator WaitForPlayerInput()
    {
        while (!Input.anyKeyDown) yield return null;

        UIManager.SharedInstance.KeyPressed();
        UIManager.SharedInstance.SetEndingAnimation();
    }
}
