using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectNickAndCharacter : MonoBehaviour {

    public InputField inputNick;

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
