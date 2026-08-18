using UnityEngine;
using UnityEngine.Events;
public class SuitsManager : MonoBehaviour
{
    int inputsIndex;
    [SerializeField]private UnityEvent AllSuitsOn;
    public void AllSuitsOnReach(int input)
    {
        inputsIndex += input;
        if (inputsIndex == 4) 
        {
            AllSuitsOn.Invoke();
        }
    }
}
