using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public Rigidbody2D bulletrb;
    int bounces = 0;

    // Start is called before the first frame update
    void Start()
    {
        bullet.layer = 10;
        Physics2D.IgnoreLayerCollision(10, 10);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.GetComponentInChildren<Collider2D>(), bullet.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //if (collision.gameObject.tag == "Wall" && bounces < 3)
        {
            //bounces++;
            //Debug.Log("bounces = " + bounces);
            //Vector2 normal = collision.contacts[0].normal;
            //Vector2 collision_incidence = bulletrb.velocity.magnitude;
            //bulletrb.AddForce(Vector2.Reflect(collision_incidence, normal));
            ////bulletrb.AddForce(reflect(collision_incidence, normal));





        }
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy");
            Destroy(bullet);
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(bullet);
        }
    }
    public Vector2 Modulus(Vector2 input)
    {
        return new Vector2(Mathf.Abs(input.x), Mathf.Abs(input.y));
    }
    public float dot_product(Vector2 incidence, Vector2 normal)
    {
        return (incidence.x * normal.x + incidence.y + normal.y);
    }
    public Vector2 reflect(Vector2 incidence, Vector2 normal)
    {
        return (incidence - 2 * dot_product(incidence, normal) * normal);
    }
   
}


