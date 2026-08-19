using UnityEngine;
using UnityEngine.Events;
public class SuitsManager : MonoBehaviour
{
    int inputsIndex;
    [SerializeField] private UnityEvent AllSuitsOn;
    [SerializeField] private UnityEvent AllSuitsIsntOn;
    public void AllSuitsOnReach(int input)
    {
        inputsIndex += input;
        if (inputsIndex < 0)
            inputsIndex = 0;

        if (inputsIndex != 4)
        {
            AllSuitsIsntOn.Invoke();
            return;
        }


        if (inputsIndex == 4)
        {
            AllSuitsOn.Invoke();           
        }
    }
}
