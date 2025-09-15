using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BackgroundMusicController : MonoBehaviour
{
    public enum MusicTrack
    {
        EarlyFloors,
        MidFloors,
        LateFloors,
        Etc
    }
    [SerializeField] private BackgroundMusicTracks bgmTracks;
    private Dictionary<MusicTrack, AudioSource> musicTracks;
    private int currentSceneIndex;
    [SerializeField] EndLevelScript endlevelBool;

    void Start()
    {

        endlevelBool.OnLevelEnd += HandleLevelEnd;
        musicTracks = new Dictionary<MusicTrack, AudioSource>
        {
            { MusicTrack.EarlyFloors, bgmTracks.earlyFloorsClip },
            { MusicTrack.MidFloors, bgmTracks.midFloorsClip },
            { MusicTrack.LateFloors, bgmTracks.lateFloorsClip },
            { MusicTrack.Etc, bgmTracks.etcClip }
        };

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AssignMusicTrack(currentSceneIndex);
    }

    private void AssignMusicTrack(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                musicTracks[MusicTrack.EarlyFloors].Play();
                Debug.Log("Playing EarlyFloors music track.", musicTracks[MusicTrack.EarlyFloors]);
                break;
            case 1:
                musicTracks[MusicTrack.MidFloors].Play();
                Debug.Log("Playing MidFloors music track.", musicTracks[MusicTrack.MidFloors]);
                break;
            case 2:
                musicTracks[MusicTrack.LateFloors].Play();
                Debug.Log("Playing LateFloors music track.", musicTracks[MusicTrack.LateFloors]);
                break;
            default:
                ; //Do nothing
                break;
        }
    }
    private void HandleLevelEnd()
    {
        foreach (var track in musicTracks)
        {
            if (track.Value.isPlaying)
            {
                track.Value.Stop();
            }
            
        }
    }

}