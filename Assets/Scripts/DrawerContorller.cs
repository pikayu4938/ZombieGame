using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerContorller : MonoBehaviour
{
    float smooth = 2f;
    float openPos;
    float closePos;
    bool isOpen;

    void Start()
    {
        isOpen = false;
        closePos = transform.position.x;
        openPos = transform.position.x - 0.44f;
    }

    private void Update()
    {
        Vector3 pos = new Vector3((!isOpen?closePos : openPos), transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }

    public void ChangeDrawerState()
    {
        isOpen = !isOpen;
    }
}
