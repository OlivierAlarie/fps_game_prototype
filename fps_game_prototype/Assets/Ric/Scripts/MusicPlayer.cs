using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource regular;
    [SerializeField] private AudioSource bossfight;
    private bool bfIsPlaying = false;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Collider>().gameObject.tag == "Player" && !bfIsPlaying)
        {
            bossfight.Play();
            regular.Pause();
            bfIsPlaying = true;
        }
    }
}
