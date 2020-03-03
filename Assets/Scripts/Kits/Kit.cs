using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kit : MonoBehaviour {

    string lastDir = "down";
    float movementSpeed = 20f;
    float maximumTime = 20f;
    public AudioClip sound;

    public void AfterEnable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position += new Vector3(0, 0.5f, 0);
        GetComponent<Rigidbody>().AddForce(transform.up * -movementSpeed);
        StartCoroutine(moveVertically());
        GetComponent<Rigidbody>().AddTorque(transform.up * movementSpeed);
        StartCoroutine(Countdown());

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(maximumTime);
        gameObject.SetActive(false);
    }


    IEnumerator moveVertically()
    {
        if (lastDir == "down")
        {
            lastDir = "up";
            GetComponent<Rigidbody>().AddForce(transform.up * movementSpeed * 2);
        }
        else
        {
            lastDir = "down";
            GetComponent<Rigidbody>().AddForce(transform.up * -movementSpeed * 2);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(moveVertically());
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == GameManager.player)
        {
            GrabKit();
        }
    }

    protected virtual void GrabKit() { }

}
