using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPress : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(WaitForPlayerInput());
    }

    private IEnumerator WaitForPlayerInput()
    {
        while (!Input.anyKeyDown) yield return null;

        gameObject.GetComponentInParent<Animator>().SetBool("StartGame_b", true);
    }
}
