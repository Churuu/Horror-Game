using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeigerCounter : MonoBehaviour
{
    AudioSource audioSrc;
	public AudioClip geigerClick;
    [Range(0, 20)] public float intervalCycle;
	[Range(0, 20)] public float intervalFrequency;
    [Range(0.1f, 5)] public float intervalAmplitude;
    float interval;


    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        interval = intervalCycle + Time.time;
    }

    void Update()
    {
        if (Time.time > interval)
        {
			audioSrc.PlayOneShot(geigerClick);
            interval = Mathf.PerlinNoise(intervalCycle, intervalFrequency) * intervalAmplitude  + Time.time;
        }
    }
}
