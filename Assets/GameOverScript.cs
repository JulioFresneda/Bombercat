using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    public Text finalScore;

    public void Start()
    {
        if (PlayerPrefs.GetInt("fl") > 20 && PlayerPrefs.GetInt("c5u") == 0)
        {
            finalScore.text = ("score: " + PlayerPrefs.GetInt("fs").ToString() + '\n' + "New character unlocked!");
            PlayerPrefs.SetInt("c5u", 1);
            PlayerPrefs.SetInt("c4u", 1);
            PlayerPrefs.SetInt("c3u", 1);
            PlayerPrefs.SetInt("c2u", 1);
        }
        else if (PlayerPrefs.GetInt("fl") > 10 && PlayerPrefs.GetInt("c4u") == 0)
        {
            finalScore.text = ("score: " + PlayerPrefs.GetInt("fs").ToString() + '\n' + "New character unlocked!");
            PlayerPrefs.SetInt("c4u", 1);
            PlayerPrefs.SetInt("c3u", 1);
            PlayerPrefs.SetInt("c2u", 1);
        }
        else if (PlayerPrefs.GetInt("fl") > 6 && PlayerPrefs.GetInt("c3u") == 0)
        {
            finalScore.text = ("score: " + PlayerPrefs.GetInt("fs").ToString() + '\n' + "New character unlocked!");
            PlayerPrefs.SetInt("c3u", 1);
            PlayerPrefs.SetInt("c2u", 1);
        }
        else if (PlayerPrefs.GetInt("fl") > 2 && PlayerPrefs.GetInt("c2u") == 0)
        {
            finalScore.text = ("score: " + PlayerPrefs.GetInt("fs").ToString() + '\n' + "New character unlocked!");
            PlayerPrefs.SetInt("c2u", 1);
        }
        else finalScore.text = ("score: " + PlayerPrefs.GetInt("fs").ToString());
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
