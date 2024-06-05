using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;
    public KeyCode rotateKey = KeyCode.R; // Taste zum Drehen
    public float rotationSpeed = 100f; // Geschwindigkeit der Drehung
    private bool isDragging = false;
    private Image objectImage;
    private Color originalColor;
    public Color hoverColor = Color.yellow; // Farbe beim Hovern
    public float fadeDuration = 1f; // Dauer des Ein- und Ausblendens

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        objectImage = GetComponent<Image>();
        if (objectImage != null)
        {
            originalColor = objectImage.color;
        }
        else
        {
            Debug.LogError("Image component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (isDragging && Input.GetKey(rotateKey))
        {
            float rotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotation); // Drehung um die Z-Achse
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        isDragging = false;
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (objectImage != null)
        {
            StopAllCoroutines(); // Stoppe vorherige Coroutine, falls vorhanden
            StartCoroutine(FadeToColor(hoverColor));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (objectImage != null)
        {
            StopAllCoroutines(); // Stoppe vorherige Coroutine, falls vorhanden
            StartCoroutine(FadeToColor(originalColor));
        }
    }

    private IEnumerator FadeToColor(Color targetColor)
    {
        Color currentColor = objectImage.color;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            objectImage.color = Color.Lerp(currentColor, targetColor, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        objectImage.color = targetColor;
    }
}
