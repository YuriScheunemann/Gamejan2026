using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlayButton : MonoBehaviour
{
    public string ChangeScene;
    public void changeScene()
    {
        SceneManager.LoadScene(ChangeScene);
    }
}
