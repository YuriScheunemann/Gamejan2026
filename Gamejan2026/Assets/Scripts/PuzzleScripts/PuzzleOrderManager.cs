using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOrderManager : MonoBehaviour
{
    public static PuzzleOrderManager Instance;

    [Header("Peças do Puzzle")]
    [SerializeField] private PuzzleDragObject[] pieces;

    [Header("Área de Spawn")]
    [SerializeField] private float horizontalPadding = 50f;
    [SerializeField] private float verticalPadding = 50f;
    [SerializeField] private int maxAttempts = 200;
    [SerializeField] private float extraSpacing = 0.2f;

    [Header("Área Central Proibida")]
    [Range(0f, 0.5f)]
    [SerializeField] private float leftLimit = 0.30f;

    [Range(0.5f, 1f)]
    [SerializeField] private float rightLimit = 0.70f;

    [Header("Feedback de Erro")]
    [SerializeField] private GameObject redPanel;
    [SerializeField] private float flashDuration = 0.15f;
    [SerializeField] private int flashAmount = 2;

    private Camera mainCamera;
    private bool flashing;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera não encontrada.");
            return;
        }

        if (pieces == null || pieces.Length == 0)
        {
            pieces = FindObjectsByType<PuzzleDragObject>(
                FindObjectsSortMode.None
            );
        }

        if (redPanel != null)
            redPanel.SetActive(false);

        SpawnPiecesRandomly();
    }

    public void WrongPlacement()
    {
        if (!flashing)
            StartCoroutine(FlashError());
    }

    private IEnumerator FlashError()
    {
        flashing = true;

        if (redPanel != null)
        {
            for (int i = 0; i < flashAmount; i++)
            {
                redPanel.SetActive(true);

                yield return new WaitForSecondsRealtime(
                    flashDuration
                );

                redPanel.SetActive(false);

                yield return new WaitForSecondsRealtime(
                    flashDuration
                );
            }
        }

        flashing = false;
    }

    private void SpawnPiecesRandomly()
    {
        if (pieces == null || pieces.Length == 0)
            return;

        List<PuzzleDragObject> spawnedPieces =
            new List<PuzzleDragObject>();

        foreach (PuzzleDragObject piece in pieces)
        {
            if (piece == null)
                continue;

            Vector3 validPosition =
                FindValidPosition(piece, spawnedPieces);

            piece.SetStartPosition(validPosition);

            spawnedPieces.Add(piece);
        }
    }

    private Vector3 FindValidPosition(
        PuzzleDragObject piece,
        List<PuzzleDragObject> spawnedPieces
    )
    {
        Collider2D pieceCollider =
            piece.GetComponent<Collider2D>();

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 position =
                GetRandomScreenPosition();

            if (IsPositionValid(
                piece,
                pieceCollider,
                position,
                spawnedPieces
            ))
            {
                return position;
            }
        }

        Debug.LogWarning(
            "Não foi possível encontrar uma posição livre para " +
            piece.name +
            ". Tente aumentar a área de spawn ou diminuir a quantidade de peças."
        );

        return GetRandomScreenPosition();
    }

    private bool IsPositionValid(
        PuzzleDragObject piece,
        Collider2D pieceCollider,
        Vector3 position,
        List<PuzzleDragObject> spawnedPieces
    )
    {
        if (pieceCollider == null)
        {
            return IsPositionValidWithoutCollider(
                position,
                spawnedPieces
            );
        }

        Vector3 originalPosition =
            piece.transform.position;

        piece.transform.position = position;

        Bounds newBounds =
            pieceCollider.bounds;

        newBounds.Expand(extraSpacing);

        bool valid = true;

        foreach (PuzzleDragObject other in spawnedPieces)
        {
            if (other == null)
                continue;

            Collider2D otherCollider =
                other.GetComponent<Collider2D>();

            if (otherCollider == null)
                continue;

            Bounds otherBounds =
                otherCollider.bounds;

            otherBounds.Expand(extraSpacing);

            if (newBounds.Intersects(otherBounds))
            {
                valid = false;
                break;
            }
        }

        piece.transform.position =
            originalPosition;

        return valid;
    }

    private bool IsPositionValidWithoutCollider(
        Vector3 position,
        List<PuzzleDragObject> spawnedPieces
    )
    {
        foreach (PuzzleDragObject other in spawnedPieces)
        {
            if (other == null)
                continue;

            float distance = Vector3.Distance(
                position,
                other.transform.position
            );

            if (distance < 1f + extraSpacing)
                return false;
        }

        return true;
    }

    private Vector3 GetRandomScreenPosition()
    {
        bool spawnLeft = Random.value < 0.5f;

        float screenX;

        if (spawnLeft)
        {
            screenX = Random.Range(
                horizontalPadding,
                Screen.width * leftLimit
            );
        }
        else
        {
            screenX = Random.Range(
                Screen.width * rightLimit,
                Screen.width - horizontalPadding
            );
        }

        float screenY = Random.Range(
            verticalPadding,
            Screen.height - verticalPadding
        );

        Vector3 screenPosition = new Vector3(
            screenX,
            screenY,
            Mathf.Abs(
                mainCamera.transform.position.z
            )
        );

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(
                screenPosition
            );

        worldPosition.z = 0f;

        return worldPosition;
    }
}
