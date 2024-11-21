using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryVisualizer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private int maxBounceCount = 5;
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private LayerMask bounceLayers;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 startPoint, Vector3 direction, float force, float sphereRadius)
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 currentVelocity = direction.normalized * force;
        int bounces = 0;

        points.Add(startPoint);

        while (bounces < maxBounceCount)
        {
            RaycastHit hit;
            if (Physics.SphereCast(startPoint, sphereRadius, currentVelocity.normalized, out hit, maxDistance, bounceLayers))
            {
                // Add the point where it hits
                Vector3 hitPoint = hit.point;
                points.Add(hitPoint);

                // Reflect the velocity
                currentVelocity = Vector3.Reflect(currentVelocity, hit.normal);

                // Update the current position
                startPoint = hitPoint;

                bounces++;
            }

            else
            {
                // If no collision, calculate the endpoint
                points.Add(startPoint + currentVelocity.normalized * maxDistance);
                break;

            }
        }

        // Set points to the LineRenderer
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}

