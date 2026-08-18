using Unity.VisualScripting;
using UnityEngine;

public class IfSuitMove : MonoBehaviour
{
    [SerializeField] private GameObject[] Suits;
    [SerializeField] private SuitsEnum suitsEnum;
    float transformBase;
    float newTransformBase;
    float diferenceFromTransformBase;
    float forDiferenceFromTransformBase;
    
    private void Start()
    {
        transformBase = gameObject.transform.position.y;
        newTransformBase = gameObject.transform.position.y;
        suitsEnum = gameObject.GetComponent<SuitsEnum>();        
    }
    private void OnMouseDrag()
    {
        if (gameObject.transform.position.y == float.PositiveInfinity)
        {
            newTransformBase -= gameObject.transform.position.y;
            diferenceFromTransformBase += newTransformBase;
            for (forDiferenceFromTransformBase = transformBase; forDiferenceFromTransformBase < diferenceFromTransformBase; forDiferenceFromTransformBase++)
            {
                if(suitsEnum == SuitsEnum.Red)
                {
                   //transform.position.y -= new Vector2(0, 2)
                }
                if (suitsEnum == SuitsEnum.Blue)
                {

                }
                if (suitsEnum == SuitsEnum.Yellow)
                {

                }
                if (suitsEnum == SuitsEnum.Green)
                {

                }
                //Suits[0].transform.position.y -= new Vector2(0, 2);

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
