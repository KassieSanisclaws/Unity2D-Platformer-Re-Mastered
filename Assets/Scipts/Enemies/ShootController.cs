using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSource audioSource;

    public Vector2 initVelocity;
    public Transform spawnPLeft;
    public Transform spawnPRight;
    public MagicBlasts projectilePrefab;

    //Audio Source Clips:
    [SerializeField] AudioClip shootSound;


    //public GameObject bullet;
    //private float timeBtwShots;
    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

    }


    public void Fire()
    {
        if (!sr.flipX)
        {
            MagicBlasts currentProjectile = Instantiate(projectilePrefab, spawnPRight.position, spawnPRight.rotation);
            currentProjectile.initVelocity = initVelocity;
        }else
        {
            MagicBlasts currentProjectile = Instantiate(projectilePrefab, spawnPLeft.position, spawnPLeft.rotation);
            currentProjectile.initVelocity = new Vector2(-initVelocity.x, initVelocity.y);
        }

        //Play the shoot sound:
        if (shootSound)
        {
            GetComponent<AudioSource>().PlayOneShot(shootSound); //Play the shoot sound
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //Distance between the player and the enemy
    //    float distance = Vector2.Distance(transform.position, player.transform.position);
    //    Debug.Log(distance);
        
    //    if (distance < 4)
    //    {
    //        timeBtwShots += Time.deltaTime;

    //        if (timeBtwShots > 2)
    //        {
    //            timeBtwShots = 0;
    //            Shoot();

    //        }

    //    }
    //}

    //void Shoot()
    //{
    //    Instantiate(bullet, bulletPos.position, Quaternion.identity);
    //}
}
