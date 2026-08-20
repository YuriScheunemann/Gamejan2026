using UnityEngine;
using UnityEngine.Events;

public class AirTaskActivation : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject letterIndication;
 
    public UnityEvent OnActive;
    public UnityEvent OnDesactive;
    private bool _isActive;
    private bool _once = true;

    public void Active()
    {
        if (_isActive)
        {
            OnDesactive.Invoke();
        }
        else
        {
            OnActive.Invoke();
        }

        _isActive = !_isActive;

        if (_once)
            Destroy(this, 0.15f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            letterIndication.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            letterIndication.SetActive(false);
        }
    }
}
