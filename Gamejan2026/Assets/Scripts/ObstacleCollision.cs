using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] [Range(0, 2)] private int obstacleIndex;
    bool Iscane = false;

    public enum IndexSpawn
    {
        Cane,
        PlasticBag,
        Web
    }
    private void Start()
    {
        SetState(newindex: (IndexSpawn)obstacleIndex);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //BackgroundVelocity - velocityy
        //boatVelocity - velocityy       
        if(collision.collider.CompareTag("Destroyer"))
            Destroy(gameObject);

        if (collision.collider.CompareTag("Player") && !Iscane)
            print("a");
            //backgroundVelocity - velocityy
            //SceneManager.LoadScene(sceneName);

        if(collision.collider.CompareTag("Player") && Iscane)
            SceneManager.LoadScene(sceneName);
    }
    public void SetState(IndexSpawn newindex)
    {
        //O primeiro swwitch é para simular um OnTriggerExit, onde o inimigo para de fazer algo relacionado ao estado anterior, e o segundo switch é para simular um OnTriggerEnter, onde o inimigo começa a fazer algo relacionado ao novo estado.
        switch (obstacleIndex)
        {
            case (int)IndexSpawn.Cane:              
                Iscane = false;
                break;
            case (int)IndexSpawn.PlasticBag:
                // velocityy = -1
                break;
            case (int)IndexSpawn.Web:
                // velocityy = -2
                break;
        }

        // O segundo switch é para simular um OnTriggerEnter, onde o inimigo começa a fazer algo relacionado ao novo estado.
        switch (obstacleIndex)
        {           
            case (int)IndexSpawn.Cane:
                Iscane = true;              
                break;
            case (int)IndexSpawn.PlasticBag:
                // velocityy = -1
                break;
            case (int)IndexSpawn.Web:
                // velocityy = -2
                break;
        }
    }
}
