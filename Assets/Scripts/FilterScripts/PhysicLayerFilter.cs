using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/PhysicLayer")]
public class PhysicLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        var filtered = new List<Transform>();

        foreach (Transform t in original)
        {
            if(mask == (mask | 1 << t.gameObject.layer))
            {
                filtered.Add(t);
            }
            
        }

        return filtered;
    }
}
