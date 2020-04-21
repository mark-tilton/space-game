using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitViewer : MonoBehaviour
{
    public int StepCount = 500;

    private OrbitalManager _orbitalManager;

    // Start is called before the first frame update
    void Start()
    {
        _orbitalManager = gameObject.GetComponent<OrbitalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var orbitals = FindObjectsOfType<Orbital>();
        var currentFrame = orbitals.Select(x => new OrbitalData(x)).ToList();

        var previousFrame = currentFrame;
        for (var i = 0; i < StepCount; i++)
        {
            var nextFrame = previousFrame;
            for(int j = 0; j < 1; j++)
            {
                nextFrame = _orbitalManager.GetNextFrame(nextFrame, _orbitalManager.TimeStep);
            }
            foreach (var (prev, next) in previousFrame.Zip(nextFrame, (p, n) => (p, n)))
            {
                Debug.DrawLine(prev.Position, next.Position);
            }
            previousFrame = nextFrame;
        }
    }
}
