using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRangeZone : MonoBehaviour
{

    public List<Collider2D> detectedColliders = new List<Collider2D>();
    private Collider2D col;  


    private void Awake()
    {
      col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
    }

}
