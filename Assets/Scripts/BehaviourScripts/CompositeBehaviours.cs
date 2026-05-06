using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviours : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock)
    {
        if (weights.Length != behaviours.Length)
        {
            Debug.LogError("Behaviours mismatch in: " + name, this);
            return Vector2.zero;
        }

        var move = Vector2.zero;

        for (int i = 0; i < behaviours.Length; i++)
        {
            {
                var partialMove = behaviours[i].CalculateMove(flockAgent, context, flock) * weights[i];

                if (partialMove != Vector2.zero)
                {
                    if(partialMove.sqrMagnitude > weights[i] * weights[i])
                    {
                        partialMove.Normalize();
                        partialMove *= weights[i];
                    }

                    move += partialMove;
                }
            }

        }

        return move;    

    }
}
