using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategicPoint : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Archer>())
        {
            other.gameObject.GetComponent<Archer>().insideStrategicPoint = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Archer>())
        {
            other.gameObject.GetComponent<Archer>().insideStrategicPoint = false;
        }
    }
}
