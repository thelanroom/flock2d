using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock)
    {
        //if no neighbors, maintain the current alignment
        if (context.Count == 0)
        {
            return flockAgent.transform.up;
        }


        //add all alignments and average
        var alignmentMove = Vector2.zero;
        foreach (Transform t in context)
        {
            alignmentMove += (Vector2)t.up;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
