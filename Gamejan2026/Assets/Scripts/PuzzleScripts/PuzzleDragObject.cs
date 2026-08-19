using UnityEngine;

public class PuzzleDragObject : MonoBehaviour
{
    [Header("Destino")]
    [SerializeField] private Transform target;

    [Header("Configurações")]
    [SerializeField] private float targetRadius = 1f;
    [SerializeField] private float returnSpeed = 10f;

    private Camera mainCamera;

    private Vector3 startPosition;
    private Vector3 dragOffset;

    private bool dragging;
    private bool completed;
    private bool returning;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (returning)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                startPosition,
                returnSpeed * Time.deltaTime
            );

            if (Vector3.Distance(
                transform.position,
                startPosition
            ) < 0.01f)
            {
                transform.position = startPosition;
                returning = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (completed || returning)
            return;

        Vector3 mousePosition = GetMouseWorldPosition();

        dragOffset = transform.position - mousePosition;

        dragging = true;
    }

    private void OnMouseDrag()
    {
        if (!dragging || completed)
            return;

        Vector3 mousePosition = GetMouseWorldPosition();

        transform.position = mousePosition + dragOffset;
    }

    private void OnMouseUp()
    {
        if (!dragging || completed)
            return;

        dragging = false;

        CheckTarget();
    }

    private void CheckTarget()
    {
        if (target == null)
        {
            StartReturn();
            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            target.position
        );

        if (distance <= targetRadius)
        {
            CompletePuzzlePiece();
        }
        else
        {
            StartReturn();
        }
    }

    private void CompletePuzzlePiece()
    {
        completed = true;
        transform.position = target.position;
    }

    private void StartReturn()
    {
        returning = true;

        if (PuzzleOrderManager.Instance != null)
        {
            PuzzleOrderManager.Instance.WrongPlacement();
        }
    }

    public void SetStartPosition(Vector3 position)
    {
        startPosition = position;

        transform.position = position;

        completed = false;
        dragging = false;
        returning = false;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;

        completed = false;
        dragging = false;
        returning = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Mathf.Abs(
            mainCamera.transform.position.z -
            transform.position.z
        );

        return mainCamera.ScreenToWorldPoint(
            mousePosition
        );
    }
}
