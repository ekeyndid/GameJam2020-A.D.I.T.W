using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindBoost : MonoBehaviour
{
    public ParticleSystem part;
    public float XForce;
    public float YForce;
    public List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        print("COLLIDED WITH SOMETHING");
        if (other.CompareTag("Player"))
        {
            print("collide");
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            int i = 0;

            while (i < 1)
            {
                if (rb)
                {
                    rb.velocity += new Vector3(XForce, YForce, 0.0f);
                }
                i++;
            }
        }
    }
}