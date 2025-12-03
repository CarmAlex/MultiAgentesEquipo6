using System.Collections.Generic;
using UnityEngine;

public class IntersectionManager : MonoBehaviour
{
    public static IntersectionManager Instance { get; private set; }

    private HashSet<string> activeZones = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool TryReserve(string zone)
    {
        if (string.IsNullOrEmpty(zone)) return true;
        if (activeZones.Contains(zone)) return false;

        activeZones.Add(zone);
        return true;
    }

    public void Release(string zone)
    {
        if (string.IsNullOrEmpty(zone)) return;
        activeZones.Remove(zone);
    }
}
