using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitalManager : MonoBehaviour
{
    public float G = 0.1f;
    public float TimeStep = 0.01f;

    Orbital[] _orbitals;

    // Start is called before the first frame update
    void Start()
    {
        _orbitals = FindObjectsOfType<Orbital>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentFrame = _orbitals.Select(x => new OrbitalData(x)).ToList();
        foreach (var orbitalData in GetNextFrame(currentFrame, TimeStep))
        {
            orbitalData.Apply();
        }
    }

    public List<OrbitalData> GetNextFrame(List<OrbitalData> _orbitalData, float step)
    {
        var nextFrame = new List<OrbitalData>();
        foreach (var orbital in _orbitalData)
        {
            var totalAcceleration = new Vector3(0, 0, 0);
            foreach (var otherOrbital in _orbitalData)
            {
                if (orbital.Orbital == otherOrbital.Orbital)
                {
                    continue;
                }

                var dir = otherOrbital.Position - orbital.Position;
                var sqrDst = dir.sqrMagnitude;
                if(sqrDst < 1e-3)
                {
                    continue;
                }
                var force = G * otherOrbital.Mass /* * orbital.Mass*/ / sqrDst;
                var acceleration = force; // / orbital.Mass

                totalAcceleration += acceleration * dir.normalized;
            }
            var newOrbitalData = orbital.Add(totalAcceleration, step);
            nextFrame.Add(newOrbitalData);
        }
        return nextFrame;
    }
}