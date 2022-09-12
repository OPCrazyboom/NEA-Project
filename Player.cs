using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Camera cam;
    public Transform crosshair;
    public GameObject bulletPrefab;
    public Transform muzzle;
    public float timer;
    Shotgun currentWeapon = new Shotgun();
    
    // Start is called before the first frame update
    void Start()
    {
        weaponCheck();


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentWeapon.getTimeSinceLastAttack());
         //time since last frame added on to the timer
        currentWeapon.setTimeSinceLastAttack(currentWeapon.getTimeSinceLastAttack() + Time.deltaTime);

        Movement();
        Aim();
        if (Input.GetMouseButton(0) && currentWeapon.readyToAttack())
        {
            Debug.Log("Mouse button is pressed down");
            currentWeapon.Attack();
           
        }

        
    }

    // Fixed Update is called at a fixed interval (independent of frame rate)
    void FixedUpdate()
    {
        
    }
    void Movement()
    {
        float x_value = Input.GetAxisRaw("Horizontal"); 
        float y_value = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(x_value, y_value).normalized * 4; // receives player input and moves player's rigidbody, 4 represents move speed
        
    }
    void Aim()
    {
        Vector2 MousePos = cam.ScreenToWorldPoint(Input.mousePosition); // converts mouse position input to coordinates on screen
        crosshair.position = MousePos;
        Vector2 lookDirection = MousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg; // tan(angle) = y / x therefore angle = arctan(y / x)
        rb.rotation = angle;
        Vector2 location = rb.position;
        Vector3 camPosition = cam.transform.position;
        camPosition.x = location.x;
        camPosition.y = location.y;
        camPosition.z = -10;
        cam.transform.position = camPosition; // makes the camera follow the player's rigidbody
    }
    
    void PickUp() //will pick up a new weapon and replace the old if the player has one 
    {
        weaponCheck();
    }

    void weaponCheck()
    {
        if (!currentWeapon.meleeCheck())
        {
            currentWeapon.setMuzzle(muzzle);
            currentWeapon.setBulletPrefab(bulletPrefab);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
