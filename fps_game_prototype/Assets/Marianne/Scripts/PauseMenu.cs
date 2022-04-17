using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _gameOver;
    private bool _isPaused;
    private PauseControls _pauseAction;
    [SerializeField] private Player _player;


       // Start is called before the first frame update
    void Awake()
    {
        _pauseAction = new PauseControls();

        _pauseMenu.SetActive(false);
        _isPaused = false;
    }

    private void OnEnable()
    {
        _pauseAction.Enable();
    }

    private void OnDisable()
    {
        _pauseAction.Disable();
    }


    private void Start()
    {
        _pauseAction.Pause.PauseGame.performed += _ => DeterminePause();

    }

    private void Update()
    {
        if(_player.Health <= 0)
        {
            _gameOver.SetActive(true);
        }
    }
    private void DeterminePause()
    {
        if (_isPaused)
            Resume();
        else
            PauseGame();
    }
    
    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}

