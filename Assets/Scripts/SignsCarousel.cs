using UnityEngine;

public class SignsCarousel : MonoBehaviour
{
    public float radius = 0.15f;
    public float rotationSpeed = 8f;
    public bool isRotating = false;

    [Header("Poziție & Efect Mărire Indicator Selectat")]
    public Vector3 focusedLocalPosition = new Vector3(0f, 0.06f, 0f);
    public float focusedScaleMultiplier = 1.35f;
    public float focusTransitionSpeed = 8f;

    private Transform[] signs;
    private Vector3[] originalScales;
    private float angle;
    private Transform focusedSign = null;
    private Vector3 targetFocusedScale;
    private bool wasRotatingBeforeFocus = false;

    void Awake()
    {
        radius = 0.15f;
        int count = transform.childCount;
        signs = new Transform[count];
        originalScales = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            signs[i] = transform.GetChild(i);
            originalScales[i] = signs[i].localScale;
        }
        UpdatePositions();
    }

    void OnEnable()
    {
        radius = 0.15f;
        if (focusedSign == null)
        {
            UpdatePositions();
        }
    }

    void Update()
    {
        if (focusedSign != null)
        {
            float t = Time.unscaledDeltaTime * focusTransitionSpeed;
            focusedSign.localPosition = Vector3.Lerp(focusedSign.localPosition, focusedLocalPosition, t);
            focusedSign.localScale = Vector3.Lerp(focusedSign.localScale, targetFocusedScale, t);
            return;
        }

        if (isRotating)
        {
            angle += rotationSpeed * Time.deltaTime;
        }

        UpdatePositions();
    }

    private void UpdatePositions()
    {
        if (signs == null) return;

        for (int i = 0; i < signs.Length; i++)
        {
            if (signs[i] == null) continue;

            float a = (angle + i * 360f / signs.Length) * Mathf.Deg2Rad;

            signs[i].localPosition = new Vector3(
                Mathf.Cos(a) * radius,
                Mathf.Sin(a) * radius,
                0f
            );
        }
    }

    public void FocusOnSign(Transform selectedSign)
    {
        if (signs == null || selectedSign == null) return;

        if (focusedSign == null)
        {
            wasRotatingBeforeFocus = isRotating;
        }

        isRotating = false;

        if (UIAudioManager.Instance != null)
            UIAudioManager.Instance.PlaySignSelect();

        for (int i = 0; i < signs.Length; i++)
        {
            if (signs[i] == null) continue;

            bool isSelected = (signs[i] == selectedSign || selectedSign.IsChildOf(signs[i]));
            signs[i].gameObject.SetActive(isSelected);

            if (isSelected)
            {
                focusedSign = signs[i];
                signs[i].localScale = originalScales[i];
                targetFocusedScale = originalScales[i] * focusedScaleMultiplier;
            }
        }
    }

    public void RestoreCarousel()
    {
        if (signs == null) return;

        focusedSign = null;

        for (int i = 0; i < signs.Length; i++)
        {
            if (signs[i] == null) continue;

            signs[i].gameObject.SetActive(true);
            signs[i].localScale = originalScales[i];
        }

        isRotating = wasRotatingBeforeFocus;
        UpdatePositions();
    }

    public void ResetCarousel()
    {
        RestoreCarousel();
        angle = 0f;
        UpdatePositions();
    }

    public void ToggleCarousel()
    {
        isRotating = !isRotating;
    }
}