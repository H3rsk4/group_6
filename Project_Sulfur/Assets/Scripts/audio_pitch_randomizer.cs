using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_pitch_randomizer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Awake()
    {
       audioSource.pitch = Random.Range(.5f,1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
