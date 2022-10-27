using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public int currentAmmo;
    public Transform attackPoint;
    public GameObject bulletType;
    public GameObject weapon;
    public bool isMoving;
    public bool recentlyEquipped;
    public bool isEquipped;
    public int hashId;
    void Update()
    {
        Rigidbody2D rb;
        if (isMoving)
        {
            rb = weapon.GetComponent<Rigidbody2D>();
            if (rb.velocity.magnitude == 0)
            {
                isMoving = false;
                Destroy(rb);
                weapon.GetComponent<Collider2D>().isTrigger = true;
                weapon.layer = LayerMask.GetMask("Weapon");
            }
        }
        else
        {
            rb = null;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb;
        rb = weapon.GetComponent<Rigidbody2D> ();
        rb.velocity = new Vector2(0, 0);
       
        if (collision.gameObject.tag == "Enemy")
        {
            
        }
        weapon.GetComponent<Collider2D>().isTrigger = true;
        Destroy(weapon.GetComponent<Rigidbody2D>());
    }
}    
