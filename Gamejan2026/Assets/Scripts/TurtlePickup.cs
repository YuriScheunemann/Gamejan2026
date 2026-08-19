using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TurtlePickup : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            GameManager.Instance?.CollectTurtle();
            Destroy(gameObject);
        }
    }
}