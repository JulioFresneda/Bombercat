using UnityEngine.UI;
using UnityEngine;

public class ScoreScript : MonoBehaviour {


    public Text scoreText;
    public Text levelText;
    public Text lifesText;
    public Text bombsText;
    public Text velocityText;
    public Text powerText;
    public Text nickText;

    float velocityNormalized;



    private GameObject player;
    private GameObject level;

    

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        level = GameObject.FindGameObjectWithTag("Level");
        

    }
	
	// Update is called once per frame
	void Update () {

        

        nickText.text = "Nick: " + PlayerPrefs.GetString("nick");
        scoreText.text = "Score: " + level.GetComponent<LevelScript>().score.ToString();
        levelText.text = "Level: " + level.GetComponent<LevelScript>().level.ToString();
        lifesText.text = "Lifes: " + level.GetComponent<LevelScript>().lifes.ToString();
        bombsText.text = "Bombs: " + player.GetComponent<PlayerController>().bombsTogether.ToString();
        velocityNormalized = player.GetComponent<PlayerController>().velocityPlayer - 3f;
        velocityText.text = "Velocity: " + velocityNormalized.ToString("0");
        powerText.text = "Power: " + player.GetComponent<PlayerController>().explosionRange.ToString();

        

    }
}
