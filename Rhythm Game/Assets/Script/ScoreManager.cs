using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get 
        {
            if (instance == null)
                return null;
            return instance; 
        }
    }

    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image scoreImg;
    [SerializeField] Sprite[] perfectGoodBad;

    int comboScore;

    void Start()
    {
        if(instance == null)
            instance = this;

        comboScore = 0;
    }

    public void Hit(int score)
    {
        if (scoreImg.gameObject.active == false)
            scoreImg.gameObject.SetActive(true);

        comboScore += 1;
        scoreImg.sprite = perfectGoodBad[score];
        scoreText.text = comboScore.ToString();
        animator.SetTrigger("Effect");
    }

    public void Miss()
    {
        if (scoreImg.gameObject.active == false)
            scoreImg.gameObject.SetActive(true);

        comboScore = 0;
        scoreImg.sprite = perfectGoodBad[3];
        scoreText.text = comboScore.ToString();
        animator.SetTrigger("Effect");
    }

    private void Update()
    {
    }
}