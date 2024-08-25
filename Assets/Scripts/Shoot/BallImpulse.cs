using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpulse : MonoBehaviour
{
    [SerializeField] float impulso;

    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce((gameObject.transform.forward + Vector3.up).normalized * impulso, ForceMode.Impulse);
    }

}
