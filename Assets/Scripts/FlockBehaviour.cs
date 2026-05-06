using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehaviour : ScriptableObject
{
    public abstract Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock);
}
