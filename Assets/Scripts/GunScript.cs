using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public string gunName;
    public float accuracy;
    public float fireRate;
    public float reloadTime;
    public float damage;
    public float range;

    public int reloadBulletCount;
    public int currentBulletCount;
    public int maxBulletCount;
    public int carryBulletCount;

    public float retroActionForce;
    public float retroActionFineSightForce;

    public Vector3 fineSightOriginPos;

    public Animator anim;
    public ParticleSystem muzzleFlash;

    public AudioClip fireSound;
    
}
