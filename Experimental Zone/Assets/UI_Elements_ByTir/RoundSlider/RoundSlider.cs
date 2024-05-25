using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoundSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider X;
    private bool onDown = false;
    private float ParentWight;
    private float ParentWighp;
    public Transform ParentScale;
    private Vector3 StartVector;

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
        if (onDown)
        {
            Vector3 Temp = ((new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z) - ParentScale.position).normalized) * ParentWight;
            transform.position = ParentScale.position + new Vector3(Temp.x, Temp.y, transform.position.z);
            float ang;

            if (transform.position.y < ParentScale.position.y) ang = Mathf.Acos((transform.position.x - ParentScale.position.x) / ParentWighp);
            else ang = Mathf.PI*2- Mathf.Acos((transform.position.x - ParentScale.position.x) / ParentWighp);
            X.value = X.minValue + (X.maxValue - X.minValue) * (ang/ (Mathf.PI * 2));
        }
    }
    void Start()
    {
        X.interactable = false;
        ParentWight = Vector3.Distance(transform.position,ParentScale.position);
        StartVector = transform.position-ParentScale.position;
        ParentWighp = (transform.position.x - ParentScale.position.x);
    }
}
