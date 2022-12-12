using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool touchDown;

	public UnityEvent onHold;
	

	void Update()
	{
		if (touchDown && onHold != null)
			onHold.Invoke();
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		touchDown = true;
		Debug.Log("Finger down.");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		touchDown = false;
		Debug.Log("Finger up.");
	}

}
