using UnityEngine;

public class TrashGameManager : MonoBehaviour
{
    public static TrashGameManager Instance { get; private set; }

    [Header("Objetivo e limites")]
    [SerializeField] private int requiredCorrectToUnlock = 15;
    [SerializeField] private int maxAllowedErrors = 3;

    private int correctCount;
    private int errorCount;
    private bool unlocked;
    private bool gameOver;

    private TrashSpawner trashSpawner;

    public int CorrectCount => correctCount;
    public int ErrorCount => errorCount;
    public int RequiredCorrect => requiredCorrectToUnlock;
    public int MaxAllowedErrors => maxAllowedErrors;
    public bool IsUnlocked => unlocked;
    public bool IsGameOver => gameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        unlocked = false;
        gameOver = false;
        correctCount = 0;
        errorCount = 0;
    }

    private void Start()
    {
        trashSpawner = FindFirstObjectByType<TrashSpawner>();

        if (trashSpawner == null)
        {
            Debug.LogWarning(
                "TrashGameManager: TrashSpawner não encontrado."
            );
        }
    }

    public void RegisterResult(bool correct)
    {
        if (gameOver || unlocked)
            return;

        if (correct)
        {
            correctCount++;

            if (errorCount > 0)
            {
                errorCount--;
            }

            Debug.Log(
                $"TrashGameManager: Acerto! " +
                $"Pontos: {correctCount} | Erros: {errorCount}"
            );

            if (correctCount >= requiredCorrectToUnlock)
            {
                unlocked = true;

                Debug.Log(
                    "TrashGameManager: 15 lixos reciclados!"
                );

                if (trashSpawner != null)
                {
                    trashSpawner.TrashGoalReached();
                }
            }
        }
        else
        {
            errorCount++;

            Debug.Log(
                $"TrashGameManager: Erro! " +
                $"{errorCount}/{maxAllowedErrors}"
            );

            if (errorCount >= maxAllowedErrors)
            {
                gameOver = true;

                Debug.Log(
                    "TrashGameManager: Limite de erros atingido."
                );
            }
        }
    }

    public void ResetProgress()
    {
        correctCount = 0;
        errorCount = 0;
        unlocked = false;
        gameOver = false;
    }
}