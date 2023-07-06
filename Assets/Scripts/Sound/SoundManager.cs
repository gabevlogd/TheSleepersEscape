using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource m_audioSource;

    [SerializeField]
    private AudioSource m_BackGroundAudioSource;

    public AudioClip PlayerStep, PickUp, ThrowDart, TargetBoard, GO15SwitchTile, Watch, SelectedTiled, Pair, NoPair, SuccessPuzzle, FailedPuzzle, SwitchLight, MasterSwitch, LampOn, LampOff, AllLightPowerOn, AllLightPowerOff, DoorDial, DoorLight, DoorOpen, WalkmanPlay, WalkmanStop, Radio,Fax;


    private void OnEnable()
    {
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlayeSound, ChangeClipAndPlay);
    }


    private void OnDisable()
    {
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlayeSound, ChangeClipAndPlay);
    }

    private void Start()
    {
        m_audioSource.enabled = true;
        m_BackGroundAudioSource.enabled = true;
    }


    private void ChangeClipAndPlay(AudioClip audioClip)
    {
        m_audioSource.clip = audioClip;
        m_audioSource.Play();

    }



}
