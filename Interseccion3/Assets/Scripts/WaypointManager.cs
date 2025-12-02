using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    [System.Serializable]
    public class WaypointBranch
    {
        public Transform waypoint;
        public string direction; // "left", "right", "straight"
        public float probability; // 0 to 1
    }

    [System.Serializable]
    public class WaypointNode
    {
        public Transform current;
        public List<WaypointBranch> branches;
    }

    public List<WaypointNode> nodes;

    void Awake()
    {
        Instance = this;
    }

    public Transform GetNextWaypoint(Transform current)
    {
        foreach (var node in nodes)
        {
            if (node.current == current)
            {
                float roll = Random.value;
                float cumulative = 0f;

                foreach (var branch in node.branches)
                {
                    cumulative += branch.probability;
                    if (roll <= cumulative)
                        return branch.waypoint;
                }

                // fallback to first branch
                return node.branches[0].waypoint;
            }
        }

        // fallback to first node if current is null
        return nodes.Count > 0 ? nodes[0].current : null;
    }
}
