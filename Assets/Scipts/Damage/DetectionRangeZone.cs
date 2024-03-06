using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRangeZone : MonoBehaviour
{
    public event System.Action<bool> PlayerDetected;

    public List<Collider2D> detectedColliders = new();
    private Collider2D col;  


    private void Awake()
    {
      col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);

        if (collision.CompareTag("Player"))
        {
            PlayerDetected?.Invoke(true);
        }
    }

    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);

        if (collision.CompareTag("Player"))
        {
            PlayerDetected?.Invoke(false);
        }
    }

}
