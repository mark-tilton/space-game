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

    public OrbitalData Add(Vector3 acceleration, float step)
    {
        var newOrbital = new OrbitalData(Orbital);
        newOrbital.Velocity = Velocity + acceleration * step;
        newOrbital.Position = Position;// + Velocity * step + 1/2 * acceleration * step * step;
        return newOrbital;
    }

    public void SetPosition(float step)
    {
        Position = Position + Velocity * step;
    }

    public void Apply()
    {
        Orbital.Velocity = Velocity;
        Orbital.transform.position = Position;
    }
}
