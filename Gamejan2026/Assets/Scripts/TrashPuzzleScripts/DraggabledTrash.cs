using UnityEngine;

public class DraggabledTrash : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLIQUE DO MOUSE DETECTADO");
        }
    }
}
