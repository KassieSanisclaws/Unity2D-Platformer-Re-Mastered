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

    [SerializeField] int maxLives = 5;
    public int testScore = 1000;
    private int _lives;
   

    //Player lives code with encapsulation loads the game over scene when the player lives are less than or equal to 0
    public int Lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
            {
                 Respawn();
            }


            _lives = value;

        //player increse past max lives so i should set it to max lives
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            if (_lives <= 0)
            {
                _lives = 0;
                //Game Over
                SceneManager.LoadScene("GameOverScene");
                GameOver();

             OnLifeValueChanged?.Invoke(_lives);
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

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        _lives = maxLives;
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
            _lives = maxLives;
    }

    public void Initialize()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        _lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
            int buildIndex = (SceneManager.GetActiveScene().name == "GamePlayScene") ? 0 : 1;
            SceneManager.LoadScene(buildIndex);
           
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
        // Check if the "GameOverScene" is added to the build settings
        if (SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/GameOverScene.unity") != -1)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            Debug.LogError("GameOverScene is not added to the build settings. Please add it to the build settings to proceed.");
        }
    }
}
