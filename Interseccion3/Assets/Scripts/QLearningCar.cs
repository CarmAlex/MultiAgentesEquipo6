using UnityEngine;

public class QLearningCar : MonoBehaviour
{
    // States: 0 = Red Light, 1 = Green Light
    // Actions: 0 = Stop, 1 = Move

    private float[,] Q = new float[2, 2];

    public float alpha = 0.1f;   // learning rate
    public float gamma = 0.9f;   // discount factor
    public float epsilon = 0.1f; // exploration rate

    private CarAgent agent;
    private CarSensor sensor;

    void Start()
    {
        agent = GetComponent<CarAgent>();
        sensor = GetComponent<CarSensor>();
    }

    void Update()
    {
        int state = GetState();
        int action = ChooseAction(state);

        ExecuteAction(action);

        float reward = GetReward();
        int nextState = GetState();

        UpdateQ(state, action, reward, nextState);
    }

    int GetState()
    {
        var light = sensor.GetTrafficLightAhead();
        if (light != null && light.currentColor == LightColor.Red)
            return 0; // red light state

        return 1; // green light or no light
    }

    int ChooseAction(int s)
    {
        if (Random.value < epsilon)
            return Random.Range(0, 2); // explore

        // exploit best known action
        return (Q[s, 1] > Q[s, 0]) ? 1 : 0;
    }

    void ExecuteAction(int a)
    {
        if (a == 0)
            agent.ForceStop(true);  // stop car
        else
            agent.ForceStop(false); // allow movement
    }

    float GetReward()
    {
        var light = sensor.GetTrafficLightAhead();

        // Penalty for running red light
        if (light != null && light.currentColor == LightColor.Red && !agent.IsStopped())
            return -5f;

        // Small reward for moving legally
        return 0.1f;
    }

    void UpdateQ(int s, int a, float r, int s2)
    {
        float maxNext = Mathf.Max(Q[s2, 0], Q[s2, 1]);
        Q[s, a] += alpha * (r + gamma * maxNext - Q[s, a]);
    }
}
