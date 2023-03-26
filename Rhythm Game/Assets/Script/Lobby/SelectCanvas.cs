using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCanvas : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.Play("SAnim");
    }

    private void OnDisable()
    {
        animator.Play("RevSAnim");
        Invoke("DisableObj", 0.5f);
    }

    private void DisableObj()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        GetComponent<SelectCanvas>().enabled = true;
    }

    public void Disable()
    {
        GetComponent<SelectCanvas>().enabled = false;
    }

    public void LoadSecne()
    {
        LoadingUIScript.Instance.LoadScene("Main");
    }
}
