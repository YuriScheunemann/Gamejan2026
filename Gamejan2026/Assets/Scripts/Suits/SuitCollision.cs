using UnityEngine;

public class SuitCollision : MonoBehaviour
{
    [SerializeField] private SuitsManager suitsManager;
  private bool alreadyStay = false;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<MouseGrab>() && !alreadyStay)
        {
            suitsManager.AllSuitsOnReach(1); 
            alreadyStay = true;            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<MouseGrab>())
        {
            suitsManager.AllSuitsOnReach(-1);
            alreadyStay = false;           
        }
    }
}
