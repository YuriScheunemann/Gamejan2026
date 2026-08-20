using UnityEngine;

public class BarrierTalk : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
<<<<<<< Updated upstream
            DialogueActivation.Instance.StartDialogo();        
=======
            DialogueActivation.Instance.StartDialogo();
        
>>>>>>> Stashed changes
    }
}
