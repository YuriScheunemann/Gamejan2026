using UnityEngine;
using UnityEngine.Events;

public class TrashGameManager : MonoBehaviour
{
    public static TrashGameManager Instance { get; private set; }

    [Header("Objetivo e limites")]
    [Tooltip("Quantidade correta de itens para desbloquear")]
    [SerializeField] private int requiredCorrectToUnlock = 10;

    [Tooltip("Máximo de erros permitidos antes do game over")]
    [SerializeField] private int maxAllowedErrors = 3;

    [Header("Eventos (atribua no Inspector para reagir)")]
    public UnityEvent OnUnlocked;
    public UnityEvent OnGameOver;
    public UnityEvent OnScoreChanged; // disparado sempre que pontuação/erros mudarem

    private int correctCount;
    private int errorCount;
    private bool unlocked;
    private bool gameOver;

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
    }

    public int CorrectCount => correctCount;
    public int ErrorCount => errorCount;
    public int RequiredCorrect => requiredCorrectToUnlock;
    public int MaxAllowedErrors => maxAllowedErrors;
    public bool IsUnlocked => unlocked;
    public bool IsGameOver => gameOver;

    // Registrar resultado de um descarte (true = correto, false = errado)
    public void RegisterResult(bool correct)
    {
        if (gameOver || unlocked)
            return;

        if (correct)
        {
            correctCount++;
            Debug.Log($"TrashGameManager: Acerto! {correctCount}/{requiredCorrectToUnlock}.");
            if (correctCount >= requiredCorrectToUnlock)
            {
                unlocked = true;
                Debug.Log("TrashGameManager: Objetivo atingido — desbloqueado!");
                OnUnlocked?.Invoke();
            }
        }
        else
        {
            errorCount++;
            Debug.Log($"TrashGameManager: Erro! {errorCount}/{maxAllowedErrors}.");
            if (errorCount >= maxAllowedErrors)
            {
                gameOver = true;
                Debug.Log("TrashGameManager: Limite de erros atingido — game over.");
                OnGameOver?.Invoke();
            }
        }

        OnScoreChanged?.Invoke();
    }

    // Reinicia estado (útil para botão de reiniciar)
    public void ResetProgress()
    {
        correctCount = 0;
        errorCount = 0;
        unlocked = false;
        gameOver = false;
        OnScoreChanged?.Invoke();
    }
}