using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public void Play()
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene(1);
    }

    public void SelectPlayer()
    {
        Debug.Log("Select Player pulsed");
    }

    public void Highscores()
    {
        Debug.Log("Highscores pulsed");
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
