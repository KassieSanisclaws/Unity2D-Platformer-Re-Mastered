using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;


[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    //Singleton Pattern
    public static GameManager Instance => _instance;
    static GameManager _instance;
   

    [SerializeField] PlayerController playerPrefab;
    public PlayerController PlayerInstance => _playerInstance;
    PlayerController _playerInstance = null;
    Transform currentSpawnpoint;
    public CinemachineVirtualCamera virtualCamera; //Reference to the virtual camera in the scene.

    public UnityEvent<int> OnLifeValueChanged;

    private readonly int maxPlayerLives = 3;
    public int testScore = 1000;
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
                GameOver();
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

        if (_instance)
        {
           Destroy(gameObject);
           return;
        }
       
            _instance = this;
            DontDestroyOnLoad(gameObject);
            playerLives = maxPlayerLives;

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

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateCheckpoint(GameObject.FindGameObjectWithTag("Test").transform);
        }
    }

    //Function to Change Scene:
    public void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    //Function to Update Checkpoint:  when first load into the level works but whe  you click escape
    public void UpdateCheckPoint(Transform updatedCheckPoint)
    {
        currentSpawnpoint = updatedCheckPoint;
    }

    //Function To Spawn Player:
    public void SpawnPlayer(Transform spawnLocation)
    {
        _playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        currentSpawnpoint = spawnLocation;
       
        Debug.LogWarning("ParallaxController not assigned in the GameManager. Make sure to assign it in the inspector.");
    }

    //Function to Update Checkpoint:
    public void UpdateCheckpoint(Transform updatedCheckPoint)
    {
        Debug.Log("Updated Checkpoint");
        currentSpawnpoint = updatedCheckPoint;
    }

    //Function to Respawn Player:
    void Respawn()
    {
        Debug.Log("Respawn Called");

        _playerInstance.transform.position = currentSpawnpoint.position;
    }

    //Function to Game Over:
    void GameOver()
    {
       Debug.Log("Game Over Called");
    }
}
