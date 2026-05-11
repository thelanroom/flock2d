using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock)
    {
        var filteredContext = filter == null ? context : filter.Filter(flockAgent, context);

        //if no neighbors, return no adjusment
        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points and average
        var avoidanceMove = Vector2.zero;
        var nAvoid = 0;// number of neighbors to avoid

        foreach (Transform t in filteredContext)
        {
            if (Vector2.SqrMagnitude(t.position - flockAgent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(flockAgent.transform.position - t.position);
            }
        }

        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}
