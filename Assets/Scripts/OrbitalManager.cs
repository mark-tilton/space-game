using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitalManager : MonoBehaviour
{
    public float G = 0.1f;
    public float TrailStep = 0.1f;
    public float StepCount = 500;

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
        foreach(var orbitalData in GetNextFrame(currentFrame, TrailStep))
        {
            orbitalData.Apply();
        }

        foreach(var orbital in _orbitals)
        {
            orbital.TrailRenderer.Clear();
        }
        
        var previousFrame = currentFrame;
        for(var i = 0; i < StepCount; i++)
        {
            var nextFrame = GetNextFrame(previousFrame, TrailStep);
            for (var j = 0; j <= 10; j++)
            {
                nextFrame = GetNextFrame(nextFrame, TrailStep);
            }
            foreach(var (prev, next) in previousFrame.Zip(nextFrame, (p, n) => (p, n)))
            {
                prev.Orbital.TrailRenderer.AddPosition(next.Position);
            }
            previousFrame = nextFrame;
        }
    }

    private List<OrbitalData> GetNextFrame(List<OrbitalData> _orbitalData, float step)
    {
        // Calculate new velocities for all the orbitals
        var nextFrame = new List<OrbitalData>();
        foreach (var orbital in _orbitalData)
        {
            var newVelocity = new Vector3(0, 0, 0);
            foreach (var otherOrbital in _orbitalData)
            {
                if(orbital.Orbital == otherOrbital.Orbital)
                {
                    continue;
                }
                var dir = otherOrbital.Position - orbital.Position;
                var r = dir.magnitude;

                var mass = orbital.Mass * otherOrbital.Mass;
                var force = G * mass / (r * r);
                var acceleration = force / orbital.Mass;

                // Gravity
                newVelocity += acceleration * dir.normalized * step;
            }
            var newOrbitalData = orbital.Add(newVelocity);
            nextFrame.Add(newOrbitalData);
        }
        return nextFrame;
    }
}

public class OrbitalData
{
    public Orbital Orbital { get; }

    public float Mass => Orbital.Mass;

    public Vector3 Position { get; private set; }

    public Vector3 Velocity { get; private set; }

    public OrbitalData(Orbital orbital)
    {
        Orbital = orbital;
        Velocity = orbital.Velocity;
        Position = orbital.transform.position;
    }

    public OrbitalData Add(Vector3 velocity)
    {
        var newOrbital = new OrbitalData(Orbital);
        newOrbital.Velocity = Velocity + velocity;
        newOrbital.Position = Position + Velocity;
        return newOrbital;
    }

    public void Apply()
    {
        Orbital.Velocity = Velocity;
        Orbital.transform.position = Position;
    }
}