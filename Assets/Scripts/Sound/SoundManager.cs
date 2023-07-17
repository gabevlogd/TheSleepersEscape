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
    private AudioSource m_audioSourceGenerator;

    [SerializeField]
    private AudioSource m_audioSourceDoor;

    [SerializeField]
    private AudioSource m_audioSourceRadio;

    [SerializeField]
    private AudioSource m_audioSourceEnv;

    [SerializeField]
    private AudioSource m_audioSourceSteps;

    [SerializeField]
    private AudioSource m_audioSourceGO15;

    [SerializeField]
    private AudioSource m_audioSourceFax;


    [Header("-----AudioClip-----")]
    public AudioClip PlayerStep, PickUp, ThrowDart, TargetBoard,TargetWall, GO15SwitchTile, Watch, SelectedTiled, Pair, NoPair, SuccessPuzzle, FailedPuzzle, SwitchLight, MasterSwitch, AllLightPowerOn, AllLightPowerOff, DoorDial, DoorLight, DoorOpen, WalkmanPlay, WalkmanStop, Radio,Fax, PenSound,GeneratorSound;



    private void OnEnable()
    {
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundPlayer, PlayFromPlayerAS);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundDoor, PlayFromDoorAS);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundRadio, PlayFromRadioAS);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundEnv, PlayFromEnvAS);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundGenerator, PlayFromGeneratorAS);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlayStepsSound, PlayStepsSound);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.StopStepsSound, StopStepsSound);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlaySoundGO15, PlayFromGO15AS);
        GameManager.Instance.SoundEventManager.Register<AudioClip>(Enumerators.MusicEvents.PlayFaxSound, PlayFromFaxAS);
        GameManager.Instance.EventManager.Register(Enumerators.Events.TurnOffLights, TurnOffGenerator);
        GameManager.Instance.EventManager.Register(Enumerators.Events.TurnOnLights, GeneratorPowerOn);

    }


    private void OnDisable()
    {
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundPlayer, PlayFromPlayerAS);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundDoor, PlayFromDoorAS);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundRadio, PlayFromRadioAS);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundEnv, PlayFromEnvAS);
        GameManager.Instance.SoundEventManager.Unregister<AudioClip>(Enumerators.MusicEvents.PlaySoundGenerator, PlayFromGeneratorAS);
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.TurnOffLights, TurnOffGenerator);
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.TurnOnLights, GeneratorPowerOn);
    }



    private void PlayFromPlayerAS(AudioClip audioClip)
    {
        if (m_audioSourcePlayer.isPlaying) return;
        m_audioSourcePlayer.clip = audioClip;
        m_audioSourcePlayer.Play();
    }



    private void PlayFromDoorAS(AudioClip audioClip)
    {
        m_audioSourceDoor.clip = audioClip;
        m_audioSourceDoor.Play();
    }

    private void PlayFromRadioAS(AudioClip audioClip)
    {
        m_audioSourceRadio.clip = audioClip;
        m_audioSourceRadio.Play();
    }

    private void PlayFromEnvAS(AudioClip audioClip)
    {
        m_audioSourceEnv.clip = audioClip;
        m_audioSourceEnv.Play();
    }

    private void PlayFromGeneratorAS(AudioClip audioClip)
    {
        m_audioSourceGenerator.clip = audioClip;
        m_audioSourceGenerator.Play();
    }

    private void PlayFromFaxAS(AudioClip audioClip)
    {
        m_audioSourceFax.clip = audioClip;
        m_audioSourceFax.Play();
    }

    private void PlayStepsSound(AudioClip audioClip)
    {
        if (m_audioSourceSteps.isPlaying) return;
        m_audioSourceSteps.Play();
    }

    private void StopStepsSound(AudioClip audioClip)
    {
        m_audioSourceSteps.Stop();
    }

    private void PlayFromGO15AS(AudioClip audioClip)
    {
        m_audioSourceGO15.clip = audioClip;
        m_audioSourceGO15.Play();
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
        m_audioSourceGenerator.clip = AllLightPowerOff;
        if(!m_audioSourceGenerator.isPlaying)
        {
            m_audioSourceGenerator.enabled = false;
            m_audioSourceGenerator.loop = false;
        }
    }


    private void GeneratorPowerOn()
    {
        m_audioSourceGenerator.enabled = true;
        m_audioSourceGenerator.clip = AllLightPowerOn;
        if (!m_audioSourceGenerator.isPlaying)
        {
            m_audioSourceGenerator.clip = GeneratorSound;
            m_audioSourceGenerator.loop = true;
        }
    }

}
