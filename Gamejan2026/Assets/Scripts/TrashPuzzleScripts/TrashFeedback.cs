using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrashFeedback : MonoBehaviour
{
    public static TrashFeedback Instance { get; private set; }

    [Header("Imagens de feedback (atribua no Inspector)")]
    [SerializeField] private Image correctImage;
    [SerializeField] private Image incorrectImage;

    [Header("Configuração")]
    [SerializeField] private float showDuration = 1f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (correctImage != null) correctImage.gameObject.SetActive(false);
        if (incorrectImage != null) incorrectImage.gameObject.SetActive(false);
    }

    public void ShowResult(bool correct)
    {
        // Se o objeto de feedback estiver desativado na cena, ativamos para garantir que as imagens apareçam
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("TrashFeedback: GameObject estava inativo. Ativando para mostrar feedback.");
            gameObject.SetActive(true);
        }

        if (correctImage == null && incorrectImage == null)
        {
            Debug.LogWarning("TrashFeedback: correctImage e incorrectImage não atribuídas no Inspector.");
            return;
        }

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(correct));
    }

    private IEnumerator ShowRoutine(bool correct)
    {
        if (correctImage != null) correctImage.gameObject.SetActive(correct);
        if (incorrectImage != null) incorrectImage.gameObject.SetActive(!correct);

        yield return new WaitForSeconds(showDuration);

        if (correctImage != null) correctImage.gameObject.SetActive(false);
        if (incorrectImage != null) incorrectImage.gameObject.SetActive(false);

        currentRoutine = null;
    }
}