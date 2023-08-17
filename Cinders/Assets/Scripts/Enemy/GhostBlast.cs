using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlast : MonoBehaviour
{
    [SerializeField] AnimationCurve SizeCurve;
    private bool launch = false;
    private float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Fire(float speed = 2)
    {
        projectileSpeed = speed;
        launch = true;
    }

    public void Charge(float chargeTime)
    {
        StartCoroutine(Expand(chargeTime));
    }

    IEnumerator Expand(float chargeTime)
    {
        float time = 0;
        while (time < chargeTime)
        {
            float size = SizeCurve.Evaluate(time / chargeTime);
            transform.localScale = new Vector3(size, size, size);
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

    }

    public void Hurt(int damage)
    {
        Destroy(gameObject);
    }

    protected void FixedUpdate()
    {
        if (launch)
        {
            transform.LookAt(Vector3.up);
            transform.position += projectileSpeed * Time.deltaTime * transform.forward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Campfire"))
        {
            // Damage Campfire
            Destroy(gameObject);
        }
    }
}
