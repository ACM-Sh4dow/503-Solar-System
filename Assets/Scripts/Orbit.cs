using System.Linq;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] Vector3 startingVelocity = new Vector3(0, 0, 0);
    Rigidbody[] otherOrbiters;
    const float constGravity = 10;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = startingVelocity;
        OrbiterCreator.Instance.objectCreated += updateOrbiters;
    }

    private void Start()
    {
        updateOrbiters();
    }

    private void FixedUpdate()
    {
        Vector3 currentVelocity = rb.linearVelocity;

        foreach (Rigidbody orbiter in otherOrbiters)
        {
            currentVelocity += CalculateOrbit(orbiter);
        }
        rb.linearVelocity = currentVelocity;
    }

    private Vector3 CalculateOrbit(Rigidbody orbiter)
    {
        Vector3 routeToOrbiter = orbiter.position - rb.position;
        Vector3 directionToOrbiter = routeToOrbiter.normalized;

        float impactMagnitude = orbiter.mass * constGravity / Mathf.Pow(routeToOrbiter.magnitude, 2f);

        return directionToOrbiter * impactMagnitude;
    }

    private void updateOrbiters()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Orbiter");
        otherOrbiters = new Rigidbody[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            otherOrbiters[i] = objects[i].GetComponent<Rigidbody>();
        }
    }
}
