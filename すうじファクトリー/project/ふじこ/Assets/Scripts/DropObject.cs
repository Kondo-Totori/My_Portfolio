using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    private Sprite nowSprite;

    void Start()
    {
        nowSprite = null;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        iconImage.color = Vector4.one * 0.6f;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        iconImage.sprite = nowSprite;
        iconImage.color = Vector4.one;
        GetComponent<arithmeticPanel>().SetNowColor();

    }
    public void OnDrop(PointerEventData pointerEventData)
    {
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        nowSprite = droppedImage.sprite;
        iconImage.color = Vector4.one;
        GetComponent<arithmeticPanel>().SetPanel(droppedImage.gameObject.GetComponent<arithmeticPanel>().GetPanelColor(),
            droppedImage.gameObject.GetComponent<arithmeticPanel>().GetArithType(),
            droppedImage.gameObject.GetComponent<arithmeticPanel>().GetArithNumber());
        droppedImage.gameObject.GetComponent<arithmeticPanel>().SetRandomNumber();
    }
}