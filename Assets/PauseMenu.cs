using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {


    public static bool gameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject bombSpawner;

	// Use this for initialization
	void Start () {
        pauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else Pause();
        }
	}
    public void Resume()
    {
        pauseMenu.SetActive(false);
        bombSpawner.GetComponent<BombSpawner>().paused = false;
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        bombSpawner.GetComponent<BombSpawner>().paused = true;
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
