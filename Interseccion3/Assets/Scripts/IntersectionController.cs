using UnityEngine;
using System.Collections.Generic;

public class IntersectionController : MonoBehaviour
{
    public List<TrafficLightController> groupA; // e.g. Ahuehuetes → Nogales
    public List<TrafficLightController> groupB; // e.g. Fernando García Roel

    public float cycleTime = 12f; // total cycle length
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < cycleTime / 2f)
        {
            // Group A green, Group B red
            SetGroupColor(groupA, LightColor.Green);
            SetGroupColor(groupB, LightColor.Red);
        }
        else if (timer < cycleTime)
        {
            // Group B green, Group A red
            SetGroupColor(groupA, LightColor.Red);
            SetGroupColor(groupB, LightColor.Green);
        }
        else
        {
            timer = 0f; // reset cycle
        }
    }

    void SetGroupColor(List<TrafficLightController> group, LightColor color)
    {
        foreach (var light in group)
        {
            if (color == LightColor.Green)
                light.SwitchToGreen();
            else
                light.SwitchToRed();
        }
    }
}
