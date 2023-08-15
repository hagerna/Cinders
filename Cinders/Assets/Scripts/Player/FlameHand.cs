using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHand : MonoBehaviour
{
    OVRInput.Controller Hand = OVRInput.Controller.LHand;
    bool isFireHeld = false;
    GameObject heldFire;
    [SerializeField] GameObject FireBolt;
    [SerializeField] float throwSpeed, FireboltDamage;

    public void SetThrowSpeed(float speed)
    {
        throwSpeed = speed;
    }

    public void SetFireboltDamage(float dmg)
    {
        FireboltDamage = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (!isFireHeld && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            isFireHeld = true;
            heldFire = Instantiate(FireBolt, Vector3.up * 0.5f, Quaternion.identity);
            heldFire.GetComponent<Firebolt>().SetTarget(gameObject);
        }
        if (isFireHeld && !OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            ThrowFire();
        }     
    }

    void ThrowFire()
    {
        Vector3 velocity = OVRInput.GetLocalControllerVelocity(Hand) * throwSpeed;
        if (heldFire)
        {
            heldFire.GetComponent<Firebolt>().Throw(velocity, FireboltDamage);
        }
        isFireHeld = false;
        heldFire = null;
    }
}
