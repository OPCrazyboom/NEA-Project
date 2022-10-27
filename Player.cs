using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Camera cam;
    public Transform crosshair;
    public GameObject bulletPrefab;
    public Transform attackPoint;
    public GameObject ammoCount;
    private int maxAmmo;
    private int currentAmmo;
    Weapons currentWeapon = new Fists();
    public int pickUpRange;
    protected GameObject equippedWeapon;
    protected GameObject discardedWeapon;
    public Transform defaultAttackPoint;
    public GameObject fists;
    
    // Start is called before the first frame update
    void Start()
    {
            
        WeaponCheck();
        

    }

    // Update is called once per frame
    void Update()
    {
        
         
        currentWeapon.setTimeSinceLastAttack(currentWeapon.getTimeSinceLastAttack() + Time.deltaTime);
        //time since last frame added on to the timer

        Movement();
        Aim();
        if (Input.GetMouseButton(0) && currentWeapon.readyToAttack() && currentWeapon.isAutomatic)
        {
            Debug.Log("Mouse button is held down");
            currentWeapon.Attack();
            if (!currentWeapon.isMelee){
                currentAmmo = currentWeapon.getCurrentAmmo();
                ammoCount.GetComponent<Text>().text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
            }
            
        }
        else if(Input.GetMouseButtonDown(0) && currentWeapon.readyToAttack())
        {
            Debug.Log("Mouse button was pressed");
            currentWeapon.Attack();
            if (!currentWeapon.isMelee)
            {
                currentAmmo = currentWeapon.getCurrentAmmo();
                ammoCount.GetComponent<Text>().text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right mouse button was pressed");
            Debug.Log("1. " +equippedWeapon);
            if(equippedWeapon != fists)
            {
                DiscardWeapn();
                Debug.Log("2. " + equippedWeapon);
            }
            
            PickUp();
            Debug.Log("3. " + equippedWeapon);
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
        Collider2D weaponToPickupCollider = Physics2D.OverlapCircle(rb.position, pickUpRange, LayerMask.GetMask("Weapon")); //checks for weapon colliders within the pick up range 
        if (weaponToPickupCollider == null)
        {
            
            return;
        }
        GameObject weaponToPickup = weaponToPickupCollider.gameObject;
        weaponToPickup.transform.SetParent(rb.gameObject.transform, true);
        weaponToPickup.transform.localPosition = new Vector2(0.8f, 0);
        weaponToPickup.transform.localRotation = Quaternion.Euler(0, 0, -90);
        WeaponInfo weaponInfo = weaponToPickup.GetComponent<WeaponInfo>();
        Debug.Log("Wow");
        
        switch (weaponToPickup.tag) //checks the weapon tag and changes the weapon object to be of that class 
        {
            case "Pistol":
                currentWeapon = new Pistol();
                break;
            case "Rifle":
                currentWeapon = new Rifle();
                break;
            case "Sniper":
                currentWeapon = new Sniper();
                break;
            case "Shotgun":
                currentWeapon = new Shotgun();
                break;
            case "Pipe":
                currentWeapon = new Pipe();
                break;
            case "Katana":
                currentWeapon = new Katana();
                break;

        }
        currentWeapon.setCurrentAmmo(weaponInfo.currentAmmo);
        attackPoint = weaponInfo.attackPoint;
        bulletPrefab = weaponInfo.bulletType;
        equippedWeapon = weaponToPickup;
        
        WeaponCheck();
    }
    void DiscardWeapn()
    {

        discardedWeapon = equippedWeapon;
        equippedWeapon = fists;
        WeaponInfo weaponInfo = discardedWeapon.GetComponent<WeaponInfo>();
        if (!currentWeapon.isMelee)
        {
            weaponInfo.currentAmmo = currentAmmo;
        }
        weaponInfo.attackPoint = null;
        discardedWeapon.transform.SetParent(null);
        discardedWeapon.AddComponent<Rigidbody2D>();
        discardedWeapon.GetComponent<Collider2D>().isTrigger = false;
        discardedWeapon.GetComponent<Rigidbody2D>().gravityScale = 0;
        weaponInfo.recentlyEquipped = true;
        discardedWeapon.GetComponent<Rigidbody2D>().AddForce(attackPoint.up * 7, ForceMode2D.Impulse);
        currentWeapon = new Fists();
        attackPoint = defaultAttackPoint;
        WeaponCheck();


    }


    void WeaponCheck()
    {
        currentWeapon.setAttackPoint(attackPoint);
        currentWeapon.isPlayerWeapon = true;
        if (currentWeapon.isMelee)
        {
            ammoCount.GetComponent<Text>().text = "";
            
        }
        if (!currentWeapon.isMelee)
        {
            
            currentWeapon.setBulletPrefab(bulletPrefab);
            maxAmmo = currentWeapon.getMaxAmmo();
            currentAmmo = currentWeapon.getCurrentAmmo();
            ammoCount.GetComponent<Text>().text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
