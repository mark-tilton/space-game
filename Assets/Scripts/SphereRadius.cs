using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SphereRadius : MonoBehaviour
{
    public float R = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(R * 2, R * 2, R * 2);
    }
}
