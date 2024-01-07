using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    protected int fireHealth, maxFireHealth;
    [SerializeField] float duration;
    [SerializeField] AnimationCurve flickerCurve;
    public bool immune = false;

    public void SetMaxHealth(int max)
    {
        maxFireHealth = max;
        fireHealth = max;

    }

    public void FireReached()
    {
        if (immune)
        {
            return;
        }
        fireHealth--;
        if (fireHealth > 0)
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
