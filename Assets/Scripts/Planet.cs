using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Planet : MonoBehaviour
{
    public float R = 2;

    private Orbital _orbital = null;

    // Start is called before the first frame update
    void Start()
    {
        _orbital = gameObject.GetComponent<Orbital>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(R * 2, R * 2, R * 2);
        if (_orbital != null)
        {
            _orbital.Mass = 4f / 3f * Mathf.PI * R * R * R;
        }
    }
}
