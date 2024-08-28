using Unity.VisualScripting;
using UnityEngine;

public class FMODContinuousPlayback : MonoBehaviour
{
    private string fmodEventPath = "event:/StepsLoop";

    private FMOD.Studio.EventInstance fmodEventInstance;

    [SerializeField] private InputController inputController;

    void Start()
    {
        fmodEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/StepsLoop");
    }

    void Update()
    {
        if (inputController.GetPlayerMovement().magnitude > 0)
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
