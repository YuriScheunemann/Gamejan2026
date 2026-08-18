using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatPanel : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private string mapSceneName;

    private void Start()
    {
        defeatPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowDefeat()
    {
        defeatPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mapSceneName);
    }
}
