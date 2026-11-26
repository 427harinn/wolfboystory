using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackTest : MonoBehaviour
{

    private AudioSource audio;

    bool audioplayed;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();

        StartCoroutine(Checking(() => {
            GManager.instance.musicstop = true;
        }));
    }

    public delegate void functionType();
    private IEnumerator Checking(functionType callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying && audioplayed)
            {
                callback();
                break;
            }
        }
    }

    private void Update()
    {
        if(audio.isPlaying)
        {
            audioplayed = true;
        }
    }
}
