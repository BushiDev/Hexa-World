using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler{

    public static VirtualJoystick instance;

    Image background, thumb;
    Vector3 inputVector;

    public JoystickType joystickType;

    void Awake(){

        instance = this;

    }

    void Start(){

        background = GetComponent<Image>();
        thumb = transform.GetChild(0).GetComponent<Image>();

    }

    public virtual void OnPointerDown(PointerEventData ped){

        OnDrag(ped);

    }

    public virtual void OnDrag(PointerEventData ped){

        Vector2 pos;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, ped.position, ped.pressEventCamera, out pos)){

            switch(joystickType){

                case JoystickType.X:
                    pos.x = (pos.x / background.rectTransform.sizeDelta.x);
                    pos.y = 0f;
                    break;
                case JoystickType.Y:
                    pos.x = 0;
                    pos.y = (pos.y / background.rectTransform.sizeDelta.y);
                    break;
                case JoystickType.BOTH:
                    pos.x = (pos.x / background.rectTransform.sizeDelta.x);
                    pos.y = (pos.y / background.rectTransform.sizeDelta.y);
                    break;

            }

            inputVector = new Vector3(pos.x, 0f, pos.y);

            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

            thumb.rectTransform.anchoredPosition = new Vector3(inputVector.x * background.rectTransform.sizeDelta.x / 2f, inputVector.z * background.rectTransform.sizeDelta.y / 2f);

        }

    }

    public virtual void OnPointerUp(PointerEventData ped){

        inputVector = Vector3.zero;
        thumb.rectTransform.anchoredPosition = Vector3.zero;

    }

    public Vector2 InputData(){

        return new Vector2(inputVector.x, inputVector.z);

    }

}

public enum JoystickType{

    X, Y, BOTH

}