using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu = null;
    private bool _isPaused = false;
    // Start is called before the first frame update
    void Awake()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            Debug.Log("entered");
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            Resume();
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }


}

