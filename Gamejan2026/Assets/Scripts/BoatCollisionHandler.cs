using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BoatCollisionHandler : MonoBehaviour
{
    // Tags esperadas nos prefabs: "Pipe", "Bag", "Turtle"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {
            GameManager.Instance?.HitPipe();
        }
        else if (other.CompareTag("Bag"))
        {
            GameManager.Instance?.HitBag();
            // opcional: destruir a sacola ao colidir
            Destroy(other.gameObject);
        }
        // tartaruga trata colecção no próprio prefab (ver TurtlePickup), mas também pode ser
        // detectada aqui caso prefira.
    }
}