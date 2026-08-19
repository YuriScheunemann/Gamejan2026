using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTeleport : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            SceneManager.LoadScene(_sceneName);
    }
}
