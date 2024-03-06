using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBlasts : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;

    //Start is called before the first frame update
     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    //Update is called once per frame
     void Update()
    {
        
    }
    
}