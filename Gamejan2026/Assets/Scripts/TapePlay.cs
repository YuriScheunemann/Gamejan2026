using UnityEngine;
using UnityEngine.Events;
public class TapePlay : MonoBehaviour
{
    [SerializeField] private AudioClip tapeClip;
    [SerializeField] private AudioSource cameraAudioSource;
    [SerializeField] private UnityEvent tapeInteraction;
 
    void Start()
    {
        tapeClip = GetComponent<AudioClip>();
        cameraAudioSource = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        cameraAudioSource.PlayOneShot(tapeClip);
    }
}
