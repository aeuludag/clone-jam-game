using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Video1Manager : MonoBehaviour
{

    public string roomToTransform;
    public string spawnPointName;
    public VideoPlayer videoPlayer;
    public string videoName;
    void Start()
    {
        if (!string.IsNullOrEmpty(videoName))
        {
            VideoClip clip = Resources.Load<VideoClip>(videoName);
            if (clip != null)
            {
                videoPlayer.clip = clip;
                videoPlayer.Play();
            }
            else
            {
                Debug.LogError("Video not found in Resources folder: " + videoName);
                SkipVideo(); // Fallback
            }
        }

        // 2. Listen for the end
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SkipVideo();
    }

    void SkipVideo()
    {
        // Load the actual room we wanted to go to
        SceneManager.LoadScene(roomToTransform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
