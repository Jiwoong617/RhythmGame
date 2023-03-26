using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SongInf : MonoBehaviour
{
    public string ButtonName;
    public string songName;
    public Sprite songImg;

    [SerializeField] Text t;
    [SerializeField] Image img;

    private void Start()
    {
        t.text = ButtonName;
        gameObject.GetComponent<Button>().onClick.AddListener(() => { img.sprite = songImg; });
    }

}
