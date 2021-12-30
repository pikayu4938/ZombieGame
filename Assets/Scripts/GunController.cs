using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    public GunScript currentGun;

    private AudioSource audioSource;

    private RaycastHit hitInfo;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    GameObject hitEffectPrefab;

    private float currentFireRate;

    private bool isReload;

    /*
    [SerializeField]
    private Vector3 originPos;*/

    private void Start()
    {
        isReload = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
    }

    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    private void TryFire()
    {
        if (Input.GetKey(KeyCode.Mouse0) && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
                StartCoroutine(ReloadCoroutine());
        }
        
    }

    private void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;
        currentGun.muzzleFlash.Play();
        PlaySE(currentGun.fireSound);
        currentFireRate = currentGun.fireRate;
        Hit();

        /*
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());*/
    }

    private void Hit()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, currentGun.range))
        {
            Debug.Log(hitInfo.transform.name);
            GameObject clone = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2.0f);
            if (hitInfo.collider.CompareTag("Zombie"))
                hitInfo.collider.gameObject.SendMessage("damaged", currentGun.damage);
        }
    }

    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }
    
    IEnumerator ReloadCoroutine()
    {
        if (currentGun.carryBulletCount > 0)
        {
            isReload = true;
            yield return new WaitForSeconds(currentGun.reloadTime);

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount = 0;
            }

            /*
            currentGun.currentBulletCount = currentGun.carryBulletCount > currentGun.maxBulletCount ? currentGun.maxBulletCount : currentGun.carryBulletCount;
            currentGun.carryBulletCount -= currentGun.maxBulletCount;
            if (currentGun.carryBulletCount < 0)
                currentGun.carryBulletCount = 0;
                */
            isReload = false;
        }
        else
            Debug.Log("not enough ammo");
    }

    /*
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);

        currentGun.transform.localPosition = originPos;
        while (currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
            yield return null;
        }

        while(currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
        }
    }*/

    private void PlaySE(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

}
