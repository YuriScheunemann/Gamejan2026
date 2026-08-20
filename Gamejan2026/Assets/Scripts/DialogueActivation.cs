using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class DialogueActivation : MonoBehaviour
{
    public static DialogueActivation Instance;

    [Header("UI")]
    public GameObject dialogPanel;
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI autorText;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Conteúdo Antigo")]
    [TextArea(3, 10)]
    public string[] message;

    [Header("WaitSeconds")]
    public float waitSeconds = 0.05f;

    [Header("Novo Sistema (SO)")]
    public DialogueSO dialogoSO;

    private int index = 0;
    private int MaxIndex;

    [Header("Controle")]
    public bool iniciarAutomatico = true;

    [Header("Eventos")]
    public UnityEvent DialogoEnd;

    private bool isTyping = false;
    private bool skipTyping = false;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (iniciarAutomatico)
        {
            StartDialogo();
        }
    }

    public void StartDialogo()
    {
        dialogPanel.SetActive(true);
        index = 0;

        MaxIndex = dialogoSO != null ? dialogoSO.linhas.Length - 1 : message.Length - 1;

        StopAllCoroutines();
        StartCoroutine(TypeDialogueText());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                skipTyping = true;
                return;
            }

            if (index >= MaxIndex)
            {
                HideDialog();
                DialogoEnd.Invoke();
                return;
            }

            index++;
            StopAllCoroutines();
            StartCoroutine(TypeDialogueText());
        }
    }

    void HideDialog()
    {
        textDisplay.text = "";
        if (autorText != null) autorText.text = "";
        dialogPanel.SetActive(false);
    }

    IEnumerator TypeDialogueText()
    {
        isTyping = true;
        skipTyping = false;

        string textoAtual;

        if (dialogoSO != null)
        {
            textoAtual = dialogoSO.linhas[index].fala;
            textDisplay.text = textoAtual;

            if (autorText != null)
                autorText.text = dialogoSO.linhas[index].autor;

            if (dialogoSO.linhas[index].audio != null)
            {
                audioSource.Stop();
                audioSource.clip = dialogoSO.linhas[index].audio;
                audioSource.Play();
            }
        }
        else
        {
            textoAtual = message[index];
            textDisplay.text = textoAtual;
        }

        textDisplay.maxVisibleCharacters = 0;

        for (int i = 0; i <= textoAtual.Length; i++)
        {
            if (skipTyping)
            {
                textDisplay.maxVisibleCharacters = textoAtual.Length;
                break;
            }

            textDisplay.maxVisibleCharacters = i;
            yield return new WaitForSeconds(waitSeconds);
        }

        isTyping = false;
    }
}
