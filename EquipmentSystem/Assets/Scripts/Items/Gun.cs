using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item, IInteractable
{

    public static Gun instance;
    
    [SerializeField] private int bulletCount;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private bool isReloading;
    [SerializeField] private float nextTimeToFire = 0f;
    [SerializeField] private float fireRate = 15f;

    [SerializeField] private Transform MagTransform;
    [SerializeField] private Transform BulletTransform;
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        instance = this;
    }

    public void Interact()
    {
        if (Time.time >= nextTimeToFire && bulletCount > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
    }
    
    private void Shoot()
    {
        bulletCount--;
        GameObject newbullet = Instantiate(bulletPrefab, BulletTransform.position, Quaternion.LookRotation(transform.forward));
        Destroy(newbullet, 1);
    }
    public IEnumerator Reload(AmmoClip targetClip)
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        bulletCount = targetClip.clipAmount;
        targetClip.gameObject.transform.position = MagTransform.position;
        targetClip.gameObject.transform.rotation = Gun.instance.transform.rotation;
        isReloading = false;

    }
}
