using UnityEngine;

public enum LightColor { Red, Green }

public class TrafficLightController : MonoBehaviour
{
    public LightColor currentColor = LightColor.Red;
    public float greenTime = 6f;
    public float redTime = 6f;

    private float timer = 0f;

    // Optional grouping ID for coordination
    public string groupId = "default";

    void Update()
    {
        timer += Time.deltaTime;

        if (currentColor == LightColor.Green && timer >= greenTime)
        {
            SwitchToRed();
        }
        else if (currentColor == LightColor.Red && timer >= redTime)
        {
            SwitchToGreen();
        }
    }

    public void SwitchToRed()
    {
        currentColor = LightColor.Red;
        timer = 0f;
    }

    public void SwitchToGreen()
    {
        currentColor = LightColor.Green;
        timer = 0f;
    }
}
