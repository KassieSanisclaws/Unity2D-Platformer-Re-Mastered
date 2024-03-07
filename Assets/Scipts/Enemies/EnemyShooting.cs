using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timeBtwShots;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //Distance between the player and the enemy
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        
        if (distance < 4)
        {
            timeBtwShots += Time.deltaTime;

            if (timeBtwShots > 2)
            {
                timeBtwShots = 0;
                Shoot();

            }

        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
