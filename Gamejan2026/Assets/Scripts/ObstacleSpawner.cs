using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleObjects;   
    [SerializeField]
    private float spawnRate;
    private float spawnTime;    
    private int spawnIndex;
    [SerializeField, Range(-9, 9)] private float leftRange;
    [SerializeField, Range(-9, 9)] private float rightRange;
    [SerializeField] private float verticalRange;

    void Update()
    {
        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + spawnRate;
            spawnIndex = Random.Range(0, obstacleObjects.Length);
            GameObject newCaneObject = Instantiate(obstacleObjects[spawnIndex], new Vector2(Random.Range(leftRange, rightRange), verticalRange), Quaternion.identity);
        }
    } 
}
