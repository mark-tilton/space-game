using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital : MonoBehaviour
{
    public float Mass = 1;

    public Vector3 Velocity = new Vector3(0, 0, 0);

    public TrailRenderer TrailRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        TrailRenderer = gameObject.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var r = transform.localScale.x / 2;
        Mass = 4f / 3f * Mathf.PI * r * r * r;
    }
}
