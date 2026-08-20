using UnityEngine;

public class Interaction : MonoBehaviour
{
    private IInteractable _target;

    void Update()
    {
        if (_target == null)
            return;
        if (Input.GetKeyDown(KeyCode.E))
        {

            _target.Active();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _target))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _target = null;
    }
}
