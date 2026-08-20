using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayCutsceneAndLoad : MonoBehaviour
{
    [Header("Referências")]
    public TapePlayerController tapePlayer;
    public GameObject uiContainer;
    public Button playButton;

    [Header("Cutscene (Timeline ou Video)")]
    public PlayableDirector timelineDirector;
    public VideoPlayer videoPlayer;

    [Header("Som / Cue antes da transição")]
    public AudioSource cueSource;
    public AudioClip cueSound;
    public float extraDelayAfterCue = 0.05f;

    [Header("Carregamento de cena")]
    public string sceneToLoad;
    public bool waitForCutscene = true;

    private bool loadingInProgress;

    private void Awake()
    {
        if (playButton != null)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(OnPlayAndCutscenePressed);
        }
    }

    public void OnPlayAndCutscenePressed()
    {
        if (loadingInProgress) return;
        loadingInProgress = true;

        if (playButton != null) playButton.interactable = false;
        if (uiContainer != null) uiContainer.SetActive(false);

        // Inicia a fita imediatamente se houver player
        tapePlayer?.PlayTape();

        // toca cue se tiver
        if (cueSound != null)
        {
            if (cueSource == null) cueSource = gameObject.AddComponent<AudioSource>();
            cueSource.PlayOneShot(cueSound);
        }

        StartCoroutine(PlayCutsceneThenLoadCoroutine());
    }

    private IEnumerator PlayCutsceneThenLoadCoroutine()
    {
        // Se houver Timeline, dá play
        if (timelineDirector != null)
        {
            timelineDirector.Play();
            if (waitForCutscene)
                yield return new WaitWhile(() => timelineDirector.state == PlayState.Playing);
        }
        // Senão, se houver VideoPlayer, toca e espera terminar
        else if (videoPlayer != null)
        {
            videoPlayer.Play();
            if (waitForCutscene)
                yield return new WaitWhile(() => videoPlayer.isPlaying);
        }
        else
        {
            // Se não houver cutscene visual, espera pelo cueSound (se existir)
            float wait = extraDelayAfterCue;
            if (cueSound != null) wait += cueSound.length;
            yield return new WaitForSeconds(wait);
        }

        // garante pequeno delay adicional (opcional)
        yield return new WaitForSeconds(extraDelayAfterCue);

        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);

        loadingInProgress = false;
    }
}