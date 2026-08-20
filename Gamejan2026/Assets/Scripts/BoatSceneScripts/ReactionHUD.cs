using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReactionHUD : MonoBehaviour
{
    [Header("Imagem")]
    [SerializeField] private Image reactionImage;

    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite goodSprite;
    [SerializeField] private Sprite badSprite;

    [Header("Tempo da reação")]
    [SerializeField] private float reactionDuration = 1f;

    private Coroutine reactionCoroutine;

    private void Awake()
    {
        if (reactionImage == null)
        {
            Debug.LogError(
                "ReactionHUD: Reaction Image não foi atribuída!"
            );

            return;
        }

        if (normalSprite == null)
        {
            Debug.LogWarning(
                "ReactionHUD: Normal Sprite não foi atribuído."
            );
        }

        if (goodSprite == null)
        {
            Debug.LogWarning(
                "ReactionHUD: Good Sprite não foi atribuído."
            );
        }

        if (badSprite == null)
        {
            Debug.LogWarning(
                "ReactionHUD: Bad Sprite não foi atribuído."
            );
        }

        reactionImage.gameObject.SetActive(true);
        reactionImage.sprite = normalSprite;
    }

    public void ShowGood()
    {
        Debug.Log("ReactionHUD: SHOW GOOD");

        ShowReaction(goodSprite);
    }

    public void ShowBad()
    {
        Debug.Log("ReactionHUD: SHOW BAD");

        ShowReaction(badSprite);
    }

    private void ShowReaction(Sprite reactionSprite)
    {
        if (reactionImage == null)
        {
            Debug.LogError(
                "ReactionHUD: Reaction Image está nula."
            );

            return;
        }

        if (reactionSprite == null)
        {
            Debug.LogError(
                "ReactionHUD: O sprite da reação está nulo."
            );

            return;
        }

        // Garante que a imagem esteja ativa.
        reactionImage.gameObject.SetActive(true);

        // Cancela uma reação anterior.
        if (reactionCoroutine != null)
        {
            StopCoroutine(reactionCoroutine);
        }

        reactionCoroutine = StartCoroutine(
            ReactionRoutine(reactionSprite)
        );
    }

    private IEnumerator ReactionRoutine(Sprite reactionSprite)
    {
        reactionImage.sprite = reactionSprite;

        yield return new WaitForSeconds(
            reactionDuration
        );

        if (reactionImage != null)
        {
            reactionImage.sprite = normalSprite;
        }

        reactionCoroutine = null;
    }
}