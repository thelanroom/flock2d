using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock)
    {
        var filteredContext = filter == null ? context : filter.Filter(flockAgent, context);

        //if no neighbors, maintain the current alignment
        if (filteredContext.Count == 0)
        {
            return flockAgent.transform.up;
        }


        //add all alignments and average
        var alignmentMove = Vector2.zero;

        foreach (Transform t in filteredContext)
        {
            alignmentMove += (Vector2)t.up;
        }
        alignmentMove /= filteredContext.Count;

        return alignmentMove;
    }
}
