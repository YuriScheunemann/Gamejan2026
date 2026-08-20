using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class AirsFixed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI airsTask;
    [SerializeField] private UnityEvent taskCompleted;
    int airsFixed = 0;
  public void TaskAtualization(int airDone)
    {
        airsFixed += airDone;
        print(airsFixed);
       airsTask.text = ("Ares-condicionados consertados: " + airDone + "/ 3" );
        if(airsFixed == 3)
            taskCompleted.Invoke();
    }
}
