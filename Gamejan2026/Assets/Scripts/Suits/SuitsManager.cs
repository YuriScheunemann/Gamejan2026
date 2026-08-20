using UnityEngine;
using UnityEngine.Events;
public class SuitsManager : MonoBehaviour
{
    int inputsIndex;
    [SerializeField] private UnityEvent AllSuitsOn;
    [SerializeField]private AirsFixed airsFixed;
    public void AllSuitsOnReach(int input)
    {
        inputsIndex += input;
        if (inputsIndex < 0)
            inputsIndex = 0;     
     
        if (inputsIndex == 4)
        {
            AllSuitsOn.Invoke();
            airsFixed.TaskAtualization(1);
        }
    }
}
