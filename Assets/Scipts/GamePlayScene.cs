using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayScene : MonoBehaviour
{
    [SerializeField] Transform gamePlaySceneStart;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //Spawns the player at the start of the game by calling the GameManager's SpawnPlayer method.
        GameManager.Instance.SpawnPlayer(gamePlaySceneStart);
    }
}
