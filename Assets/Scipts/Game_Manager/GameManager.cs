using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
}
