using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singelton<Spawner>
{
    [SerializeField] private GameObject circle;
    public int minSpawnAmount;
    public int maxSpawnAmount;

    public float minCircleSize;
    public float maxCircleSize;

    Vector2 worldBoundary;

    private void Start()
    {
        worldBoundary = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        SpawnCircles();
    }

    public void SpawnCircles()
    {
        for (int i = 0; i < Random.Range(minSpawnAmount, maxSpawnAmount); i++)
        {
            float randomX = Random.Range(-worldBoundary.x, worldBoundary.x);
            float randomY = Random.Range(-worldBoundary.y, worldBoundary.y);

            Transform clone = Instantiate(circle, new Vector2(randomX, randomY), Quaternion.identity).transform;

            float randomCircleSize = Random.Range(minCircleSize, maxCircleSize);

            clone.transform.localScale = new Vector3(randomCircleSize, randomCircleSize, 0f);
            clone.transform.GetComponent<SpriteRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        }
    }
}
