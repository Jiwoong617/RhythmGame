using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lane : MonoBehaviour
{
    [SerializeField] Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    [SerializeField] KeyCode input;
    [SerializeField] GameObject notePrefab;
    [SerializeField] List<double> timeStamps = new List<double>();
    List<Note> notes = new List<Note>();

    int spawnIndex = 0;
    int inputIndex = 0;
    GameObject effect;

    void Start()
    {
        effect = transform.GetChild(0).gameObject;
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        SpawnNote();
        NoteHitJudge();
        PressEffect();
    }

    private void SpawnNote()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                note.transform.position = transform.position + Vector3.up * 10;
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }
    }

    private void NoteHitJudge()
    {
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(0);
                    //Debug.Log("Hit");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if(Math.Abs(audioTime - timeStamp) < marginOfError*2)
                {
                    Hit(1);
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if(Math.Abs(audioTime - timeStamp) < marginOfError * 4)
                {
                    Hit(2);
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    //print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                //print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }
    }

    private void PressEffect()
    {
        if(Input.GetKey(input))
            effect.SetActive(true);
        else 
            effect.SetActive(false);
    }

    private void Hit(int score)
    {
        ScoreManager.Instance.Hit(score);
    }

    private void Miss()
    {
        ScoreManager.Instance.Miss();
    }
} 