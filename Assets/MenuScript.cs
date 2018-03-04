using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Text primero, segundo, tercero;
    public GameObject highscorespanel;

    public void Start()
    {
        highscorespanel.SetActive(false);
        primero.text = PlayerPrefs.GetString("nick1o", "-") + ": " + PlayerPrefs.GetInt("hs1o", 0);
        segundo.text = PlayerPrefs.GetString("nick2o", "-") + ": " + PlayerPrefs.GetInt("hs2o", 0);
        tercero.text = PlayerPrefs.GetString("nick3o", "-") + ": " + PlayerPrefs.GetInt("hs3o", 0);
    }

    public void Play()
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene(1);
    }


    public void Highscores()
    {
        highscorespanel.SetActive(true);
        Debug.Log("Highscores pulsed");
    }

    public void QuitHighscores()
    {
        highscorespanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }


    IEnumerator Wait()
    {
       
        yield return new WaitForSeconds(0.5f);
        
    }
}
