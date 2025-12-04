using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public TrafficLight northLight;
    public TrafficLight southLight;
    public TrafficLight eastLight;
    public TrafficLight westLight;

    public float greenTime = 6f;
    public float yellowTime = 2f;
    public float redTime = 6f;

    float timer;
    int phase = 0;

    void Update()
    {
        timer += Time.deltaTime;

        switch (phase)
        {
            
            case 0:
                if (timer >= greenTime) { SetNorthSouth(LightState.Yellow); phase = 1; timer = 0; }
                break;

            case 1:
                if (timer >= yellowTime) { SetNorthSouth(LightState.Red); SetEastWest(LightState.Green); phase = 2; timer = 0; }
                break;

            
            case 2:
                if (timer >= greenTime) { SetEastWest(LightState.Yellow); phase = 3; timer = 0; }
                break;

            case 3:
                if (timer >= yellowTime) { SetEastWest(LightState.Red); SetNorthSouth(LightState.Green); phase = 0; timer = 0; }
                break;
        }
    }

    void SetNorthSouth(LightState s)
    {
        northLight.state = s;
        southLight.state = s;
    }

    void SetEastWest(LightState s)
    {
        eastLight.state = s;
        westLight.state = s;
    }
}
