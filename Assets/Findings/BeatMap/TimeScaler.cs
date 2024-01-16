using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{

    public AudioSource song;  // Audio clip for the song


    public void ChangeScale()
    {
        float scale = GetComponent<UnityEngine.UI.Slider>().value;
        song.pitch = scale;
    }
}
