using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 2f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(-90, ( open ? doorOpenAngle : doorCloseAngle), 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
    }

    public void ChangeDoorState() {
        open = !open;
    }

}
