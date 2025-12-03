using UnityEngine;

public class StopLineTrigger : MonoBehaviour
{
    public TrafficLight assignedLight;   // drag your light here

    void OnTriggerEnter(Collider other)
    {
        CarAI ai = other.GetComponent<CarAI>();
        if (!ai) return;

        ai.trafficLight = assignedLight;
    }

    void OnTriggerExit(Collider other)
    {
        CarAI ai = other.GetComponent<CarAI>();
        if (!ai) return;

        ai.trafficLight = null;   // once past the stop line, car follows intersection logic
    }
}
