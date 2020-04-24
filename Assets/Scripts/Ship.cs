using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Thruster[] _thrusters;
    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _thrusters = gameObject.GetComponentsInChildren<Thruster>();
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var thruster in _thrusters)
        {
            _rigidBody.AddForceAtPosition(
                thruster.transform.up * thruster.ThrustAmount,
                thruster.transform.position,
                ForceMode.Force);
        }
    }
}
