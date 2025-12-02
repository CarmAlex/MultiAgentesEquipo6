using UnityEngine;
using System.Collections.Generic;

public class CarSensor : MonoBehaviour
{
    public float rayDistance = 15f;
    public LayerMask detectionMask; // Assign "TrafficLight" and "Car" layers in Unity

    // Detect the closest traffic light ahead
    public TrafficLightController GetTrafficLightAhead()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, detectionMask))
        {
            var light = hit.collider.GetComponent<TrafficLightController>();
            if (light != null)
                return light;
        }

        return null;
    }

    // Detect the closest car ahead
    public Transform GetCarAhead()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, detectionMask))
        {
            if (hit.collider.CompareTag("Car"))
                return hit.collider.transform;
        }

        return null;
    }

    // Optional: detect all lights ahead (for debugging or advanced logic)
    public List<TrafficLightController> GetAllTrafficLightsAhead()
    {
        List<TrafficLightController> lights = new List<TrafficLightController>();
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance, detectionMask);
        foreach (var h in hits)
        {
            var light = h.collider.GetComponent<TrafficLightController>();
            if (light != null)
                lights.Add(light);
        }

        return lights;
    }
}
