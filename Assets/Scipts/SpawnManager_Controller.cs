using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Controller : MonoBehaviour
{

    [SerializeField] Camera _camera;
    [SerializeField] GameObject[] _gameObjects;
    [SerializeField] int _offsetX;
    [SerializeField] int _offsetY;

    private GameObject _spawnedObject;

     private int _ranX;
             int _ranY;



    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }   
    }

     void Spawn()
    {
        int randomObjectId = Random.Range(0, _gameObjects.Length);
        Vector2 position = GetRandomCoordinates();

        _spawnedObject = Instantiate(_gameObjects[randomObjectId], position, Quaternion.identity) as GameObject;
    }

    Vector2 GetRandomCoordinates()
    {
        _ranX = Random.Range(0 + _offsetX, Screen.width - _offsetX);
        _ranY = Random.Range(0 + _offsetY, Screen.height - _offsetY);

        Vector2 coordinates = new(_ranX, _ranY);

        Vector2 screenToWorldPosition = _camera.ScreenToWorldPoint(coordinates);

        return screenToWorldPosition;
    }
}
