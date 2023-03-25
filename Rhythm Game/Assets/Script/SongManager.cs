using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    private static SongManager instance;
    public static SongManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelay;

    public int inputDelayInMilliseconds;
    public float marginOfError;

    public string fileLoc;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    public float noteDespawnY{get{return noteTapY - (noteSpawnY - noteTapY);}}

    public static MidiFile midiFile = null;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        ReadFromFile();

        if (midiFile == null)
            Debug.Log("worng midi file");
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLoc);
        GetDataFromMidi();
    }

    private void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var arr = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(arr, 0);

        foreach (var lane in lanes)
            lane.SetTimeStamps(arr);

        Invoke(nameof(StartSong), songDelay);
    }

    private void StartSong()
    {
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}
