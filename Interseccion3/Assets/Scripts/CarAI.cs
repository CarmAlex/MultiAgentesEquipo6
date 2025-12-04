using UnityEngine;

public class CarAI : MonoBehaviour
{
    public Path path;
    public VehicleSpawner spawner;
    public float speed = 10f;
    public float turnSpeed = 8f;
    public float waypointThreshold = 0.3f;

    [HideInInspector] 
    public TrafficLight trafficLight;   // set by StopLineTrigger

    int currentIndex = 0;
    bool waiting = false;
    bool hasReservation = false;

    void Update()
    {
        if (!path || path.waypoints.Count == 0) return;

        //
        // 1) TRAFFIC LIGHT CHECK (highest priority)
        //
        if (trafficLight != null)
        {
            if (!trafficLight.IsGreen)
            {
                // Red or Yellow → STOP.
                waiting = true;
                return;
            }
            else
            {
                waiting = false;
            }
        }

        //
        // 2) INTERSECTION RESERVATION (only when green light or no light)
        //
        if (!string.IsNullOrEmpty(path.conflictZoneId))
        {
            // Only try to reserve if not already holding it
            if (!hasReservation)
            {
                if (IntersectionManager.Instance.TryReserve(path.conflictZoneId))
                {
                    hasReservation = true;
                    waiting = false;
                }
                else
                {
                    waiting = true;
                    return;
                }
            }
        }

        if (waiting) return;

        //
        // 3) MOVEMENT ALONG PATH
        //
        Transform target = path.waypoints[currentIndex];
        Vector3 dir = target.position - transform.position;
        Vector3 flat = new Vector3(dir.x, 0, dir.z);

        if (flat.magnitude < waypointThreshold)
        {
            currentIndex++;

            if (currentIndex >= path.waypoints.Count)
            {
                EndOfPath();
                return;
            }
        }

        Vector3 move = flat.normalized;
        transform.position += move * speed * Time.deltaTime;

        if (move.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                rot, 
                turnSpeed * Time.deltaTime
            );
        }
    }

    void EndOfPath()
    {
        if (hasReservation)
            IntersectionManager.Instance.Release(path.conflictZoneId);

        if (spawner)
            spawner.Despawn(gameObject);
        else
            Destroy(gameObject);
    }

}
