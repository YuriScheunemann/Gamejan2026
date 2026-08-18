using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float verticalRange;
    [Header("GeneralObstaclesConfigs")]
    private int spawnCanePosition;
    [SerializeField, Range(-9, 9)] private float leftRange;
    [SerializeField, Range(-9, 9)] private float rightRange;
    [SerializeField] private GameObject[] obstacleObjects;

    [SerializeField]
    private float spawnRate;
    private float spawnTime;
    private int obstaclesSpawnIndex;

    [Header("Canes")]    
    [SerializeField] private float spawnRateCane;
    private SpriteRenderer _spriteRendererCane;
    [SerializeField] private GameObject caneObjects;
    void Update()
    {
        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + spawnRate;
            obstaclesSpawnIndex = Random.Range(0, obstacleObjects.Length);

            if (obstaclesSpawnIndex != 0)
            {
                GameObject newObstacleObject = Instantiate(obstacleObjects[obstaclesSpawnIndex], new Vector2(Random.Range(leftRange, rightRange), verticalRange), Quaternion.identity);
            }

        }
        if (Time.time > spawnTime)
        {
            spawnCanePosition = Random.Range(0, 1);
            if (spawnCanePosition == 0)
            {
                GameObject newCaneObject = Instantiate(caneObjects, new Vector2(Random.Range(-9, -9), verticalRange), Quaternion.identity);
            }
            if (spawnCanePosition == 1)
            {
                GameObject newCaneObject = Instantiate(caneObjects, new Vector2(Random.Range(9, 9), verticalRange), transform.rotation);
                _spriteRendererCane = newCaneObject.GetComponent<SpriteRenderer>();
                _spriteRendererCane.flipX = true;
            }
        }

    }
}