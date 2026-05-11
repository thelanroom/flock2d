using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    Vector2 _currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock)
    {
        var filteredContext = filter == null ? context : filter.Filter(flockAgent, context);

        //if no neighbors, return no adjusment
        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points and average
        var cohesionMove = Vector2.zero;

        foreach (Transform t in filteredContext)
        {
            cohesionMove += (Vector2)t.position;
        }
        cohesionMove /= filteredContext.Count;

        //create offset from agent's position
        cohesionMove -= (Vector2)flockAgent.transform.position;
        cohesionMove = Vector2.SmoothDamp(flockAgent.transform.up, cohesionMove, ref _currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
