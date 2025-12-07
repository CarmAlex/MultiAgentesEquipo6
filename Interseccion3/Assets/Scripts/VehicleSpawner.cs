using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class VehicleEntry
    {
        public GameObject prefab;
        public float weight = 1f;
    }

    public List<VehicleEntry> vehicles = new List<VehicleEntry>();
    public Path path;
    public float spawnInterval = 3f;
    public int maxAlive = 10;

    private float timer;
    private List<GameObject> aliveVehicles = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && aliveVehicles.Count < maxAlive)
        {
            SpawnVehicle();
            timer = 0f;
        }
    }

    void SpawnVehicle()
    {
        GameObject prefab = ChooseWeightedPrefab();

        // Base spawn pos
        Vector3 spawnPos = transform.position;

        // --- APPLY SPECIAL OFFSETS FOR CAMION ---
        if (prefab.name.ToLower().Contains("camion"))
        {
            // Move forward to avoid spawning too far back
            spawnPos += transform.forward * 5.5f;

            // Move downward to fix floating
            // Adjust this value if needed (start with -1f to -2f)
            spawnPos += Vector3.down * 1.5f;
        }
        // ---------------------------------------

        GameObject obj = Instantiate(prefab, spawnPos, transform.rotation);

        aliveVehicles.Add(obj);

        // Setup AI
        CarAI ai = obj.GetComponent<CarAI>();
        ai.path = path;
        ai.spawner = this;
    }

    public void Despawn(GameObject vehicle)
    {
        aliveVehicles.Remove(vehicle);
        Destroy(vehicle);
    }

    GameObject ChooseWeightedPrefab()
    {
        float total = 0f;
        foreach (var v in vehicles)
            total += v.weight;

        float r = Random.value * total;

        foreach (var v in vehicles)
        {
            if (r < v.weight)
                return v.prefab;
            r -= v.weight;
        }

        return vehicles[0].prefab; // fallback
    }
}
