using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

    public Character.Team team;
    public int damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            if (other.GetComponent<Character>().team != team)
            {
                other.GetComponent<Character>().HealthPoints -= damage;
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
