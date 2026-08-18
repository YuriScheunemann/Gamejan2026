using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public TrashType type;
    public float conveyorSpeed = 2f;

    private bool isDragging;
    private Camera mainCamera;
    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!isDragging)
        {
            transform.Translate(Vector3.down * conveyorSpeed * Time.deltaTime);
        }

        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Mathf.Abs(mainCamera.transform.position.z);

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = transform.position.z;

            transform.position = worldPosition + offset;
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryStartDragging();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            CheckBin();
        }
    }

    private void TryStartDragging()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        Vector2 mouseWorldPosition = new Vector2(
            worldPosition.x,
            worldPosition.y
        );

        RaycastHit2D hit = Physics2D.Raycast(
            mouseWorldPosition,
            Vector2.zero
        );

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isDragging = true;

            offset = transform.position - worldPosition;
            offset.z = 0f;
        }
    }

    private void CheckBin()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            transform.position,
            0.5f
        );

        foreach (Collider2D collider in colliders)
        {
            TrashBin bin = collider.GetComponent<TrashBin>();

            if (bin != null && bin.acceptedType == type)
            {
                bin.ReceiveTrash(this);
                return;
            }
        }
    }
}
