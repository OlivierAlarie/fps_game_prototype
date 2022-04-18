using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public PauseMenu pauseMenuScript;
    public MusicPlayer musicplayer;

    private void OnTriggerEnter(Collider other) 
    {
        
        if(other.GetComponent<Collider>().gameObject.tag == "Player")
        {
            pauseMenuScript.VictoryScreen();
            musicplayer.PlayCredits();
        }
    }
}
