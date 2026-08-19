using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string playerTag = "Player";
    [Tooltip("Se verdadeiro, checa se a cena existe nas Build Settings antes de carregar.")]
    [SerializeField] private bool checkBuildSettings = true;

    private void TryLoadScene()
    {
        if (string.IsNullOrWhiteSpace(sceneToLoad))
        {
            Debug.LogWarning($"{name}: 'sceneToLoad' está vazio. Defina o nome da cena no Inspector.");
            return;
        }

        if (checkBuildSettings && !SceneIsInBuildSettings(sceneToLoad))
        {
            Debug.LogWarning($"{name}: Cena '{sceneToLoad}' não encontrada em Build Settings.");
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    private bool SceneIsInBuildSettings(string sceneName)
    {
        int count = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < count; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (string.Equals(name, sceneName, System.StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    // 3D Physics
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag)) TryLoadScene();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(playerTag)) TryLoadScene();
    }

    // 2D Physics
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag)) TryLoadScene();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(playerTag)) TryLoadScene();
    }
}   