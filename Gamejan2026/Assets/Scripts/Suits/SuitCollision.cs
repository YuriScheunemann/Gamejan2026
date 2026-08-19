using UnityEngine;

public class SuitCollision : MonoBehaviour
{
    [SerializeField] private SuitsManager suitsManager;
    private SpriteRenderer spriteRenderer;
    private bool alreadyStay = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<MouseGrab>() && !alreadyStay)
        {
            suitsManager.AllSuitsOnReach(1);
            alreadyStay = true;
            spriteRenderer.color = Color.green;          
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<MouseGrab>())
        {
            suitsManager.AllSuitsOnReach(-1);
            alreadyStay = false;
            spriteRenderer.color = Color.red;           
        }
    }
}
