using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton Pattern
    public static GameManager Instance => _instance;
    static GameManager _instance;
    public int testScore = 1000;


    private readonly int maxPlayerLives;

    public int playerLives = 3;
    //Player lives code with encapsulation loads the game over scene when the player lives are less than or equal to 0
    public int PlayerLives
    {
        get { return playerLives; }
        set
        {
            if (playerLives > value)
            {
                 Respawn();
            }


            playerLives = value;

            if (playerLives > maxPlayerLives)
            {
                playerLives = maxPlayerLives;
            }

            if (playerLives <= 0)
            {
                playerLives = 0;
                //Game Over
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    //Player Score code with encapsulation
    public int playerScore = 0;
 

    public int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //Load Static Scene before the game starts
        //SceneManager.LoadScene("StaticScene", LoadSceneMode.Additive);
        //SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        //SceneManager.GetActiveScene().GetRootGameObjects();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
            int buildIndex = (SceneManager.GetActiveScene().name == "GamePlayScene") ? 0 : 1;
            SceneManager.LoadScene(buildIndex);
            //Application.Quit();
        }
    }

    void Respawn()
    {

    }

    void GamneOver()
    {

    }
}
