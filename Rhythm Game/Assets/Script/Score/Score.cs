using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI MaxCombo;
    [SerializeField] Text text;

    private void Start()
    {
        ScoreCalculate();
    }

    private void Update()
    {
        GameObject go = scoreText.gameObject;
        go.transform.Rotate(Vector3.up);
    }

    private void ScoreCalculate()
    {
        float noteCount = SelectedSong.Instance.score.Sum()*3;
        float totalScore = SelectedSong.Instance.score[0] * 3 + SelectedSong.Instance.score[1] * 2 + SelectedSong.Instance.score[2];
        if (totalScore / noteCount >= 0.9f) scoreText.text = "S";
        else if (totalScore / noteCount >= 0.8f) scoreText.text = "A";
        else if (totalScore / noteCount >= 0.7f) scoreText.text = "B";
        else if (totalScore / noteCount >= 0.6f) scoreText.text = "C";
        else if (totalScore / noteCount >= 0.5f) scoreText.text = "D";
        else if (totalScore / noteCount >= 0.4f) scoreText.text = "E";
        else scoreText.text = "F";

        MaxCombo.text = "MaxCombo : " + SelectedSong.Instance.maxCombo.ToString();
        text.text = $"Perfect : {SelectedSong.Instance.score[0]}\nCool : {SelectedSong.Instance.score[1]}\nBad : {SelectedSong.Instance.score[2]}\nMiss : {SelectedSong.Instance.score[0]}";
    }

    public void LoadLobby()
    {
        LoadingUIScript.Instance.LoadScene("Lobby");
    }
}
