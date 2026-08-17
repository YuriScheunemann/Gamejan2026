using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("texto")]
    [SerializeField] private TMP_Text subtitle;

    [TextArea]
    [SerializeField] private string legenda;


    void Start()
    {
        subtitle.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TocarRadio();
        }
    }
    private void TocarRadio()
    {
        if(audioSource.isPlaying)
        return;

        audioSource.Play();
        subtitle.text = legenda;

        Invoke(nameof(HideLegenda), audioSource.clip.length);
    }

    private void HideLegenda()
    {
        subtitle.text = "";
    }
}
