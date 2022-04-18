using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public PauseMenu pauseMenu;

    private void Start() 
    {
        pauseMenu = GetComponent<PauseMenu>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Collider>().gameObject.tag == "Player")
        {
            pauseMenu.VictoryScreen();
        }
    }
}
