using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField][Range(0, 2)] private int obstacleIndex;
    bool Iscane = false;
    private Rigidbody2D rigidbody2D;
    private SurfaceEffector2D surfaceEffector2D;
    private BoxCollider2D boxCollider2D;
    BoatMoviment boatMoviment;
    float slowSpeed = 0;
    public enum IndexSpawn
    {
        Cane,
        PlasticBag,
        Web
    }
    private void Start()
    {
        SetState(newindex: (IndexSpawn)obstacleIndex);
        rigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = GetComponent<SurfaceEffector2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //BackgroundVelocity - velocityy       
        if (collision.collider.CompareTag("Destroyer"))
            Destroy(gameObject);

        if (collision.collider.CompareTag("Player") && !Iscane)
        {
            gameObject.transform.SetParent(collision.collider.transform);
            rigidbody2D.gravityScale = 0;
            rigidbody2D.linearVelocity = Vector2.zero;
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;     
            surfaceEffector2D.forceScale = 0;
            surfaceEffector2D.speed = 0;
            boxCollider2D.isTrigger = true;
            boatMoviment = GetComponentInParent<BoatMoviment>();
            boatMoviment.SlowSpeed(slowSpeed);
            StartCoroutine(ObstacleDestroy());
        }
        
        if (collision.collider.CompareTag("Player") && Iscane)
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
                slowSpeed = 0;
                break;
            case (int)IndexSpawn.Web:
                slowSpeed = 0;
                break;
        }

        // O segundo switch é para simular um OnTriggerEnter, onde o inimigo começa a fazer algo relacionado ao novo estado.
        switch (obstacleIndex)
        {
            case (int)IndexSpawn.Cane:
                Iscane = true;
                break;
            case (int)IndexSpawn.PlasticBag:
                slowSpeed = 0.2f;
                break;
            case (int)IndexSpawn.Web:
                slowSpeed = 0.4f;
                break;
        }
    }

    IEnumerator ObstacleDestroy()
    {
        yield return new WaitForSeconds(30);
        if (obstacleIndex == 1)
            slowSpeed = -0.2f;
        else if (obstacleIndex == 2)
            slowSpeed = -0.4f;
        boatMoviment.SlowSpeed(slowSpeed);
        Destroy(gameObject);
    }
}
