using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitalManager : MonoBehaviour
{
    public float G = 0.1f;

    Orbital[] _orbitals;

    // Start is called before the first frame update
    void Start()
    {
        _orbitals = GameObject.FindObjectsOfType<Orbital>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate new velocities for all the orbitals
        var newVelocities = new List<Vector3>();
        foreach (var orbital in _orbitals)
        {
            var newVelocity = new Vector3(0, 0, 0);
            foreach (var otherOrbital in _orbitals)
            {
                if(orbital == otherOrbital)
                {
                    continue;
                }
                var dir = otherOrbital.transform.position - orbital.transform.position;
                var r = dir.magnitude;

                var mass = orbital.Mass * otherOrbital.Mass;
                var force = G * mass / (r * r);
                var acceleration = force / orbital.Mass;

                // Gravity
                newVelocity += acceleration * dir.normalized * Time.deltaTime;
            }
            newVelocities.Add(newVelocity);
        }

        // Apply the new velocities to all the orbitals
        foreach (var (orbital, velocity) in _orbitals.Zip(newVelocities, (o, v) => (o, v)))
        {
            orbital.Velocity += velocity;
        }

        foreach (var orbital in _orbitals)
        {
            orbital.transform.position += orbital.Velocity;
        }
    }
}
