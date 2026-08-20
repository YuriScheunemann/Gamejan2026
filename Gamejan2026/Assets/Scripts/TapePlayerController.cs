using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TapePlayerController : MonoBehaviour
{
    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip tapeClip;

    [Header("Legenda")]
    [TextArea] public string subtitle; // texto completo, será dividido em partes
    public TMP_Text subtitleTMP;
    public Text subtitleText;

    [Header("Configuração da exibição")]
    [Tooltip("Tempo em segundos que cada parte permanece visível após ser totalmente exibida.")]
    public float partDisplayDuration = 1.5f;
    [Tooltip("Delay entre caracteres ao exibir (efeito máquina de escrever).")]
    public float charRevealDelay = 0.03f;
    [Tooltip("Se verdadeiro, limpa a legenda ao parar o áudio.")]
    public bool clearOnStop = true;

    [SerializeField] GameObject painel;
    private Coroutine subtitleRoutine;

    // Permite configurar fita/legenda dinamicamente (ex: ao pegar a fita)
    public void SetTape(AudioClip clip, string subtitleText)
    {
        tapeClip = clip;
        subtitle = subtitleText ?? string.Empty;
    }

    public bool IsPlaying => audioSource != null && audioSource.isPlaying;

    public void PlayTape()
    {
        if (tapeClip == null)
        {
            Debug.LogWarning($"{name}: 'tapeClip' não atribuído.");
            return;
        }

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = tapeClip;
        audioSource.Play();

        if (subtitleRoutine != null)
            StopCoroutine(subtitleRoutine);

        subtitleRoutine = StartCoroutine(ShowSubtitlePartsCoroutine());
    }

    public void StopTape()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        if (subtitleRoutine != null)
        {
            StopCoroutine(subtitleRoutine);
            subtitleRoutine = null;
        }

        if (clearOnStop) ClearSubtitle();        
    }

    private IEnumerator ShowSubtitlePartsCoroutine()
    {
        // Divide a legenda em partes por sentenças/linhas, preservando pontuação final
        string[] rawParts = subtitle.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var partsList = new System.Collections.Generic.List<string>();

        foreach (var line in rawParts)
        {
            int start = 0;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '.' || c == '!' || c == '?')
                {
                    int len = i - start + 1;
                    if (len > 0)
                    {
                        partsList.Add(line.Substring(start, len).Trim());
                        start = i + 1;
                    }
                }
            }
            if (start < line.Length)
            {
                string rem = line.Substring(start).Trim();
                if (!string.IsNullOrEmpty(rem))
                    partsList.Add(rem);
            }
        }

        if (partsList.Count == 0 && !string.IsNullOrWhiteSpace(subtitle))
            partsList.Add(subtitle.Trim());

        float audioLength = (audioSource != null && audioSource.clip != null) ? audioSource.clip.length : Mathf.Infinity;
        float startTime = Time.time;

        for (int p = 0; p < partsList.Count; p++)
        {
            if (audioSource == null || !audioSource.isPlaying || (Time.time - startTime) > audioLength)
                break;

            string part = partsList[p];
            yield return StartCoroutine(TypewriterShow(part));

            float t = 0f;
            while (t < partDisplayDuration)
            {
                if (audioSource == null || !audioSource.isPlaying) break;
                t += Time.deltaTime;
                yield return null;
            }
        }

        ClearSubtitle();
        subtitleRoutine = null;
    }

    private IEnumerator TypewriterShow(string text)
    {
        SetSubtitle(string.Empty);

        for (int i = 0; i < text.Length; i++)
        {
            string sub = text.Substring(0, i + 1);
            if (subtitleTMP != null) subtitleTMP.text = sub;
            if (subtitleText != null) subtitleText.text = sub;
            yield return new WaitForSeconds(charRevealDelay);
        }
    }

    private void SetSubtitle(string text)
    {
        if (subtitleTMP != null) subtitleTMP.text = text;
        if (subtitleText != null) subtitleText.text = text;
    }

    private void ClearSubtitle()
    {
        SetSubtitle(string.Empty);
        painel.SetActive(false);
    }
}