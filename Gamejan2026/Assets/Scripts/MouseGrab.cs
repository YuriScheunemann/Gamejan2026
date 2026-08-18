using UnityEngine;

public class MouseGrab : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging;
    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera não encontrada.");
        }
    }

    private void OnMouseDown()
    {
        if (mainCamera == null)
            return;

        isDragging = true;

        Vector3 mousePosition = GetMouseWorldPosition();

        // Mantém a distância entre o mouse e o objeto
        offset = transform.position - mousePosition;
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
            return;

        Vector3 mousePosition = GetMouseWorldPosition();

        transform.position = mousePosition + offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Mantém o objeto na mesma profundidade da câmera
        mousePosition.z = Mathf.Abs(
            mainCamera.transform.position.z - transform.position.z
        );

        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}