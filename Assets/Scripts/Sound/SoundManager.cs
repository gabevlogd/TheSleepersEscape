using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource m_audioSourcePlayer;

    [SerializeField]
    private AudioSource m_generatorSound;

    [SerializeField]
    private AudioSource m_audioSourceDoor;

    [SerializeField]
    private AudioSource m_audioSourceRadio;

    [SerializeField]
    private AudioSource m_audioSourceWatch;


    public AudioClip PlayerStep, PickUp, ThrowDart, TargetBoard,TargetWall, GO15SwitchTile, Watch, SelectedTiled, Pair, NoPair, SuccessPuzzle, FailedPuzzle, SwitchLight, MasterSwitch, LampOn, LampOff, AllLightPowerOn, AllLightPowerOff, DoorDial, DoorLight, DoorOpen, WalkmanPlay, WalkmanStop, Radio,Fax;


    private void OnEnable()
    {
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundPlayer, ChangeClipAndPlayForPlayer);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundDoor, ChangeClipAndPlayForDoor);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundRadio, ChangeClipAndPlayForRadio);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundDoor, ChangeClipAndPlayForPlayer);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundWatch, ChangeClipAndPlayForWatch);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundGenerator, ChangeClipAndPlayForGenerator);
    }


    private void OnDisable()
    {
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundPlayer, ChangeClipAndPlayForPlayer);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundDoor, ChangeClipAndPlayForDoor);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundRadio, ChangeClipAndPlayForRadio);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundWatch, ChangeClipAndPlayForWatch);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundGenerator, ChangeClipAndPlayForGenerator);
    }



    private void ChangeClipAndPlayForPlayer(AudioClip audioClip)
    {
        m_audioSourcePlayer.clip = audioClip;
        m_audioSourcePlayer.Play();

    }

    private void ChangeClipAndPlayForDoor(AudioClip audioClip)
    {
        m_audioSourceDoor.clip = audioClip;
        m_audioSourceDoor.Play();

    }


    private void ChangeClipAndPlayForRadio(AudioClip audioClip)
    {
        m_audioSourceRadio.clip = audioClip;
        m_audioSourceRadio.Play();

    }


    private void ChangeClipAndPlayForWatch(AudioClip audioClip)
    {
        m_audioSourceWatch.clip = audioClip;
        m_audioSourceWatch.Play();

    }




    private void ChangeClipAndPlayForGenerator(AudioClip audioClip)
    {
        m_generatorSound.clip = audioClip;
        m_generatorSound.Play();

    }





}
