using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
//public class UIEventListener : UnityEngine.EventSystems.EventTrigger{
public class UIEventListener : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    public Util.VoidDelegate onClick;
    public Util.VoidDelegate onPointerDown;
    public Util.VoidDelegate onPointerUp;
    public Util.VoidDelegate onDragPointerEnter;
    public Util.VoidDelegate onDragPointerExit;
    public Util.VoidDelegate onPointerEnter;
    public Util.VoidDelegate onPointerExit;
	public Util.VoidDelegate onLongPress;
	public Util.VoidDelegate onLongPressMoreThanOnce;
    public Util.DragDelegate onBeginDrag;
    public Util.DragDelegate onEndDrag;
    public Util.DragDelegate onDrag;

    private bool isDrag = false;
	private bool isPointerDown = false;
	private bool isLongPressEnd = true;
	private bool isLongPressMoreThanOnceEnd = true;
	private float longPressTimer = 0f;
	private float longPressTime = 1f;
	private float longPressInterval = 0.3f;
	private float longPressMoreThanOnceTimer = 0f;
    private float longPressAcceleration = 0f;
    private float longPressMinInterval = 0.1f;
    private float longPressCurInterval = 0f;
    
	void Update(){
		//如果我在长安的期间，触发了，ondrag，那么longpress timer就重新计时
		if(isPointerDown){
			longPressTimer += Time.deltaTime;
			longPressMoreThanOnceTimer += Time.deltaTime;
			if(!isLongPressEnd){
				if(longPressTimer >= longPressTime){
					OnLongPress();
					isLongPressEnd = true;
					longPressTimer = 0f;
				}
			}
			if(!isLongPressMoreThanOnceEnd){
                if(longPressMoreThanOnceTimer >= longPressCurInterval)
                {
					OnLongPressMoreThanOnce();
					longPressMoreThanOnceTimer = 0;
                    longPressCurInterval -= longPressAcceleration;
                    if(longPressCurInterval < longPressMinInterval)
                    {
                        longPressCurInterval = longPressMinInterval;
                    }
				}
			}
		}
	}

    static public UIEventListener Get(GameObject go)
	{
        UIEventListener listener = go.GetComponent<UIEventListener>();
        if (listener == null) listener = go.AddComponent<UIEventListener>();
		return listener;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
        if (onClick != null && !isDrag) onClick(gameObject, eventData);
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDown != null) onPointerDown(gameObject, eventData);
		isPointerDown = true;
		isLongPressEnd = false;
		isLongPressMoreThanOnceEnd = false;
        longPressCurInterval = longPressInterval;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUp != null) onPointerUp(gameObject, eventData);
		longPressTimer = 0f;
		longPressMoreThanOnceTimer = 0f;
		isPointerDown = false;
		isLongPressEnd = true;
		isLongPressMoreThanOnceEnd = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnter != null) onPointerEnter(gameObject, eventData);
        if (isDrag && (onDragPointerEnter != null)) onDragPointerEnter(gameObject, eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExit != null) onPointerExit(gameObject, eventData);
        if (isDrag && (onDragPointerExit != null)) onDragPointerExit(gameObject, eventData);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        if (onBeginDrag != null) onBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        if (onEndDrag != null) onEndDrag(eventData);
    }
    public void OnDrag(PointerEventData data)
    {
        if (onDrag != null) onDrag(data);
		isPointerDown = false;
    }

	public void OnLongPress()
	{
		if (onLongPress != null) onLongPress(gameObject);
	}

	public void OnLongPressMoreThanOnce(){
		if(onLongPressMoreThanOnce != null) onLongPressMoreThanOnce(gameObject);
	}

	public float LongPressDuration{
		get{return longPressTime;}
		set{longPressTime = value;}
	}

	public float LongPressInterval{
		get{
			return longPressInterval;
		}
		set{
			longPressInterval = value;
		}
	}

    public float LongPressAcceleration{
        get{
            return longPressAcceleration;
        }
        set{
            longPressAcceleration = value;
        }
    }

    public float LongPressMinInterval{
        get{
            return longPressMinInterval;
        }
        set{
            longPressMinInterval = value;
        }
    }

    public void AddValueChangeEventListener(Util.VoidDelegate Callback)
    {
        UnityEngine.UI.InputField inputField = gameObject.GetComponent<UnityEngine.UI.InputField>();
        if (inputField)
        {
            inputField.onValueChange.AddListener(delegate { Callback(gameObject); });
        }
    }
}
