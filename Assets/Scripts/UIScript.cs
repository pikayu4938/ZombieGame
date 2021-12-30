using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private GunController gunController;

    [SerializeField]
    private Text bulletText;

    void Update()
    {
        bulletText.text = gunController.currentGun.currentBulletCount + "/" + gunController.currentGun.carryBulletCount;
    }
}
