using UnityEngine;

public class SimplePathFollower : MonoBehaviour
{
    public Path path;
    public float speed = 8f;
    public float turnSpeed = 8f;
    public float waypointThreshold = 0.2f;
    public bool loop = false;

    int currentIndex = 0;

    void Start()
    {
        if (path != null && path.waypoints.Count > 0)
        {
            // snap to first waypoint at start, optional
            transform.position = path.waypoints[0].position;
        }
    }

    void Update()
    {
        if (path == null || path.waypoints.Count == 0) return;

        Transform target = path.waypoints[currentIndex];
        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - transform.position;
        Vector3 flatDir = new Vector3(dir.x, 0f, dir.z);

        if (flatDir.magnitude < waypointThreshold)
        {
            currentIndex++;

            if (currentIndex >= path.waypoints.Count)
            {
                if (loop)
                {
                    currentIndex = 0;
                }
                else
                {
                    // stop at final waypoint
                    enabled = false;
                }
                return;
            }
        }

        Vector3 moveDir = flatDir.normalized;
        transform.position += moveDir * speed * Time.deltaTime;

        if (moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                turnSpeed * Time.deltaTime
            );
        }
    }
}
