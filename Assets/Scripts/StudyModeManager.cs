using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudyModeManager : MonoBehaviour
{
    public SignTouchManager signTouchManager;
    public PinchScale pinchScale;
    public SignsCarousel signsCarousel;

    public GameObject infoPanel;
    public AudioSource infoAudioSource;

    public Button studyButton;
    public TMP_Text studyButtonText;

    void Start()
    {
        DeactivateStudyMode();
    }

    public void ActivateStudyMode()
    {
        SetStudyMode(true);
    }

    public void DeactivateStudyMode()
    {
        SetStudyMode(false);
    }

    private void SetStudyMode(bool active)
    {
        if (signTouchManager != null)
            signTouchManager.enabled = active;

        if (pinchScale != null)
            pinchScale.enabled = active;

        if (signsCarousel != null)
        {
            signsCarousel.isRotating = active;
            signsCarousel.enabled = active;
        }

        if (!active)
        {
            if (pinchScale != null)
                pinchScale.ResetScale();

            if (signsCarousel != null)
                signsCarousel.ResetCarousel();

            if (infoAudioSource != null)
                infoAudioSource.Stop();

            if (infoPanel != null)
                infoPanel.SetActive(false);
        }

        if (studyButton != null)
            studyButton.interactable = !active;

        if (studyButtonText != null)
            studyButtonText.text = active
                ? "Modul Studiere activ"
                : "Studiază";
    }
}