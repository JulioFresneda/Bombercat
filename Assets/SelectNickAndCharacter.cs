using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectNickAndCharacter : MonoBehaviour {

    public InputField inputNick;


    public void Start()
    {
        if (PlayerPrefs.GetInt("c2u") == 1) GameObject.Find("c2locked").SetActive(false);
    }

    public void Nick()
    {
        string nick = inputNick.text;
        PlayerPrefs.SetString( "nick", nick );

    }

    public void Character( int numChar  )
    {
        PlayerPrefs.SetInt("character", numChar);
    }

    public void StartButtom()
    {
        SceneManager.LoadScene(2);
    }
}
