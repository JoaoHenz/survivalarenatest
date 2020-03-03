using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float MouseSensitivity;
    float viewRange=90f;
    Transform cameraTransform;
    float pitch = 0f;
    public float yaw = 0f;
    public GameObject playersprite;
    Player player;
    float cameraSensitivity = 2f;


    void Start(){
      MouseSensitivity = cameraSensitivity;
      cameraTransform = gameObject.transform;
      player = gameObject.GetComponentInParent(typeof(Player)) as Player;
    }

    void LateUpdate()
    {

      pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
      yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
      pitch = Mathf.Clamp(pitch, -viewRange+10, viewRange-30);

      transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0f);


    }
}
