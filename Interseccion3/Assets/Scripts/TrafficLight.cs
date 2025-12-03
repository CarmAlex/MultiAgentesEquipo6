using UnityEngine;

public enum LightState { Red, Yellow, Green }

public class TrafficLight : MonoBehaviour
{
    public LightState state = LightState.Red;

    public bool IsGreen => state == LightState.Green;
    public bool IsRed => state == LightState.Red;
    public bool IsYellow => state == LightState.Yellow;

    // Optional: assign materials/colors here
    public Renderer lightRenderer;
    public Material redMat;
    public Material yellowMat;
    public Material greenMat;

    void Update()
    {
        if (!lightRenderer) return;

        switch (state)
        {
            case LightState.Red: lightRenderer.material = redMat; break;
            case LightState.Yellow: lightRenderer.material = yellowMat; break;
            case LightState.Green: lightRenderer.material = greenMat; break;
        }
    }
}
