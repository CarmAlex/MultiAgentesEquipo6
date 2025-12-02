using UnityEngine;

public class CarAgent : MonoBehaviour
{
    public float speed = 6f;
    public float stopDistance = 3f;
    public float turnSpeed = 3f;

    private CarSensor sensor;
    private Transform currentWaypoint;
    private bool isStopped = false;

    void Start()
    {
        sensor = GetComponent<CarSensor>();
        currentWaypoint = WaypointManager.Instance.GetNextWaypoint(null);
    }

    void Update()
    {
        DetectTrafficLight();
        DetectCarAhead();

        if (!isStopped)
            MoveTowardsWaypoint();
    }

    void DetectTrafficLight()
    {
        var light = sensor.GetTrafficLightAhead();

        if (light != null && light.currentColor == LightColor.Red)
        {
            float dist = Vector3.Distance(transform.position, light.transform.position);
            if (dist < stopDistance)
            {
                isStopped = true;
                return;
            }
        }

        isStopped = false;
    }

    void DetectCarAhead()
    {
        var car = sensor.GetCarAhead();

        if (car != null)
        {
            float dist = Vector3.Distance(transform.position, car.position);
            if (dist < stopDistance)
                isStopped = true;
        }
    }

    void MoveTowardsWaypoint()
    {
        if (Vector3.Distance(transform.position, currentWaypoint.position) < 2f)
        {
            currentWaypoint = WaypointManager.Instance.GetNextWaypoint(currentWaypoint);
        }

        Vector3 dir = (currentWaypoint.position - transform.position).normalized;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(dir),
            Time.deltaTime * turnSpeed
        );

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void ForceStop(bool stop)
    {
        isStopped = stop;
    }

    public bool IsStopped()
    {
        return isStopped;
    }
}
