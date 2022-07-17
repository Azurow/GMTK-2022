using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    Animator animator;
    public float transitionDelayTime = 1.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadLevel(int scene)
    {
        StartCoroutine(DelayLoadLevel(scene));
    }

    IEnumerator DelayLoadLevel(int index)
    {
        animator.SetTrigger("TriggerTransition");
        yield return new WaitForSeconds(transitionDelayTime);
        SceneManager.LoadScene(index);
    }
}
