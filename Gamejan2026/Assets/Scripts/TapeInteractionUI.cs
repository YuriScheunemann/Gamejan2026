using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TapeInteractionUI : MonoBehaviour
{
    [Header("Detecção")]
    public string tapeTag = "fita";

    [Header("UI")]
    [Tooltip("Container que contém o botão e o texto pré-visualização (será ativado ao colidir).")]
    public GameObject uiContainer;
    public Button playButton;
    public TMP_Text previewTextTMP;
    public Text previewText;

    [Header("Referência ao player de fita")]
    public TapePlayerController tapePlayer;

    private GameObject currentTapeObject;

    private void Awake()
    {
        if (uiContainer != null) uiContainer.SetActive(false);

        if (playButton != null)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(OnPlayPressed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryShowUIForTape(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryShowUIForTape(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TryHideUIForTape(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        TryHideUIForTape(other.gameObject);
    }

    private void TryShowUIForTape(GameObject obj)
    {
        if (!obj.CompareTag(tapeTag)) return;

        currentTapeObject = obj;

        // tenta ler dados diretamente (componente TapeData) ou AudioSource
        AudioClip foundClip = null;
        string subtitle = "";

        // evita referência direta ao tipo TapeData (resolve CS0246 quando o tipo não é acessível)
        Component dataComp = obj.GetComponent("TapeData");
        if (dataComp != null)
        {
            var t = dataComp.GetType();
            var clipField = t.GetField("clip");
            if (clipField != null && clipField.FieldType == typeof(AudioClip))
                foundClip = clipField.GetValue(dataComp) as AudioClip;
            var subtitleField = t.GetField("subtitle");
            if (subtitleField != null && subtitleField.FieldType == typeof(string))
                subtitle = subtitleField.GetValue(dataComp) as string ?? "";
        }
        else
        {
            var aud = obj.GetComponent<AudioSource>();
            if (aud != null) foundClip = aud.clip;
        }

        // configura tapePlayer para tocar a fita encontrada
        if (tapePlayer != null)
        {
            tapePlayer.SetTape(foundClip, subtitle);
        }

        // mostra UI e define texto de preview (pode ser primeira linha ou instrução)
        string preview = !string.IsNullOrEmpty(subtitle) ? subtitle.Split('\n')[0] : "Pressione para tocar a fita";
        if (previewTextTMP != null)
            previewTextTMP.text = preview;
        if (previewText != null)
            previewText.text = preview;

        if (uiContainer != null) uiContainer.SetActive(true);
    }

    private void TryHideUIForTape(GameObject obj)
    {
        if (currentTapeObject == null) return;
        if (obj != currentTapeObject) return;

        if (uiContainer != null) uiContainer.SetActive(false);
        if (tapePlayer != null) tapePlayer.StopTape();

        currentTapeObject = null;
    }

    private void OnPlayPressed()
    {
        if (tapePlayer == null) return;

        tapePlayer.PlayTape();

        // Esconde o UI quando o botão é apertado, conforme solicitado
        if (uiContainer != null) uiContainer.SetActive(false);
    }
}