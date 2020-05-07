using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoxCreator : MonoBehaviour
{
    public Vector3 Size;
    public List<Transform> Walls;

    public float _wallThickness = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        Walls.Clear();
        foreach(var child in transform)
        {
            Walls.Add((Transform)child);
        }
        Walls[0].localPosition = new Vector3(0, 0, Size.z / 2);
        Walls[0].localScale = new Vector3(Size.x, Size.y, _wallThickness);
        Walls[1].localPosition = new Vector3(0, 0, -Size.z / 2);
        Walls[1].localScale = new Vector3(Size.x, Size.y, _wallThickness);
        Walls[2].localPosition = new Vector3(Size.x / 2, 0, 0);
        Walls[2].localScale = new Vector3(_wallThickness, Size.y, Size.z);
        Walls[3].localPosition = new Vector3(-Size.x / 2, 0, 0);
        Walls[3].localScale = new Vector3(_wallThickness, Size.y, Size.z);
        Walls[4].localPosition = new Vector3(0, Size.y / 2, 0);
        Walls[4].localScale = new Vector3(Size.x, _wallThickness, Size.z);
        Walls[5].localPosition = new Vector3(0, -Size.y / 2, 0);
        Walls[5].localScale = new Vector3(Size.x, _wallThickness, Size.z);
    }
}
