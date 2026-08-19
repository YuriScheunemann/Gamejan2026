using UnityEngine;

public class PuzzleDragObject : MonoBehaviour
{
    [Header("Posição Correta Primária")]
    [SerializeField] private Transform target;

    [Header("Posição Final")]
    [SerializeField] private Transform finalPosition;

    [Header("Configurações")]
    [SerializeField] private float targetRadius = 1f;
    [SerializeField] private float returnSpeed = 10f;
    [SerializeField] private float finalMoveSpeed = 8f;

    [Header("Área de Detecção")]
    [SerializeField] private float spawnDetectionRadius = 1f;

    private Camera mainCamera;

    private Vector3 startPosition;
    private Vector3 dragOffset;

    private bool dragging;
    private bool completed;
    private bool returning;
    private bool movingToFinal;

    public float SpawnDetectionRadius => spawnDetectionRadius;
    public bool IsCompleted => completed;

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

        if (movingToFinal && finalPosition != null)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                finalPosition.position,
                finalMoveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(
                transform.position,
                finalPosition.position
            ) < 0.01f)
            {
                transform.position = finalPosition.position;
                movingToFinal = false;

                if (PuzzleOrderManager.Instance != null)
                {
                    PuzzleOrderManager.Instance.FinalPieceArrived();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (completed || returning || movingToFinal)
            return;

        Vector3 mousePosition = GetMouseWorldPosition();

        dragOffset = transform.position - mousePosition;

        dragging = true;
    }

    private void OnMouseDrag()
    {
        if (!dragging || completed || movingToFinal)
            return;

        Vector3 mousePosition = GetMouseWorldPosition();

        transform.position = mousePosition + dragOffset;
    }

    private void OnMouseUp()
    {
        if (!dragging || completed || movingToFinal)
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
            completed = true;

            transform.position = target.position;

            if (PuzzleOrderManager.Instance != null)
            {
                PuzzleOrderManager.Instance.PieceCompleted();
            }
        }
        else
        {
            StartReturn();
        }
    }

    private void StartReturn()
    {
        returning = true;

        if (PuzzleOrderManager.Instance != null)
        {
            PuzzleOrderManager.Instance.WrongPlacement();
        }
    }

    public void MoveToFinalPosition()
    {
        if (finalPosition == null)
            return;

        movingToFinal = true;
        dragging = false;
        returning = false;
    }

    public void SetStartPosition(Vector3 position)
    {
        startPosition = position;

        transform.position = position;

        completed = false;
        dragging = false;
        returning = false;
        movingToFinal = false;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;

        completed = false;
        dragging = false;
        returning = false;
        movingToFinal = false;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            spawnDetectionRadius
        );
    }
}
