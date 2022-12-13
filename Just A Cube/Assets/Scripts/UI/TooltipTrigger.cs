using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		//FindObjectOfType<Options>().Invoke("ShowTooltip", 0.5f);
		Debug.Log("Tooltip shown.");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//FindObjectOfType<Options>().HideTooltip();
		Debug.Log("Tooltip hid.");
	}
}
