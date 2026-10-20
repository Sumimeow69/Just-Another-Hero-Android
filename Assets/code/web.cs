using UnityEngine;
using UnityEngine.Video;
using System.IO;
public class web : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string nombreVideo = "JSJDJASK.mp4";

    private void Awake()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        string rutaVideo = Application.streamingAssetsPath + "/" + nombreVideo;

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = rutaVideo;
        videoPlayer.playOnAwake = false;

        videoPlayer.Prepare();
    }

    private void Start()
    {
        videoPlayer.prepareCompleted += ReproducirCuandoEsteListo;
    }

    private void ReproducirCuandoEsteListo(VideoPlayer vp)
    {
        vp.Play();
    }
}
