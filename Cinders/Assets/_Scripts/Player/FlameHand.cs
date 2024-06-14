using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHand : MonoBehaviour
{
    OVRInput.Controller hand = OVRInput.Controller.LHand;
    GameObject heldFire;
    Vector3 lastPosition;
    public bool useable;
    FlameHandSO flameHand;
    List<Upgrade> flameHandUpgrades;

    private void Start()
    {
        GameManager.instance.newRun += (RunSettings obj) => LoadFlameHand(obj.flameHandBase);
    }

    private void LoadFlameHand(FlameHandSO scriptableObj)
    {
        flameHand = Instantiate(scriptableObj);
        if (scriptableObj.rightHanded)
        {
            hand = OVRInput.Controller.LHand;
        }
        else
        {
            hand = OVRInput.Controller.RHand;
        }
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade is InstantUpgrade)
        {
            InstantUpgrade instant = upgrade as InstantUpgrade;
            instant.ApplyUpgrade(flameHand);
        }
        flameHandUpgrades.Add(upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        if (!useable) {
            return;
        }
        OVRInput.Update();
        if (!heldFire && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            heldFire = Instantiate(flameHand.fireboltPrefab, Vector3.up * 0.5f, Quaternion.identity);
            heldFire.GetComponent<Firebolt>().SetTarget(gameObject, flameHand.pullSpeed);
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
            float motionMagnitude = direction.magnitude * 100;
            Vector3 velocity = flameHand.throwSpeed * motionMagnitude * direction.normalized;
            heldFire.GetComponent<Firebolt>().Throw(velocity, flameHand.damage);
        }
        heldFire = null;
    }
}
