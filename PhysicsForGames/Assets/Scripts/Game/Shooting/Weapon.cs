
using System;
using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct ObjectFields
{
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject grenadeEffect;
    public GameObject grenade;
    public TextMeshProUGUI text;
}

public class Weapon : MonoBehaviour
{
    [Header("Gun")]
    public float bulletDamage = 2;
    public float fireRate = 15.0f;
    public float range = 100;
    public float gunImpactForce = 300.0f;
    public int ammo = 20;

    [Header("Grenade")]
    public float nadeDamage = 50.0f;
    public float nadeThrowForce = 8.0f;
    public float nadeRadius = 10.0f;
    public float force = 200.0f;

    [Space]
    public ObjectFields gameObjects;

    float nextTimeTofire = 0.0f;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        gameObjects.text = FindObjectOfType<TextMeshProUGUI>();
    }

    void Update()
    {
        //Shoot
        if (Input.GetButton("Fire1") && Time.time >= nextTimeTofire)
        {
            nextTimeTofire = Time.time + 1.0f / fireRate;
            
            //same as an if - else
            (ammo > 1 ? new Action(Shoot) : Reload)();

        }
        //Throw Grenade
        if (Input.GetKeyDown(KeyCode.G) && Time.time >= nextTimeTofire)
        {
            ThrowGrenade();
        }
        //display ammo
        gameObjects.text.SetText($"{ammo} / 20");
    }

    void Shoot()
    {
        ammo -= 1;
        //play the flash particle system
        gameObjects.muzzleFlash.Play();

        RaycastHit hitInfo; //if we hit something with our ray. and they have the target script on them - take damage
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            //if the target has a ragdole or target attached to it.
            Target target = hitInfo.transform.GetComponentInParent<Target>();

            //it will take damage
            if (target != null)
            {
                target.TakeDamage(bulletDamage);
            }

            //it will add push force to rigid bodies
            if (hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForceAtPosition(gunImpactForce * cam.transform.forward, hitInfo.point);
            }

            Instantiate(gameObjects.impactEffect, hitInfo.point, Quaternion.LookRotation(transform.position));

        }
    }

    void Reload()
    {
        Debug.Log("Reloading");
        ammo = 20;
        
        //move the gun in some way.
    }

    void ThrowGrenade()
    {
        //create and throw grenade forward.
        GameObject nade = Instantiate(gameObjects.grenade, transform.position, Quaternion.Euler(Vector3.forward));
        nade.GetComponent<Rigidbody>().AddForce(transform.forward * nadeThrowForce, ForceMode.Impulse);

        //wait 5 seconds then explode
        StartCoroutine(Explode(3, nade));
    }

    IEnumerator Explode(float time, GameObject nade)
    {
        yield return new WaitForSeconds(time);

        //check a radius and trigger ragdole.
        Collider[] colliders = Physics.OverlapSphere(nade.transform.position, nadeRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider c = colliders[i];
            RagDoll ragdoll = c.GetComponentInParent<RagDoll>();
            Target target = c.GetComponentInParent<Target>();
            Rigidbody rb = c.GetComponentInParent<Rigidbody>();

            if (ragdoll != null)           
                ragdoll.RagDollOn = true;
            
            //deal damage
            if (target != null)           
                target.TakeDamage(nadeDamage);
            
            if (rb != null)
            {
                //check if any object is inbetween 
                



                rb.AddExplosionForce(100 * force, nade.transform.position, 20);

                
            }

        }
        //visual effecty and destory.
        Instantiate(gameObjects.grenadeEffect, nade.transform.position, Quaternion.identity);
        Destroy(nade);
    }

}
