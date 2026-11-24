using UnityEngine;
using Vuforia;

public class TrackingUIManager : MonoBehaviour
{
    public GameObject trackingMessage;
    public ObserverBehaviour imageTarget;

    void OnEnable()
    {
        if (imageTarget != null)
            imageTarget.OnTargetStatusChanged += OnTrackingChanged;
    }

    void Start()
    {
        UpdateMessage();
    }

    void OnDisable()
    {
        if (imageTarget != null)
            imageTarget.OnTargetStatusChanged -= OnTrackingChanged;
    }

    void OnTrackingChanged(ObserverBehaviour observer, TargetStatus status)
    {
        UpdateMessage();
    }

    void UpdateMessage()
    {
        if (trackingMessage == null || imageTarget == null)
            return;

        Status status = imageTarget.TargetStatus.Status;

        bool detected =
            status == Status.TRACKED ||
            status == Status.EXTENDED_TRACKED;

        trackingMessage.SetActive(!detected);
    }
}