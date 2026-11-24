using UnityEngine;
using UnityEngine.EventSystems;

public class SignTouchManager : MonoBehaviour
{
    public Camera arCamera;
    public SignsCarousel signsCarousel;

    void Update()
    {
        // Ignorăm click-ul/atingerea dacă se apasă pe un buton din UI (X, Ascultă, Quiz etc.)
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            CheckHit(Input.mousePosition);
        }

        // Touch pe telefon
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            CheckHit(Input.GetTouch(0).position);
        }
    }

    void CheckHit(Vector2 screenPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Am lovit: " + hit.collider.name);

            TrafficSign sign = hit.collider.GetComponentInParent<TrafficSign>();

            if (sign != null)
            {
                sign.OpenInfo();

                if (signsCarousel != null)
                {
                    signsCarousel.FocusOnSign(sign.transform);
                }
            }
        }
    }
}