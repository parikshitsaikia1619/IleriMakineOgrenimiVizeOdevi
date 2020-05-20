using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite hSprite;
    private Sprite dSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = hSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = dSprite;
    }

    private void Start()
    {
        dSprite = GetComponent<Image>().sprite;
    }
}
