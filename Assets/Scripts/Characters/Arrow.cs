using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    
    public Character.Team team;
    public float speed = 3.0f;
    int damage = 10;
    public float maximumTime = 3f;

	public void AfterEnable (Character.Team team, int damage) {
        this.damage = damage;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        this.team = team;
        StartCoroutine(Countdown());

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(maximumTime);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            if (other.GetComponent<Character>().team != team)
            {
                other.GetComponent<Character>().HealthPoints -= damage;
                gameObject.SetActive(false);
            }
        }
    }
	
}
