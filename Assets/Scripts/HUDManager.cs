using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject TutorialOne;
    public GameObject TutorialTwo;

    public Image CrossAir;
    public Image InteractablePointFeedback;
    public Image FadeEffect;
    public Image QInfoImage;

    public TextMeshProUGUI Info;

    public float FadeSpeed;
    public float TimeBetweenFades;
    public bool CanFade { get => m_canFade; } 

    private bool m_canFade;
    private int m_fadeDirection;
    private Color m_fadeColor;

    private void Awake()
    {
        GameManager.Instance.EventManager.Register(Enumerators.Events.ShowHud, ShowHUD);
        GameManager.Instance.EventManager.Register(Enumerators.Events.HideHud, HideHUD);

        GameManager.Instance.EventManager.Register(Enumerators.Events.ShowInteractablePoint, ShowInteractableFeedback);
        GameManager.Instance.EventManager.Register(Enumerators.Events.HideInteractablePoint, HideInteractableFeedback);

        GameManager.Instance.EventManager.Register(Enumerators.Events.StartFade, StartFade);

        GameManager.Instance.EventManager.Register(Enumerators.Events.NextTutorial, SetNextTutorial);
        GameManager.Instance.EventManager.Register(Enumerators.Events.CloseTutorial, CloseTutorial);


        GameManager.Instance.EventManager.Register(Enumerators.Events.StopInteraction, HideQInfo);
        GameManager.Instance.EventManager.Register(Enumerators.Events.TurnOffLights, ShowQInfo);


        TutorialOne.SetActive(true);
        TutorialTwo.SetActive(false);

    }

    private void Update() => HandleFade();


    public void ShowHUD() => HUD.SetActive(true);
    public void HideHUD() => HUD.SetActive(false);

    public void ShowQInfo()
    {
        QInfoImage.gameObject.SetActive(true);
        ShowHUD();
        HideCrossAir();
    }

    public void HideQInfo()
    {
        QInfoImage.gameObject.SetActive(false);
        ShowCrossAir();
    }

    public void ShowCrossAir() => CrossAir.gameObject.SetActive(true);
    public void HideCrossAir() => CrossAir.gameObject.SetActive(false);

    public void ShowInteractableFeedback()
    {
        if (InteractablePointFeedback != null)
            InteractablePointFeedback.gameObject.SetActive(true);
    }

    public void HideInteractableFeedback()
    {
        if (InteractablePointFeedback != null)
            InteractablePointFeedback.gameObject.SetActive(false);
    }


    private void StartFade()
    {
        if (GameManager.PlayerWin) m_fadeColor = new Color(1f,1f,1f,0f);
        else m_fadeColor = FadeEffect.color;

        if (m_fadeColor.a <= 0)
        {
            m_fadeDirection = 1;
            m_fadeColor.a = 0;
        }
        else if (m_fadeColor.a >= 1)
        {
            m_fadeDirection = -1;
            m_fadeColor.a = 1;
        }
        else return;

        m_canFade = true;
    }

    private void HandleFade()
    {
        if (m_canFade)
        {
            m_fadeColor.a += Time.deltaTime * FadeSpeed * m_fadeDirection;
            FadeEffect.color = m_fadeColor;

            if (m_fadeColor.a <= 0f || m_fadeColor.a >= 1f) m_canFade = false;
        }
    }

    public void SetNextTutorial()
    {
        TutorialOne.SetActive(false);
        TutorialTwo.SetActive(true);
    }

    public void CloseTutorial()
    {
        TutorialTwo.SetActive(false);
        //GameManager.Instance.Player.PlayerStateMachine.ChangeState(Enumerators.PlayerState.Navigation);

        StartCoroutine(TutorialEndMessage());
    }

    private IEnumerator TutorialEndMessage()
    {
        yield return new WaitForSeconds(0.5f);
        Info.text = "Good game";
        Info.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        CloseInfo();
    }

    private void CloseInfo()
    {
        Info.text = "";
        Info.gameObject.SetActive(false);
    }


    


}
