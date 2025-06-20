using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject projectile;
    public float force;
    public float interval;
    private Vector3 shootVelocity;
    private bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot)
        {
            GameObject go = Instantiate(projectile, this.transform);
            go.transform.parent = null;
            go.GetComponent<Rigidbody>().AddForce(shootVelocity * force);
            shoot = false;
            Destroy(go, 5);
        }
    }

    public void Shoot(Vector3 velocity)
    {
        shootVelocity = velocity;
        shoot = true;
    }
}
