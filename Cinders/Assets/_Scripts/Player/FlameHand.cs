using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHand : MonoBehaviour
{
    OVRInput.Controller hand = OVRInput.Controller.LHand;
    GameObject heldFire;
    [SerializeField] GameObject fireBolt;
    float throwSpeed, fireboltDamage, pullSpeed;
    Vector3 lastPosition;
    public bool useable;

    // Update is called once per frame
    void Update()
    {
        if (!useable) {
            return;
        }
        OVRInput.Update();
        if (!heldFire && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            heldFire = Instantiate(fireBolt, Vector3.up * 0.5f, Quaternion.identity);
            heldFire.GetComponent<Firebolt>().SetTarget(gameObject, pullSpeed);
        }
        if (heldFire)
        {
            if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                ThrowFire();
            }
            else
            {
                lastPosition = transform.position;
            }
        }
    }

    void ThrowFire()
    {
        if (heldFire)
        {
            Vector3 direction = transform.position - lastPosition;
            float motionMagnitude = OVRInput.GetLocalControllerVelocity(hand).magnitude;
            Vector3 velocity = throwSpeed * motionMagnitude * direction.normalized;
            heldFire.GetComponent<Firebolt>().Throw(velocity, fireboltDamage);
        }
        heldFire = null;
    }


    public void SetThrowSpeed(float speed)
    {
        throwSpeed = speed;
    }

    public void SetPullSpeed(float speed)
    {
        pullSpeed = speed;
    }

    public void SetFireboltDamage(float dmg)
    {
        fireboltDamage = dmg;
    }
}
