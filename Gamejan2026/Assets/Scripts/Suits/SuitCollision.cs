using UnityEngine;

public class SuitCollision : MonoBehaviour
{
    SuitsManager suitsManager;
    private void Start()
    {
        suitsManager = GetComponent<SuitsManager>();        ;
    }
    void OnTriggerEnter(Collider other)
    {
        print("Trigger entered");
        if (other.GetComponent<MouseGrab>())
        {
            suitsManager.AllSuitsOnReach(1);
            print("less one");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MouseGrab>())
        {
            suitsManager.AllSuitsOnReach(-1);
            print("More one");
        }
    }
}
