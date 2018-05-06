using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

    private Image BGImg;
    private Image joyImg;
    private Vector3 inputVector;

    void Start () {
        BGImg = GetComponent<Image>();
        joyImg = transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnDrag(PointerEventData eventData)
    {
        // calculate the joystick position on the control area
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BGImg.rectTransform,
                                                                        eventData.position,
                                                                        eventData.pressEventCamera,
                                                                        out pos))
        {
            pos.x = (pos.x / BGImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / BGImg.rectTransform.sizeDelta.y);
            Debug.Log("Position" + pos);
            inputVector = new Vector3(pos.x * 2 - 1, 0, pos.y * 2 - 1);
            Debug.Log("inputVector" + inputVector);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joyImg.rectTransform.anchoredPosition = new Vector3(
                                                        inputVector.x * BGImg.rectTransform.sizeDelta.x / 2.15f,
                                                        inputVector.z * BGImg.rectTransform.sizeDelta.y / 2.15f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joyImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else return Input.GetAxis("Vertical");
    }
}
