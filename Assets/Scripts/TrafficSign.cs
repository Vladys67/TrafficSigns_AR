using System.Collections;
using UnityEngine;

public class TrafficSign : MonoBehaviour
{
    public InfoPanelManager infoManager;
    public TrafficSignData data;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    public void OpenInfo()
    {
        if (infoManager == null)
            return;

        if (data != null)
        {
            infoManager.ShowInfo(
                data.signName,
                data.category,
                data.description,
                data.audioClip
            );

            StopAllCoroutines();
            StartCoroutine(ClickEffect());
        }
        else
        {
            // Cale de recuperare pentru date invalide/lipsă
            infoManager.ShowInfo(
                "Date indisponibile",
                "Necunoscut",
                "Informațiile pentru acest indicator nu sunt configurate.",
                null
            );
        }
    }

    IEnumerator ClickEffect()
    {
        transform.localScale = initialScale * 1.2f;
        yield return new WaitForSeconds(0.2f);
        transform.localScale = initialScale;
    }
}