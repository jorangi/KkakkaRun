using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlideButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UIController uiCon;
    public CharacterBase character;
    private bool Slide;
    private void Update() 
    {
        if(Slide && Input.GetMouseButton(0)) uiCon.SlideOn();
        else uiCon.SlideOff();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Slide = true;
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Slide = false;
    }
}
