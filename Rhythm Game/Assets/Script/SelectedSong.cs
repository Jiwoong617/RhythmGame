using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedSong : MonoBehaviour
{
    private static SelectedSong instance;
    public static SelectedSong Instance
    {
        get 
        {
            if (instance == null)
                return null;
            return instance; 
        }
    }

    public int[] score = new int[4];
    public int maxCombo;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public string songName = "private_letter";
    public bool isSelected = false;

    public void GetSongInf()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        GameObject go = EventSystem.current.currentSelectedGameObject;
        SongInf inform = go.GetComponent<SongInf>();
        if (inform == null)
            return;

        instance.songName= inform.songName;
        isSelected = true;

        ResetScore();
    }

    private void ResetScore()
    {
        for (int i = 0; i < 4; i++)
            score[i] = 0;
        maxCombo = 0;
    }
}
