using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Awake()
    {
        
        Hashtable weaponIDs = new Hashtable(20);
        
        GameObject[] arrayOfObjects = FindObjectsOfType<GameObject>(); 
        List<GameObject> arrayOfWeapons = new List<GameObject>();
        for (int x = 0; x < arrayOfObjects.Length; x++) //searches through all game objects and finds the weapons 
        {
            if(arrayOfObjects[x].layer == 12)
            {
                arrayOfWeapons.Add(arrayOfObjects[x]);
            }
        }
        for (int i = 0; i < arrayOfWeapons.Count; i++) //gives each weapon game object an ID and adds it to the Hash table 
        {
            int total = 0;
            string weapon = arrayOfWeapons[i].name;
            
            char[] CharArray = weapon.ToCharArray();
            for (int j = 0; j < CharArray.Length; j++)
            {

                int x = System.Convert.ToInt32(CharArray[j]);
                total =+ x;
                
            }
            int id = total * 17;
            
            int location = id % 20;
           
            weaponIDs.Add(id, weapon);
            arrayOfWeapons[i].GetComponent<WeaponInfo>().hashId = id;
        }
    } 
}
