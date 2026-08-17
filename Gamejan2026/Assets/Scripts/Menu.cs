using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    [Header("Botoes")]
    public Button playButton;
    public Button quitButton;
    public Button closeButton;
    public Button controlsButton;

    public GameObject creditosPanel;
    public GameObject controlsPanel;
    public UnityEvent Credits;
    public UnityEvent Controls;
    public UnityEvent close;
    [Header("Configuracoes")]
    public string sceneToLoad = "GameScene";

    void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public void AbrirCreditos()
    {
        Credits.Invoke();
    }
    public void OpenControls()
    {
        Controls.Invoke();
    }

    public void Close()
    {
        close.Invoke();
    }
}
