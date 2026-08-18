using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleObjects;   
    [SerializeField]
    private float spawnRate;
    private float spawnTime;    
    private int spawnIndex;
    private int spawnCanePosition;
    [SerializeField, Range(-9, 9)] private float leftRange;
    [SerializeField, Range(-9, 9)] private float rightRange;
    [SerializeField] private float verticalRange;

    void Update()
    {
        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + spawnRate;
            spawnIndex = Random.Range(0, obstacleObjects.Length);
            if (spawnIndex == 0)
            {
                spawnCanePosition = Random.Range(0, 1);
                if (spawnCanePosition == 0)
                {
                    GameObject newCaneObject = Instantiate(obstacleObjects[spawnIndex], new Vector2(Random.Range(-9, -9), verticalRange), Quaternion.identity);
                }
                if (spawnCanePosition == 1)
                {
                    GameObject newCaneObject = Instantiate(obstacleObjects[spawnIndex], new Vector2(Random.Range(9, 9), verticalRange), Quaternion.identity);
                }
            }
                
            if (spawnIndex != 0)
            {
              GameObject newObstacleObject = Instantiate(obstacleObjects[spawnIndex], new Vector2(Random.Range(leftRange, rightRange), verticalRange), Quaternion.identity);
            }
            
        }
    } 
}
