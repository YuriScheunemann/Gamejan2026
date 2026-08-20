using TMPro;
using UnityEngine;

public class AirsFixed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI airsTask;
   
  public void TaskAtualization(int airDone)
    {
       airsTask.text = ("Ares-condicionados consertados: " + airDone + "/ 3" );
    }
}
