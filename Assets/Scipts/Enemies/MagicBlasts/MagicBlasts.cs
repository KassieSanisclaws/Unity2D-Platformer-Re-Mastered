using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MagicBlasts : MonoBehaviour
{
    //private GameObject player;
    //private Rigidbody2D rb;
    //public float force;
    //private float timer;

    public float lifeTime;
    public Vector2 initVelocity;

    //Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //rb = GetComponent<Rigidbody2D>();

        //Vector2 direction = (player.transform.position - transform.position).normalized;
        //rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        //float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rot);

        if (lifeTime <= 0)
        {
            lifeTime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = initVelocity;
        Destroy(gameObject, lifeTime);
    }

    //Update is called once per frame
    //void Update()
    //{
    //    timer += Time.deltaTime;

    //    if (timer >= 5)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //collision.gameObject.GetComponent<PlayerHealth>().health  ;
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && CompareTag("PlayerProjectile"))
        {
            //collision.gameObject.GetComponent<Enemy>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}