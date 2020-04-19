using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrbitScript : MonoBehaviour
{
    public Vector3 Velocity;

    public float Mass;

    public static float G = 15;
    
    private List<OrbitScript> _otherOrbitals;

    private HashSet<OrbitScript> _attached = new HashSet<OrbitScript>();

    // Start is called before the first frame update
    void Start()
    {
        _otherOrbitals = GameObject.FindGameObjectsWithTag("Orbital").Select(x => x.GetComponent<OrbitScript>()).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        var sum = _attached.Aggregate(Velocity, (v, a) => v + a.Velocity);
        var average = sum / (_attached.Count + 1);
        Velocity = average;
        foreach(var attached in _attached.ToList())
        {
            attached.Velocity = average;
            var dir = attached.transform.position - transform.position;
            var r = dir.magnitude;
            if(r > 1)
            {
                _attached.Remove(attached);
            }
        }

        transform.position += Velocity * Time.deltaTime * G;
        foreach(var orbital in _otherOrbitals)
        {
            // Skip ourselves
            if(orbital.transform == transform || _attached.Contains(orbital))
            {
                continue;
            }

            var dir = orbital.transform.position - transform.position;
            var r = dir.magnitude;

            // Collision
            if(r <= 1)
            {
                //_attached.Add(orbital);
                //continue;
            }

            // Gravity
            Velocity += 1 / (r * r) * dir.normalized * G * Time.deltaTime;
        }
    }

    private void BroadcastAttached(OrbitScript sender)
    {

    }
}
