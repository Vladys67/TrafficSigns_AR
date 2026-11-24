using UnityEngine;

public class ARScreenCenterDisplay : MonoBehaviour
{
    [Header("Referințe")]
    public Transform arCamera;
    public GameObject signsRoot;
    public GameObject trackingMessage;
    public SignsCarousel signsCarousel;

    [Header("Poziționare pe centrul ecranului")]
    public float distanceFromCamera = 0.85f;
    public float verticalOffset = -0.05f;
    public Vector3 screenRotationOffset = new Vector3(0f, 0f, 0f);

    void Start()
    {
        // Optimizare performanță mobilă (FPS stabil)
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        if (signsRoot != null)
            signsRoot.SetActive(false);

        if (trackingMessage != null)
            trackingMessage.SetActive(true);
    }

    public void OnImageScanned()
    {
        if (signsRoot == null || arCamera == null) return;

        bool wasHidden = !signsRoot.activeSelf;

        signsRoot.transform.SetParent(arCamera, false);
        signsRoot.transform.localPosition = new Vector3(0f, verticalOffset, distanceFromCamera);
        signsRoot.transform.localRotation = Quaternion.Euler(screenRotationOffset);
        signsRoot.SetActive(true);

        if (trackingMessage != null)
            trackingMessage.SetActive(false);

        if (wasHidden && UIAudioManager.Instance != null)
            UIAudioManager.Instance.PlayScanSuccess();
    }

    public void OnImageLost()
    {
        if (signsCarousel != null)
            signsCarousel.RestoreCarousel();

        if (signsRoot != null)
            signsRoot.SetActive(false);

        // Recuperare vizuală la pierderea tracking-ului
        if (trackingMessage != null)
            trackingMessage.SetActive(true);
    }
}