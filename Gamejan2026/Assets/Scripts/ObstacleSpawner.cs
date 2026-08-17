using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject caneObject;
    [SerializeField] private GameObject plasticBagObject;
    [SerializeField] private GameObject a;
    [SerializeField]
    private float spawnRate;
    private float spawnTime;
    private int spawnIndex = 0;
    public enum IndexSpawn
    {
        Cane,
        PlasticBag,
        Web
    }
    void Update()
    {
        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + spawnRate;
            spawnIndex++;

        }
    }
    public void SetState(IndexSpawn newindex)
    {
        //O primeiro swwitch é para simular um OnTriggerExit, onde o inimigo para de fazer algo relacionado ao estado anterior, e o segundo switch é para simular um OnTriggerEnter, onde o inimigo começa a fazer algo relacionado ao novo estado.
        switch (spawnIndex)
        {
            case (int)IndexSpawn.Cane:
                GameObject newCaneObject = Instantiate(caneObject, new Vector3(Random.Range(-4f, 4), Random.Range(-4.5f, 4), 0), Quaternion.identity);
                break;
            case (int)IndexSpawn.PlasticBag:


                break;
            case (int)IndexSpawn.Web:

                break;
        }
        spawnIndex = (int)newindex;// Atualiza o estado atual para o novo estado
        // O segundo switch é para simular um OnTriggerEnter, onde o inimigo começa a fazer algo relacionado ao novo estado.
        switch (spawnIndex)
        {
            case (int)IndexSpawn.Cane:

                break;
            case (int)IndexSpawn.PlasticBag:

                break;
            case (int)IndexSpawn.Web:

                break;
        }
    }
}
