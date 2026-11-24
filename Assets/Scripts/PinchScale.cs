using UnityEngine;

public class PinchScale : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float zoomSpeed = 0.01f;

    private Vector3 initialScale;
    private float scaleMultiplier = 1f;

    void Awake()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Marire si micsorare cu doua degete
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 pos1Anterior = touch1.position - touch1.deltaPosition;
            Vector2 pos2Anterior = touch2.position - touch2.deltaPosition;

            float distantaAnterioara =
                Vector2.Distance(pos1Anterior, pos2Anterior);

            float distantaActuala =
                Vector2.Distance(touch1.position, touch2.position);

            float diferenta = distantaActuala - distantaAnterioara;

            ChangeScale(diferenta * zoomSpeed);
        }

#if UNITY_EDITOR
        // Testare cu rotita mouse-ului
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            ChangeScale(scroll * 0.1f);
        }
#endif
    }

    void ChangeScale(float change)
    {
        scaleMultiplier += change;

        scaleMultiplier = Mathf.Clamp(
            scaleMultiplier,
            minScale,
            maxScale
        );

        transform.localScale = initialScale * scaleMultiplier;
    }

    public void ResetScale()
    {
        scaleMultiplier = 1f;
        transform.localScale = initialScale;
    }
}