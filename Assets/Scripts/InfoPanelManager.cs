using UnityEngine;
using TMPro;

public class InfoPanelManager : MonoBehaviour
{
    public GameObject infoPanel;
    public TMP_Text titleText;
    public TMP_Text categoryText;
    public TMP_Text descriptionText;
    public AudioSource audioSource;

    private AudioClip currentAudio;

    void Start()
    {
        infoPanel.SetActive(false);
    }

    public void ShowInfo(string title, string category, string description)
    {
        ShowInfo(title, category, description, null);
    }

    public void ShowInfo(string title, string category,
                         string description, AudioClip audio)
    {
        audioSource.Stop();

        titleText.text = title;
        categoryText.text = "Categorie: " + category;
        descriptionText.text = description;

        currentAudio = audio;
        infoPanel.SetActive(true);
    }

    public void PlayAudio()
    {
        if (currentAudio != null)
        {
            audioSource.Stop();
            audioSource.clip = currentAudio;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Nu exista audio pentru acest indicator.");
        }
    }

    public void CloseInfo()
    {
        audioSource.Stop();
        infoPanel.SetActive(false);
    }
}