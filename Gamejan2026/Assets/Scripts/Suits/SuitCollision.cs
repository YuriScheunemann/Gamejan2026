using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class SuitCollision : MonoBehaviour
{
    [SerializeField] private SuitsManager suitsManager;
    private SpriteRenderer spriteRenderer;
    private bool alreadyStay = false;
    [SerializeField] private AudioClip onClip;
    [SerializeField] private AudioClip offClip;
    [SerializeField] private AudioSource cameraAudioSource;
    private bool _soundOnColdown = false;
    private bool _soundOffColdown = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<MouseGrab>() && !alreadyStay)
        {
            suitsManager.AllSuitsOnReach(1);
            alreadyStay = true;
            spriteRenderer.color = Color.green;
            _soundOffColdown = false;
            if (!_soundOnColdown)
                StartCoroutine(SoundTimeOn(0.2f));
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<MouseGrab>())
        {
            suitsManager.AllSuitsOnReach(-1);
            alreadyStay = false;
            spriteRenderer.color = Color.red;
            _soundOnColdown = false;
            if (!_soundOffColdown)
                StartCoroutine(SoundTimeOff(0.2f));
        }
    }
    private IEnumerator SoundTimeOn(float coldown)
    {
        yield return new WaitForSeconds(coldown);
        cameraAudioSource.PlayOneShot(onClip);
        StartCoroutine(SoundOnColdown());

    }
    private IEnumerator SoundTimeOff(float coldown)
    {
        yield return new WaitForSeconds(coldown);
        cameraAudioSource.PlayOneShot(offClip);
        StartCoroutine(SoundOffColdown());
    }
    private IEnumerator SoundOffColdown()
    {
        yield return new WaitForSeconds(0.2f);
        _soundOffColdown = true;
    }
    private IEnumerator SoundOnColdown()
    {
        yield return new WaitForSeconds(0.2f);
        _soundOnColdown = true;
    }
}
