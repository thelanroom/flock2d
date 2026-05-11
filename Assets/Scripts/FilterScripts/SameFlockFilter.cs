using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        var filtered = new List<Transform>();

        foreach (Transform t in original) {
        
            var iAgent = t.GetComponent<FlockAgent>();
            if(iAgent != null && iAgent.ParentFlock == agent.ParentFlock)
            {
                filtered.Add(t);
            }
        }

        return filtered;
    }
}
