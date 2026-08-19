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

    private TrashGameManager manager;

    private void Start()
    {
        manager = TrashGameManager.Instance;

        if (manager == null)
        {
            Debug.LogError(
                "TrashHUD: TrashGameManager não encontrado na cena."
            );

            return;
        }

        UpdateHUD();
    }

    private void Update()
    {
        if (manager == null)
            return;

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (manager == null)
            return;

        if (scoreText != null)
        {
            scoreText.text = manager.CorrectCount.ToString();
        }

        UpdateErrorImage(manager.ErrorCount);
    }

    private void UpdateErrorImage(int errors)
    {
        if (errorImage == null)
            return;

        if (errorSprites == null ||
            errorSprites.Length == 0)
            return;

        int index = Mathf.Clamp(
            errors,
            0,
            errorSprites.Length - 1
        );

        errorImage.sprite = errorSprites[index];
    }
}