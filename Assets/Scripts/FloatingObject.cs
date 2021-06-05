using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float randomRange = 0.01f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 velocity = Random.insideUnitCircle.normalized * Random.Range(-randomRange, randomRange);
        rb.AddForce(velocity, ForceMode.VelocityChange);
    }
}
