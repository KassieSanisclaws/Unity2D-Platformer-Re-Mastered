using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;


public class ParallaxController : MonoBehaviour
{
     public Camera cam;
     private Transform playerTransform; //Stores player tarnsform from GameManager.
     public bool isInitialized = false;
     private Vector2 canvas_StartPosition;
     private float z_StartPosition;



    //Disatnce of the camera moving from the starting position of the parallax object [How far the camera moves from the starting scene]:
      Vector2 Cam_ProgressionFromStart => (Vector2)cam.transform.position - canvas_StartPosition;

    float Z_DistanceFromTarget => transform.position.z - playerTransform.position.z;

    //If an object is infrom of target use near clip plane. If behind target, use farClipPlane:
      float ClippingPlane => (cam.transform.position.z + (Z_DistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //The Further the object from the player, the faster the Parallax effect will move. Drag its z value closer to the target to make it move slower.
      float ParallaxFactor => Mathf.Abs(Z_DistanceFromTarget) / ClippingPlane; //ClippingPlane divide

    // Start is called before the first frame update
    void Start()
    {
        canvas_StartPosition = transform.position;
        z_StartPosition = transform.position.z;

        //Get player transform from GameManager:
        if (GameManager.Instance != null && GameManager.Instance.PlayerInstance != null)
        {
            playerTransform = GameManager.Instance.PlayerInstance.transform;

            // Check if virtual camera is assigned in GameManager
            if (GameManager.Instance.virtualCamera != null)
            {
                // Assign player prefab to the virtual camera's follow target
                GameManager.Instance.virtualCamera.Follow = GameManager.Instance.PlayerInstance.transform;
            }
            else
            {
                Debug.LogError("Virtual camera not assigned to GameManager!");
            }

           }
        }


    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    // //Get player transform from GameManager:
    //     if(GameManager.Instance != null && GameManager.Instance.PlayerInstance != null)
    //    {
    //        playerTransform = GameManager.Instance.PlayerInstance.transform;

    //   // Check if virtual camera is assigned in GameManager
    //    if (GameManager.Instance.virtualCamera != null)
    //    {
    //        // Assign player prefab to the virtual camera's follow target
    //        GameManager.Instance.virtualCamera.Follow = GameManager.Instance.PlayerInstance.transform;
    //    }
    //    else
    //    {
    //        Debug.LogError("Virtual camera not assigned to GameManager!");
    //    }

    //  //Set initialization flag to true:
    //    isInitialized = true;
    //  }
    //    else
    //    {
    //        Debug.LogError("GameManager or PlayerInstance is not initialized!");
    //    }
    //}

    // Update is called once per frame
    void Update()
    { 
        //If the player transition is null, return:
          if (playerTransform == null)
        {
            Debug.Log("Player Transform is null");
          return;
        }
            

        //When the target moves, mkove the parallax object the same distance times a multiplier:
          Vector2 newPosition = canvas_StartPosition + Cam_ProgressionFromStart * ParallaxFactor;

        //The x/y position changes based on the target parallax factor, but z starys consistent:
          transform.position = new Vector3(newPosition.x, newPosition.y, z_StartPosition);

        //
        
    }
}
