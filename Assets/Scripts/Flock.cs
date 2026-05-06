using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class Flock : MonoBehaviour
{
    [Header("Refs")]
    public FlockAgent agentPrefab;
    public FlockBehavior flockBehavior;

    [Header("Settings")]
    [Range(10, 1000)]
    public int startingSize = 100;
    public const float agentDensity = 0.08f;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    private float _squareMaxSpeed;
    private float _squareNeighborRadius;
    private float _squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return _squareAvoidanceRadius; } }

    private List<FlockAgent> _agents = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _squareMaxSpeed = maxSpeed * maxSpeed;
        _squareNeighborRadius = neighborRadius * neighborRadius;
        _squareAvoidanceRadius = _squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingSize; i++)
        {
            var newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingSize * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );

            newAgent.name = "Agent " + i;
            _agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var agent in _agents)
        {
            var context = GetNearbyObjects(agent);
            //agent.SpriteRenderer.color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            Vector2 move = flockBehavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > _squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> nearbyObjects = new();
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (var collider in nearbyColliders)
        {
            if (collider != agent.AgentCollider)
                nearbyObjects.Add(collider.transform);
        }

        return nearbyObjects;
    }
}
