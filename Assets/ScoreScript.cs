using UnityEngine.UI;
using UnityEngine;

public class ScoreScript : MonoBehaviour {


    public Text scoreText;
    public Text levelText;
    public Text lifesText;
    public Text bombsText;
    public Text velocityText;
    public Text powerText;

    float velocityNormalized;



    public GameObject player;
    public GameObject level;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + level.GetComponent<LevelScript>().score.ToString();
        levelText.text = "Level: " + level.GetComponent<LevelScript>().level.ToString();
        lifesText.text = "Lifes: " + player.GetComponent<PlayerController>().lifes.ToString();
        bombsText.text = "Bombs: " + player.GetComponent<PlayerController>().bombsTogether.ToString();
        velocityNormalized = player.GetComponent<PlayerController>().velocityPlayer - 3f;
        velocityText.text = "Velocity: " + velocityNormalized.ToString("0");
        powerText.text = "Power: " + player.GetComponent<PlayerController>().explosionRange.ToString();

    }
}
