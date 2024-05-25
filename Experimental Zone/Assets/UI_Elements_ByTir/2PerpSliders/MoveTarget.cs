using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveTarget : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider X;
    public Slider Y;
    private bool onDown;
    private Vector3 StartPosition;
    float ScaleVert, ScaleHoriz;
    public float CorrectionScale = 1;
    public Transform ParentScale;

    public void OnPointerDown(PointerEventData data)
    {
        onDown = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        onDown = false;
    }
    public void Update()
    {
        if(onDown)
        {
            Vector3 Temp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            if (Temp.x > StartPosition.x + ScaleHoriz) Temp.x = StartPosition.x + ScaleHoriz;
            else if (Temp.x < StartPosition.x - ScaleHoriz) Temp.x = StartPosition.x - ScaleHoriz;

            if (Temp.y > StartPosition.y + ScaleVert) Temp.y = StartPosition.y + ScaleVert;
            else if (Temp.y < StartPosition.y - ScaleVert) Temp.y = StartPosition.y - ScaleVert;

            transform.position = Temp;
            X.transform.position = new Vector3(X.transform.position.x, Temp.y , X.transform.position.z);
            Y.transform.position = new Vector3(Temp.x, Y.transform.position.y, Y.transform.position.z);

            Y.value = Y.minValue + (Y.maxValue - Y.minValue) * (((Temp.y - StartPosition.y) / ScaleVert) + 1) / 2;
            X.value = X.minValue + (X.maxValue - X.minValue) * (((Temp.x - StartPosition.x) / ScaleHoriz) + 1) / 2;    
        }
    }
    void Start()
    {
        StartPosition = transform.position;
        X.interactable = false;
        Y.interactable = false;
        ScaleVert = X.GetComponent<RectTransform>().rect.width * X.transform.localScale.x * ParentScale.localScale.x * CorrectionScale / 2;
        ScaleHoriz = Y.GetComponent<RectTransform>().rect.width * Y.transform.localScale.x * ParentScale.localScale.y * CorrectionScale / 2;
    }
}
