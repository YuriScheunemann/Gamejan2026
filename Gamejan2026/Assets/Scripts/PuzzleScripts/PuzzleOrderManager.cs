using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleOrderManager : MonoBehaviour
{
    public static PuzzleOrderManager Instance;

    [System.Serializable]
    private class SpawnedPiece
    {
        public Vector3 position;
        public float radius;
    }

    [Header("Peças")]
    [SerializeField] private PuzzleDragObject[] pieces;

    [Header("Áreas de Spawn")]
    [SerializeField] private PuzzleSpawnArea spawnArea1;
    [SerializeField] private PuzzleSpawnArea spawnArea2;

    [SerializeField] private int maxAttempts = 500;

    [Header("Feedback")]
    [SerializeField] private GameObject redPanel;
    [SerializeField] private GameObject greenPanel;
    [SerializeField] private float flashDuration = 0.15f;
    [SerializeField] private int flashAmount = 2;

    [Header("Cena Final")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private float waitBeforeNextScene = 3f;

    private bool flashingRed;
    private bool flashingGreen;
    private bool puzzleCompleted;

    private int finalPiecesArrived;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (spawnArea1 == null || spawnArea2 == null)
        {
            Debug.LogError(
                "As duas áreas de spawn precisam ser definidas."
            );

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

        if (greenPanel != null)
            greenPanel.SetActive(false);

        SpawnPieces();

        spawnArea1.DisableCollider();
        spawnArea2.DisableCollider();
    }

    public void PieceCompleted()
    {
        if (puzzleCompleted)
            return;

        StartCoroutine(FlashGreen());

        foreach (PuzzleDragObject piece in pieces)
        {
            if (piece == null)
                continue;

            if (!piece.IsCompleted)
                return;
        }

        puzzleCompleted = true;

        StartCoroutine(MovePiecesToFinal());
    }

    private IEnumerator FlashGreen()
    {
        if (flashingGreen)
            yield break;

        flashingGreen = true;

        if (greenPanel != null)
        {
            greenPanel.SetActive(true);

            yield return new WaitForSecondsRealtime(
                flashDuration
            );

            greenPanel.SetActive(false);
        }

        flashingGreen = false;
    }

    private IEnumerator MovePiecesToFinal()
    {
        yield return new WaitForSeconds(0.1f);

        finalPiecesArrived = 0;

        foreach (PuzzleDragObject piece in pieces)
        {
            if (piece != null)
            {
                piece.MoveToFinalPosition();

                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void FinalPieceArrived()
    {
        finalPiecesArrived++;

        if (finalPiecesArrived >= pieces.Length)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError(
                "Nenhuma cena foi definida em Next Scene Name."
            );

            yield break;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    public void WrongPlacement()
    {
        if (!flashingRed)
            StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        flashingRed = true;

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

        flashingRed = false;
    }

    private void SpawnPieces()
    {
        List<SpawnedPiece> spawned =
            new List<SpawnedPiece>();

        foreach (PuzzleDragObject piece in pieces)
        {
            if (piece == null)
                continue;

            Vector3 position =
                FindFreePosition(
                    piece,
                    spawned
                );

            piece.SetStartPosition(position);

            spawned.Add(
                new SpawnedPiece
                {
                    position = position,
                    radius =
                        piece.SpawnDetectionRadius
                }
            );
        }
    }

    private Vector3 FindFreePosition(
        PuzzleDragObject piece,
        List<SpawnedPiece> spawned
    )
    {
        for (
            int attempt = 0;
            attempt < maxAttempts;
            attempt++
        )
        {
            Vector3 position =
                GetRandomPositionFromAreas();

            if (IsPositionFree(
                piece,
                position,
                spawned
            ))
            {
                return position;
            }
        }

        return GetRandomPositionFromAreas();
    }

    private Vector3 GetRandomPositionFromAreas()
    {
        if (Random.value < 0.5f)
            return spawnArea1.GetRandomPosition();

        return spawnArea2.GetRandomPosition();
    }

    private bool IsPositionFree(
        PuzzleDragObject piece,
        Vector3 position,
        List<SpawnedPiece> spawned
    )
    {
        float radius =
            piece.SpawnDetectionRadius;

        foreach (SpawnedPiece other in spawned)
        {
            float requiredDistance =
                radius + other.radius;

            float distance =
                Vector2.Distance(
                    position,
                    other.position
                );

            if (distance < requiredDistance)
                return false;
        }

        return true;
    }
}
