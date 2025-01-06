using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] string SceneNameSF;
    [SerializeField] GameObject Loading;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; // Subscribe to the video end event
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(videoEndPlz());
    }

    IEnumerator videoEndPlz()
    {
        yield return new WaitForSeconds(5);
        Loading.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneNameSF);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            videoPlayer.playbackSpeed = 7;
        }
        else
        {
            videoPlayer.playbackSpeed = 1;
        }
        
    }
}