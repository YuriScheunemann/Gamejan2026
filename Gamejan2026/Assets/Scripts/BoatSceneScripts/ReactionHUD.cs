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

    private void Start()
    {
        if (reactionImage != null)
        {
            reactionImage.sprite = normalSprite;
        }
    }

    public void ShowGood()
    {
        ShowReaction(goodSprite);
    }

    public void ShowBad()
    {
        ShowReaction(badSprite);
    }

    private void ShowReaction(Sprite reactionSprite)
    {
        if (reactionImage == null)
            return;

        if (reactionSprite == null)
            return;

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

        yield return new WaitForSeconds(reactionDuration);

        reactionImage.sprite = normalSprite;

        reactionCoroutine = null;
    }
}