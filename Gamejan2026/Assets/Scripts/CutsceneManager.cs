using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string proximaCena;

    private void Start()
    {
        videoPlayer.loopPointReached += VideoTerminou;
    }

    private void VideoTerminou(VideoPlayer vp)
    {
        SceneManager.LoadScene(proximaCena);
    }
}