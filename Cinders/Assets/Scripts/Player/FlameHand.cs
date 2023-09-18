using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHand : MonoBehaviour
{
    OVRInput.Controller Hand = OVRInput.Controller.LHand;
    GameObject HeldFire;
    [SerializeField] GameObject FireBolt;
    float ThrowSpeed, FireboltDamage, PullSpeed;

    public void SetThrowSpeed(float speed)
    {
        ThrowSpeed = speed;
    }

    public void SetPullSpeed(float speed)
    {
        PullSpeed = speed;
    }

    public void SetFireboltDamage(float dmg)
    {
        FireboltDamage = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (HeldFire == null && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            HeldFire = Instantiate(FireBolt, Vector3.up * 0.5f, Quaternion.identity);
            HeldFire.GetComponent<Firebolt>().SetTarget(gameObject, PullSpeed);
        }
        if (HeldFire != null && !OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            ThrowFire();
        }
        if (HeldFire == null && Input.GetKey(KeyCode.Space))
        {
            HeldFire = Instantiate(FireBolt, Vector3.up * 0.5f, Quaternion.identity);
            HeldFire.GetComponent<Firebolt>().SetTarget(gameObject, PullSpeed);
        }
    }

    void ThrowFire()
    {
        Vector3 velocity = OVRInput.GetLocalControllerVelocity(Hand) * ThrowSpeed;
        if (HeldFire)
        {
            HeldFire.GetComponent<Firebolt>().Throw(velocity, FireboltDamage);
        }
        HeldFire = null;
    }
}
