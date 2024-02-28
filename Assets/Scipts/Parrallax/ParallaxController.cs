using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxController : MonoBehaviour
{
    public Camera cam;
    public Transform followPlayerTarget;

    //Starting Position for the parallax gameObject:
      Vector2 canvas_StartPosition;

    //Start z value of the parallax game object:
      float z_StartPosition;

    //Disatnce of the camera moving from the starting position of the parallax object [How far the camera moves from the starting scene]:
      Vector2 Cam_ProgressionFromStart => (Vector2)cam.transform.position - canvas_StartPosition;

    float Z_DistanceFromTarget => transform.position.z - followPlayerTarget.position.z;

    //If an object is infrom of target use near clip plane. If behind target, use farClipPlane:
      float ClippingPlane => (cam.transform.position.z + (Z_DistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //The Further the object from the player, the faster the Parallax effect will move. Drag its z value closer to the target to make it move slower.
      float ParallaxFactor => Mathf.Abs(Z_DistanceFromTarget) / ClippingPlane; //ClippingPlane divide

    // Start is called before the first frame update
    void Start()
    {
        canvas_StartPosition = transform.position;
        z_StartPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //When the target moves, mkove the parallax object the same distance times a multiplier:
          Vector2 newPosition = canvas_StartPosition + Cam_ProgressionFromStart * ParallaxFactor;

        //The x/y position changes based on the target parallax factor, but z starys consistent:
          transform.position = new Vector3(newPosition.x, newPosition.y, z_StartPosition);
    }
}
