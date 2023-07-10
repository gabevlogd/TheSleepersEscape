using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    [Header("-----AudioMixer-----")]
    [SerializeField] private AudioMixer m_audioMixer;


    [Header("-----AudioSource-----")]
    [SerializeField]
    private AudioSource m_audioSourcePlayer;

    [SerializeField]
    private AudioSource m_generatorSound;

    [SerializeField]
    private AudioSource m_audioSourceDoor;

    [SerializeField]
    private AudioSource m_audioSourceRadio;

    [SerializeField]
    private AudioSource m_audioSourceEnv;


    [Header("-----AudioClip-----")]
    public AudioClip PlayerStep, PickUp, ThrowDart, TargetBoard,TargetWall, GO15SwitchTile, Watch, SelectedTiled, Pair, NoPair, SuccessPuzzle, FailedPuzzle, SwitchLight, MasterSwitch, AllLightPowerOn, AllLightPowerOff, DoorDial, DoorLight, DoorOpen, WalkmanPlay, WalkmanStop, Radio,Fax, PenSound,GeneratorSound;



    private void OnEnable()
    {
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundPlayer, ChangeClipAndPlayForPlayer);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundDoor, ChangeClipAndPlayForDoor);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundRadio, ChangeClipAndPlayForRadio);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundDoor, ChangeClipAndPlayForPlayer);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundEnv, ChangeClipAndPlayForEnv);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundGenerator, ChangeClipAndPlayForGenerator);
        GameManager.Instance.EventManager.Register(Enumerators.Events.TurnOffLights, TurnOffGenerator);
        GameManager.Instance.EventManager.Register(Enumerators.Events.TurnOnLights, GeneratorPowerOn);

    }


    private void OnDisable()
    {
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundPlayer, ChangeClipAndPlayForPlayer);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundDoor, ChangeClipAndPlayForDoor);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundRadio, ChangeClipAndPlayForRadio);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundEnv, ChangeClipAndPlayForEnv);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundGenerator, ChangeClipAndPlayForGenerator);
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.TurnOffLights, TurnOffGenerator);
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.TurnOnLights, GeneratorPowerOn);
    }



    private void ChangeClipAndPlayForPlayer(AudioClip audioClip)
    {
        if (m_audioSourcePlayer.isPlaying) return;
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

    private void ChangeClipAndPlayForEnv(AudioClip audioClip)
    {

        m_audioSourceEnv.clip = audioClip;
        m_audioSourceEnv.Play();

    }

    private void ChangeClipAndPlayForGenerator(AudioClip audioClip)
    {
        m_generatorSound.clip = audioClip;
        m_generatorSound.Play();

    }


    public void SetMasterVolum(float volume)
    {
        m_audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
  
    }

    public void SetMusicVolum(float volume)
    {
        m_audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);

    }

    public void SetSFXVolum(float volume)
    {
        m_audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);

    }


    private void TurnOffGenerator()
    {
        m_generatorSound.clip = AllLightPowerOff;
        if(!m_generatorSound.isPlaying)
        {
            m_generatorSound.enabled = false;
            m_generatorSound.loop = false;
        }
    }


    private void GeneratorPowerOn()
    {
        m_generatorSound.enabled = true;
        m_generatorSound.clip = AllLightPowerOn;
        if (!m_generatorSound.isPlaying)
        {
            m_generatorSound.clip = GeneratorSound;
            m_generatorSound.loop = true;
        }
    }

}
