using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class TrashHUD : MonoBehaviour
{
    [Header("Referências UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image errorImage;

    [Header("Sprites de Erro")]
    [SerializeField] private Sprite[] errorSprites;

    private void Start()
    {
        TrashGameManager mgr = TrashGameManager.Instance;

        if (mgr == null)
        {
            Debug.LogError("TrashGameManager não encontrado na cena.");
            return;
        }

        mgr.OnScoreChanged.AddListener(UpdateHUD);
        mgr.OnGameOver.AddListener(OnGameOver);

        UpdateHUD();
    }

    private void OnDestroy()
    {
        TrashGameManager mgr = TrashGameManager.Instance;

        if (mgr == null)
            return;

        mgr.OnScoreChanged.RemoveListener(UpdateHUD);
        mgr.OnGameOver.RemoveListener(OnGameOver);
    }

    private void UpdateHUD()
    {
        TrashGameManager mgr = TrashGameManager.Instance;

        if (mgr == null)
            return;

        if (scoreText != null)
        {
            scoreText.text = mgr.CorrectCount.ToString();
        }

        UpdateErrorImage(mgr.ErrorCount);
    }

    private void UpdateErrorImage(int errors)
    {
        if (errorImage == null)
            return;

        if (errorSprites == null || errorSprites.Length == 0)
            return;

        int index = Mathf.Clamp(
            errors,
            0,
            errorSprites.Length - 1
        );

        errorImage.sprite = errorSprites[index];
    }

    private void OnGameOver()
    {
        Debug.Log("TrashHUD: GameOver acionado.");
    }
}