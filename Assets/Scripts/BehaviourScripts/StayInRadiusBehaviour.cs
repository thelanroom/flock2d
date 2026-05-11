using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/StayInRadius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    public Vector2 center;
    public float radius = 10f;

    public override Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock)
    {
        var centerOffset = center - (Vector2)flockAgent.transform.position;
        float t = centerOffset.magnitude/radius;

        if(t < 0.9f)
        {
            return Vector2.zero;
        }

        return t * t * centerOffset;
    }
}
