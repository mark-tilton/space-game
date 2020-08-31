using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ship : Orbital
{
    private Thruster[] _thrusters;

    // Start is called before the first frame update
    void Start()
    {
        _thrusters = gameObject.GetComponentsInChildren<Thruster>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var thruster in _thrusters)
        {
            Velocity += thruster.ThrustAmount * thruster.transform.up * Time.deltaTime;
        }

        //transform.Rotate(new Vector3(0, 0, 5 * Time.deltaTime));
    }
}
