using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCanvas : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.Play("RevAnim");
    }

    private void OnDisable()
    {
        animator.Play("Anim");
        Invoke("DisableObj", 0.5f);
    }

    private void DisableObj()
    {
        gameObject.SetActive(false);
    }

    public void Exitgame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        GetComponent<TitleCanvas>().enabled = true;
    }

    public void Disable()
    {
        GetComponent<TitleCanvas>().enabled = false;
    }
}
