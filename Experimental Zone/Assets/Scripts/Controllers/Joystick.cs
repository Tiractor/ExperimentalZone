using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events; // Для использования событий.

public class Joystick : TaskExecutor<Joystick>, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool onDown;
    private Vector2 startLocalPosition;
    private float radius;
    private RectTransform rect_trans;
    public UnityEvent m_OnChange;
    public Vector2 Axes; 

    public void OnPointerDown(PointerEventData data)
    {
        onDown = true;
        OnDrag(data); // Если мы сразу же хотим переместить джойстик в точку касания
    }

    public void OnPointerUp(PointerEventData data)
    {
        onDown = false;
        // Возврат в исходную позицию
        transform.localPosition = startLocalPosition;
        Axes = Vector2.zero;
    }

    public void OnDrag(PointerEventData data)
    {
        if (onDown)
        {
            Vector2 direction = data.position - (Vector2)transform.parent.position;
            direction = Vector2.ClampMagnitude(direction, radius);
            transform.position = transform.parent.position + (Vector3)direction;
            Axes = rect_trans.anchoredPosition / 20f;
        }
    }

    void Start()
    {
        Denote();
        rect_trans = GetComponent<RectTransform>();
        startLocalPosition = transform.localPosition;
        RectTransform par = transform.parent.GetComponent<RectTransform>();
        radius = par.rect.width * 0.5f * par.localScale.x;
    }
}