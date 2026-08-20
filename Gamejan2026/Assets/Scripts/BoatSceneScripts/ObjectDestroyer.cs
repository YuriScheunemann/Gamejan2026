using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [Header("Tags que podem ser destruídas")]
    [SerializeField]
    private string[] tagsParaDestruir =
    {
        "sapo",
        "fita",
        "obstaculo"
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        TentarDestruir(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TentarDestruir(collision.collider);
    }

    private void TentarDestruir(Collider2D other)
    {
        if (other == null)
            return;

        GameObject objeto = other.gameObject;

        // Se o collider estiver em um filho,
        // tenta usar o GameObject do Rigidbody.
        if (other.attachedRigidbody != null)
        {
            objeto = other.attachedRigidbody.gameObject;
        }

        // Se ainda não encontrou a tag, tenta o objeto pai.
        if (!TemTagValida(objeto))
        {
            Transform root = other.transform.root;

            if (root != null && TemTagValida(root.gameObject))
            {
                objeto = root.gameObject;
            }
        }

        if (!TemTagValida(objeto))
            return;

        Debug.Log(
            "ObjectDestroyer: destruindo " + objeto.name
        );

        Destroy(objeto);
    }

    private bool TemTagValida(GameObject objeto)
    {
        if (objeto == null)
            return false;

        foreach (string tag in tagsParaDestruir)
        {
            if (objeto.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }
}