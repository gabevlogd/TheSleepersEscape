using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject HUD;
    public Image CrossAir;
    public Image InteractablePointFeedback;

    private void Awake()
    {
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.ShowHud, ShowHUD);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.HideHud, HideHUD);

        GameManager.Instance.EventManager.Registrer(Enumerators.Events.ShowInteractablePoint, ShowInteractableFeedback);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.HideInteractablePoint, HideInteractableFeedback);
    }

    public void ShowHUD() => HUD.SetActive(true);
    public void HideHUD() => HUD.SetActive(false);

    public void ShowInteractableFeedback() => InteractablePointFeedback.gameObject.SetActive(true);
    public void HideInteractableFeedback() => InteractablePointFeedback.gameObject.SetActive(false);


}
