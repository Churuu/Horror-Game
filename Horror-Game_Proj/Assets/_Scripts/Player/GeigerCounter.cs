using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeigerCounter : MonoBehaviour
{
    AudioSource audioSrc;
	public AudioClip geigerClick;
    public float intervalCycle;
	public float intervalFrequency;
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
            interval = Mathf.PerlinNoise(intervalCycle, intervalFrequency) + Time.time;
        }
    }
}
