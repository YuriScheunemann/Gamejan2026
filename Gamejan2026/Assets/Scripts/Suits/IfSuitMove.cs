using Unity.VisualScripting;
using UnityEngine;

public class IfSuitMove : MonoBehaviour
{
    [SerializeField] private SuitsEnum suitsEnum;
    float transformBase;
    float newTransformBase;
    float diferenceFromTransformBase;
    float forDiferenceFromTransformBase;
    private void Start()
    {
        transformBase = gameObject.transform.position.y;
        newTransformBase = gameObject.transform.position.y;        
    }
    private void OnMouseDrag()
    {
        if (gameObject.transform.position.y == float.PositiveInfinity)
        {
            newTransformBase -= gameObject.transform.position.y;
            diferenceFromTransformBase += newTransformBase;
            for (forDiferenceFromTransformBase = transformBase; forDiferenceFromTransformBase < diferenceFromTransformBase; forDiferenceFromTransformBase++) 
            { 
            
            }
            for (forDiferenceFromTransformBase = transformBase; forDiferenceFromTransformBase > diferenceFromTransformBase; forDiferenceFromTransformBase++)
            {

            }
        }
    }

    private void OnMouseUp()
    {
       
        
    }
}
