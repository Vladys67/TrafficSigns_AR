using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [Header("Sursa Audio (AudioSource)")]
    public AudioSource sfxSource;

    [Header("Volume Individuale")]
    [Range(0f, 1f)] public float buttonClickVolume = 0.08f;
    [Range(0f, 1f)] public float quizFeedbackVolume = 1.0f;

    [Header("Efecte Sonore (SFX)")]
    public AudioClip scanSuccessClip;
    public AudioClip signSelectClip;
    public AudioClip buttonClickClip;
    public AudioClip closeButtonClip;
    public AudioClip quizCorrectClip;
    public AudioClip quizWrongClip;
    public AudioClip quizFinishClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Forțăm volumul click-ului la 0.08 ca să nu fie suprascris de valoarea veche din Inspector
        buttonClickVolume = 0.08f;
    }

    public void PlayScanSuccess() => PlayClip(scanSuccessClip, 0.5f, "ScanSuccess");
    public void PlaySignSelect() => PlayClip(signSelectClip, 0.55f, "SignSelect");
    public void PlayButtonClick() => PlayClip(buttonClickClip, buttonClickVolume, "ButtonClick");

    public void PlayCloseButton()
    {
        AudioClip clipToUse = closeButtonClip != null ? closeButtonClip : buttonClickClip;
        PlayClip(clipToUse, buttonClickVolume, "CloseButton");
    }

    public void PlayQuizCorrect() => PlayClip(quizCorrectClip, quizFeedbackVolume, "QuizCorrect");
    public void PlayQuizWrong() => PlayClip(quizWrongClip, quizFeedbackVolume, "QuizWrong");
    public void PlayQuizFinish() => PlayClip(quizFinishClip, 0.9f, "QuizFinish");

    private void PlayClip(AudioClip clip, float volume, string soundName)
    {
        if (sfxSource == null)
        {
            Debug.LogError("UIAudioManager: Câmpul Sfx Source NU este legat în Inspector!");
            return;
        }

        if (clip == null)
        {
            Debug.LogWarning($"UIAudioManager: Lipsește fișierul audio (AudioClip) pentru '{soundName}' în Inspector!");
            return;
        }

        sfxSource.PlayOneShot(clip, volume);
    }
}