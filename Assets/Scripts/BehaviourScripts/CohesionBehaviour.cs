using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjusment
        if(context.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points and average
        var cohesionMove = Vector2.zero;
        foreach (Transform t in context)
        {
            cohesionMove += (Vector2)t.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent's position
        cohesionMove -= (Vector2)flockAgent.transform.position;
        return cohesionMove;
    }
}
