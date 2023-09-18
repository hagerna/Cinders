using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    protected int FireHealth, MaxFireHealth;
    [SerializeField] float duration;
    [SerializeField] AnimationCurve FlickerCurve;

    public void SetMaxHealth(int max)
    {
        MaxFireHealth = max;
        FireHealth = max;

    }

    public void FireReached()
    {
        FireHealth--;
        if (FireHealth > 0)
        {
            StartCoroutine(Flicker());
            // Trigger reached effects
        }
        else
        {
            // Game Over
            Destroy(gameObject);
        }
    }


    IEnumerator Flicker()
    {
        float time = 0;
        while (time <= duration)
        {
            // Trigger flickering effect
            time += Time.deltaTime;
        }
        yield return new WaitForEndOfFrame();
    }
}
