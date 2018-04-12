using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeigerCounter : MonoBehaviour
{
    public AudioClip geigerClick;
    public GameObject target;
    public GameObject player;

    AudioSource audioSrc;
    float intervalCycle;
    float intervalFrequency;
    float interval;
    float intervalAmplitude;
    float time;


    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        interval = intervalCycle + Time.time;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, target.transform.position) * .1f;
        intervalAmplitude = distance;

        if (Time.time > interval)
        {
            audioSrc.PlayOneShot(geigerClick);
            intervalCycle = Random.Range(1, 11);
            intervalFrequency = Random.Range(1, 11);
            time = Time.time;
        }
        interval = Mathf.PerlinNoise(intervalCycle, intervalFrequency) * intervalAmplitude + time;
    }
}
