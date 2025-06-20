
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text liveText;
    public Text gameOver;

    private BladeController blades;
    private Spawner spawner;

    private int score;
    private int lives;
    private const string scores = "Score: ";
    private const string life = "Lives: ";

    private const string over = "GAME OVER";

    private float requiredTime = 5f;
    private float timeStart = 0f;

    private void Awake(){
        blades = FindObjectOfType<BladeController>();
        spawner = FindObjectOfType<Spawner>();
    }
    private void Start(){
        NewGame();
    }
    void Update(){
        if (lives == 0){
            timeStart += Time.deltaTime;

            if (timeStart > requiredTime) {
                LoadSceneMenu();
            }
        } else {
            timeStart = 0f;
        }
    }
    private void NewGame(){

        ClearScene();

        blades.enabled = true;
        spawner.enabled = true;

        lives = 3;
        score = 0;

        scoreText.text = scores + score.ToString();
        liveText.text = life + lives.ToString();
        gameOver.text = "";
    }

    public void IncreaseScore(int n){
        score+=n;
        scoreText.text = scores + score.ToString();
    }

    private void ClearScene(){
        Vegetable[] vegetables = FindObjectsOfType<Vegetable>();

        foreach(Vegetable veg in vegetables){
            Destroy(veg.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach(Bomb bomb in bombs){
            Destroy(bomb.gameObject);   
        }
    }

    public void Explode(){
        lives--;
        liveText.text = life + lives.ToString();

        if(lives <= 0){
            spawner.enabled = false;
            blades.enabled = false;
            scoreText.text = "";
            liveText.text = "";

            ClearScene();

            gameOver.text = over + "\n" + scores + score.ToString();

        }
    }

    public void LoadSceneMenu()
    {
        
        SceneManager.LoadScene("MainSceneMenu"); //Assets/Scenes/SampleScene.unity
    }
}
