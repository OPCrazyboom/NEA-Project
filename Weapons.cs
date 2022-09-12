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
    protected bool isMelee;
    public bool meleeCheck()
    {
        return isMelee;
    }

}
public class Melee : Weapons
{
    protected string damage_type;
    protected int range;
    public Melee()
    {
        isMelee = true;
    }
}
public class Katana : Melee
{
    public Katana() : base()
    {
        damage_type = "sharp";

    }
}
public class Pipe : Melee
{

    public Pipe()
    {
        damage_type = "blunt";
    }
}
public class Guns : Weapons
{
    public Guns()
    {
        isMelee = false;
    }
    protected Transform muzzle;
    public void setMuzzle(Transform m)
    {
        muzzle = m;
    }
    protected GameObject bulletPrefab;
    public void setBulletPrefab(GameObject bP)
    {
        bulletPrefab = bP;
    }

    protected int projectileSpeed = 14;
    protected int spread;
    protected bool isAutomatic = false;
  
    protected int maxAmmo;
    public int getMaxAmmo()
    {
        return maxAmmo;
    }
    protected int currentAmmo;
    public int getCurrentAmmo()
    {
        return currentAmmo;
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

        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.AddForce(muzzle.up * projectileSpeed + new Vector3(x, y), ForceMode2D.Impulse);
        timeSinceLastAttack = 0;

    }
    
}
public class Pistol : Guns
{   
    
    public Pistol()
    {
        
        spread = 2;
        timeBetweenAttacks = 1;
    }
    public override void Attack()
    {
        base.Attack(); // propels the bullet forward
        currentAmmo--;
    }
    
}
public class Shotgun : Guns
{
    public Shotgun()
    {
        spread = 4;
        timeBetweenAttacks = 1;
        maxAmmo = 8;
        currentAmmo = 8;
    }
    public override void Attack()
    {
        for (int i = 0; i < 8; i++)
        {

            base.Attack();
        }
        currentAmmo--;
    }
}
public class Rifle : Guns
{
    public Rifle()
    {
        spread = 3;
        isAutomatic = true;

    }
    public override void Attack()
    {
        base.Attack();
        currentAmmo--;
    }
}
public class Sniper : Guns
{

}