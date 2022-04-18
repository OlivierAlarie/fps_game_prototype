using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _controls;
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _gameOver;
    private bool _isPaused;
    private PauseControls _pauseAction;
    [SerializeField] private Player _player;


       // Start is called before the first frame update
    void Awake()
    {
        _player = FindObjectOfType<Player>();
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
            GameOver();
        }
    }

    private void DeterminePause()
    {
        if (_isPaused)
            Resume();
        else
            PauseGame();
    }
    
    private void GameOver()
    {
        _gameOver.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        _player.CommandManager.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        _credits.SetActive(false);
        _controls.SetActive(false);
        _player.CommandManager.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        _isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   


}

