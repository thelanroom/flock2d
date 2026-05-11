using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public static float RotationSpeed =180;
    Collider2D _agentCollider;

    public Flock ParentFlock { get; private set;  }
    public Collider2D AgentCollider {  get { return _agentCollider; } }
    public SpriteRenderer SpriteRenderer { get; private set;  }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _agentCollider = GetComponent<Collider2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(Flock flock)
    {
        ParentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        // Normalize velocity to avoid scaling issues
        Vector3 targetDirection = velocity.normalized;

        // Create the target rotation that points 'up' in the velocity direction
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);

        // Smoothly rotate from current rotation toward target rotation
        
        transform.SetPositionAndRotation(transform.position + (Vector3)velocity * Time.deltaTime, Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            RotationSpeed * Time.deltaTime));
    }
}
