using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Path path;
    public float spawnInterval = 3f;
    public int maxCarsAlive = 10;

    float timer;
    List<CarAI> cars = new List<CarAI>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && cars.Count < maxCarsAlive)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        GameObject obj = Instantiate(carPrefab, transform.position, transform.rotation);
        CarAI ai = obj.GetComponent<CarAI>();

        ai.path = path;
        ai.spawner = this;

        cars.Add(ai);
    }

    public void Despawn(CarAI ai)
    {
        cars.Remove(ai);
        Destroy(ai.gameObject);
    }
}
