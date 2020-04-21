using UnityEngine;

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

    public OrbitalData Add(Vector3 velocity, float step)
    {
        var newOrbital = new OrbitalData(Orbital);
        newOrbital.Velocity = Velocity + velocity * step;
        newOrbital.Position = Position + Velocity * step;
        return newOrbital;
    }

    public void Apply()
    {
        Orbital.Velocity = Velocity;
        Orbital.transform.position = Position;
    }
}
