using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAudioFinish : MonoBehaviour
{
    public UnityEvent onAudioFinish;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        //get tyhe length of the audio clip
        float audioLength = audioSource.clip.length;
        //wait for the length of the audio clip
        StartCoroutine(WaitForAudio(audioLength));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitForAudio(float audioLength)
    {
        //wait for the length of the audio clip
        yield return new WaitForSeconds(audioLength);
        //do something after the audio clip has finished playing
        Debug.Log("Audio has finished playing");
        onAudioFinish.Invoke();
    }
}
