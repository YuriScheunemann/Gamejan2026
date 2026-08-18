using UnityEngine;

public class SuitCollision : MonoBehaviour
{
    [SerializeField] private SuitsManager suitsManager;
  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MouseGrab>())
        {
            suitsManager.AllSuitsOnReach(1);           
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<MouseGrab>())
        {
            suitsManager.AllSuitsOnReach(-1);           
        }
    }
}
