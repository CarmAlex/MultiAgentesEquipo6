using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform[] spawnPoints;

    public AnimationCurve spawnRateCurve; // Controls spawn rate over time
    public float simulationStartTime = 7f; // 07:00
    public float simulationEndTime = 10f;  // 10:00

    private float elapsedTime = 0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCar), 1f, 1f); // Initial interval, dynamically adjusted
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    void SpawnCar()
    {
        float currentHour = simulationStartTime + (elapsedTime / 3600f);
        float normalizedTime = Mathf.InverseLerp(simulationStartTime, simulationEndTime, currentHour);
        float spawnInterval = Mathf.Lerp(6f, 1f, spawnRateCurve.Evaluate(normalizedTime)); // 6s to 1s

        CancelInvoke(nameof(SpawnCar));
        InvokeRepeating(nameof(SpawnCar), spawnInterval, spawnInterval);

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(carPrefab, point.position, point.rotation);
    }
}
