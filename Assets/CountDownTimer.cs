using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public int startTime = 5;
    TextMeshProUGUI timerText;
    public AudioClip beepLow;
    public AudioClip beepHigh;
    AudioSource audioSource;

    public ClimbingLadder ClimbingLadder;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timerText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2); /// laggy editor start workaround // REMOVE IN FINAL BUILD
        while (startTime > 0)
        {
            timerText.text = startTime.ToString();
            audioSource.PlayOneShot(beepLow);
            yield return new WaitForSeconds(60/ClimbingLadder.bpm);
            startTime--;
        }
        audioSource.PlayOneShot(beepHigh);
        timerText.text = "GO!";
        ClimbingLadder.isPlaying = true;
        ClimbingLadder.song.Play();
        yield return new WaitForSeconds(1);
        timerText.text = "";
    }

}
