using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Setări Hover & Apăsare")]
    public float hoverScale = 1.08f;
    public float pressScale = 0.95f;
    public float smoothSpeed = 12f;

    [Header("Sunet Buton")]
    public bool isCloseButton = false;

    [Header("Nuanță culoare la Hover (opțional)")]
    public bool tintOnHover = true;
    public Color hoverTint = new Color(0.92f, 0.92f, 0.92f, 1f);

    private Vector3 originalScale;
    private Vector3 targetScale;
    private Image buttonImage;
    private Color originalColor;

    void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }
    }

    void OnEnable()
    {
        transform.localScale = originalScale;
        targetScale = originalScale;
        if (buttonImage != null)
        {
            buttonImage.color = originalColor;
        }
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * smoothSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
        if (tintOnHover && buttonImage != null)
        {
            buttonImage.color = originalColor * hoverTint;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        if (tintOnHover && buttonImage != null)
        {
            buttonImage.color = originalColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = originalScale * pressScale;

        if (UIAudioManager.Instance != null)
        {
            if (isCloseButton)
                UIAudioManager.Instance.PlayCloseButton();
            else
                UIAudioManager.Instance.PlayButtonClick();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetScale = originalScale;
        if (tintOnHover && buttonImage != null)
        {
            buttonImage.color = originalColor;
        }
    }
}