using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_player : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioSource audioSource;
    public AudioClip audioClip;

    private static List<AudioClip> audioQueue = new List<AudioClip>();

    private static audio_pitch_randomizer audioPitcher;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioPitcher = GetComponent<audio_pitch_randomizer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(audioQueue.Count != 0){
            if(!audioSource.isPlaying){
                audioSource.clip = audioQueue[0];
                Debug.Log("playing " + audioClip);
                audioSource.PlayDelayed(0);
                audioQueue.RemoveAt(0);
            }
            
        }
    }

    public static void PlaySound(AudioClip _audioClip){
        
        if(audioSource.clip != _audioClip){
            audioSource.clip = _audioClip;
        }
        
        //audioQueue.Add(_audioClip);
        
        /*
        if(audioSource.isPlaying){
            //audioSource.Stop();
            //audioSource.PlayDelayed(audioSource.clip.length - audioSource.time);
            audioSource.PlayDelayed(audioSource.time);
        }else{
            audioSource.PlayDelayed(0);
        }
        */

        audioPitcher.RandomizePitch();
        
        audioSource.PlayDelayed(0);

        //Debug.Log("time " + audioSource.time);
        //Debug.Log("length " + audioSource.clip.length);
        
        
    }

}
