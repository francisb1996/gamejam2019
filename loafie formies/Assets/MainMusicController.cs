using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicController : MonoBehaviour
{
    private AudioSource audio;
    private AudioSource audio2;

    public AudioClip clip1;
    public AudioClip clip2;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        audio = GameObject.Find("MainCamera").GetComponent<AudioSource>();
        audio2 = GameObject.Find("MainCamera").GetComponentInChildren<AudioSource>();
        audio.clip = clip1;
        audio.Play();
        Debug.Log("Length: " + audio.clip.length);
        yield return new WaitForSeconds(audio.clip.length - 3);
        audio2.clip = clip2;
        audio2.Play();
    }
}
