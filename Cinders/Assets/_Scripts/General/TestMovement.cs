using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.left * 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 5)
        {
            rb.velocity = Vector3.left * 5;
        }
        if (transform.position.x < -5)
        {
            rb.velocity = Vector3.right * 5;
        }
    }
}
