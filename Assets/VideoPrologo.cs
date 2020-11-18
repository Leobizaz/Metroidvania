using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPrologo : MonoBehaviour
{
    VideoPlayer video;

    private void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.loopPointReached += EndVideo;
        video.SetDirectAudioVolume(0, MenuController.Saved_volumeMusica);
    }

    void EndVideo(VideoPlayer vp)
    {
        vp.playbackSpeed = 0;
        PlayerController.prologo = false;
        PlayerController.jogoNovo = true;
        SceneManager.LoadScene("CENAPRINCIPAL");
    }

}
