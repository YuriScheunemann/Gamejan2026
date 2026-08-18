using UnityEngine;

public class SuitCollision : MonoBehaviour
{
    SuitsManager suitsManager;
   
    private void Start()
    {
        suitsManager = GetComponent<SuitsManager>();
    }
    void OnTriggerStay(Collider other)
    {
        //if(other.GetComponent<CScriptName>()) {
        suitsManager.AllSuitsOnReach(1);
    }
    void OnTriggerExit(Collider other)
    {
        //if(other.GetComponent<CScriptName>()) {
        suitsManager.AllSuitsOnReach(-1);
    }
}
