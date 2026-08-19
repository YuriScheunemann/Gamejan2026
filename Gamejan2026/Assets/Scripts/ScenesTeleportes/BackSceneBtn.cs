using UnityEngine;
using UnityEngine.SceneManagement;

public class BackSceneBtn : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void SceneButton()
    {
        SceneManager.LoadScene(sceneName);
    }
}
