using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : Object
{
    public virtual void Attack() //inherited by both ranged shooting and melee swings 
    {

    }
    public void Throw() //unequps the weapon and hits enemies, disarming them
    {

    }
    protected float timeSinceLastAttack;
    public void setTimeSinceLastAttack(float t) // receives timer's count of time, on the player shoots the time is set to 0
    {
        timeSinceLastAttack = t;
    }
    public float getTimeSinceLastAttack()
    {
        return timeSinceLastAttack;
    }
    protected float timeBetweenAttacks;
    public virtual bool readyToAttack()
    {
        return true;
    }
    public bool isMelee;
    public bool isAutomatic;
    protected Transform attackPoint;
    public void setAttackPoint(Transform m)
    {
        attackPoint = m;
    }
    public virtual void setBulletPrefab(GameObject bP)
    {
        
    }
    public virtual int getCurrentAmmo()
    {
        return 0;
    }
    public virtual void setCurrentAmmo(int a)
    {

    }
    public virtual int getMaxAmmo()
    {
        return 0;
    }
    public bool isPlayerWeapon;
}
public class Melee : Weapons
{
    protected string damage_type;
    protected float range = 4;
    protected int layer;
    public Melee()
    {
        isMelee = true;
        

    }
    public override bool readyToAttack()
    {
        if (timeSinceLastAttack >= timeBetweenAttacks)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public override void Attack() 
    {
        if (isPlayerWeapon) //if the weapon is player owned it detects enemies only and vice versa
        {
            layer = LayerMask.GetMask("Enemy"); 
        }
        else
        {
            layer = LayerMask.GetMask("Player");
        }
        Collider2D[] entitiesHit = Physics2D.OverlapCircleAll(attackPoint.position, range, layer); // casts a circle from the attack point, over the radius of the range and only takes entity layer colliders
        for (int i = 0; i < entitiesHit.Length; i++)
        {
            Debug.Log("Kill");
            Destroy(entitiesHit[i].gameObject);
        }

        Debug.Log("cry");
        timeSinceLastAttack = 0;
        
    }
}
public class Fists : Melee
{
    public Fists()
    {
        damage_type = "blunt";
        range = 1.5f;
        timeBetweenAttacks = 0.1f;
    }    
    
}
public class Katana : Melee
{
    public Katana()
    {
        damage_type = "lethal";
        range = 2.5f;
        timeBetweenAttacks = 0.2f;
    }
}
public class Pipe : Melee
{

    public Pipe()
    {
        damage_type = "lethal";
        range = 2;
        timeBetweenAttacks = 0.23f;
    }
}
public class Guns : Weapons
{
    public Guns()
    {
        isMelee = false;
        isAutomatic = false;
    }
    
    
    protected GameObject bulletPrefab;
    public override void setBulletPrefab(GameObject bP)
    {
        bulletPrefab = bP;
    }

    protected int projectileSpeed;
    protected float spread;
    
  
    protected int maxAmmo;
    public override int getMaxAmmo()
    {
        return maxAmmo;
    }
    protected int currentAmmo;
    public override int getCurrentAmmo()
    {
        return currentAmmo;
    }
    public override void setCurrentAmmo(int a)
    {
        currentAmmo = a;
    }



    public override bool readyToAttack()
    {
        if (timeSinceLastAttack >= timeBetweenAttacks && currentAmmo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public override void Attack()
    {
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread); 

        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.AddForce(attackPoint.up * projectileSpeed + new Vector3(x, y), ForceMode2D.Impulse);
        timeSinceLastAttack = 0;

    }
    
}
public class Pistol : Guns
{

    public Pistol()
    {
        projectileSpeed = 15;
        spread = 1;
        timeBetweenAttacks = 0.4f;
        maxAmmo = 12;
        currentAmmo = 12;
    }
    public override void Attack()
    {
        base.Attack(); // propels the bullet forward
        currentAmmo--;
    }

}
public class Rifle : Guns
{
    public Rifle()
    {
        projectileSpeed = 17;
        spread = 2;
        timeBetweenAttacks = 0.12f;
        maxAmmo = 32;
        currentAmmo = 32;
        isAutomatic = true;

    }
    public override void Attack()
    {
        base.Attack();
        currentAmmo--;
    }
}
public class Shotgun : Guns
{

    public Shotgun()
    {
        projectileSpeed = 14;
        spread = 3.5f;
        timeBetweenAttacks = 0.7f;
        maxAmmo = 8;
        currentAmmo = 8;
    }
    public override void Attack()
    {
        for (int i = 0; i < 6; i++)
        {

            base.Attack();
        }
        currentAmmo--;
    }
}
public class Sniper : Guns
{
    public Sniper()
    {
        projectileSpeed = 18;
        spread = 0.2f;
        timeBetweenAttacks = 0.9f;
        maxAmmo = 4;
        currentAmmo = 4;
    }
}