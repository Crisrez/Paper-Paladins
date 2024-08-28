using UnityEngine;

public class FMODContinuousPlayback : MonoBehaviour
{
    private string fmodEventPath = "event:/StepsLoop";

    private FMOD.Studio.EventInstance fmodEventInstance;

    void Start()
    {
        fmodEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/StepsLoop");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.DownArrow))
        {
            PlayFMODEvent();
        }
        else
        {
            PauseFMODEvent();
        }
    }

    void PlayFMODEvent()
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        fmodEventInstance.getPlaybackState(out playbackState);

        if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            fmodEventInstance.start();
        }
        else
        {
            fmodEventInstance.setPaused(false);
        }
    }

    void PauseFMODEvent()
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        fmodEventInstance.getPlaybackState(out playbackState);

        if (playbackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            fmodEventInstance.setPaused(true);
        }
    }

    void OnDestroy()
    {
        fmodEventInstance.release();
    }
}
