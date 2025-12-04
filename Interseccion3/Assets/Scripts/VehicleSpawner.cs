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
        GameObject obj = Instantiate(prefab, transform.position, transform.rotation);

        aliveVehicles.Add(obj);

        // ✨ VERY IMPORTANT ✨
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
