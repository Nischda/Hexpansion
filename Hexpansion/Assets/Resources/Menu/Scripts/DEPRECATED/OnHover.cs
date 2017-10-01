using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	private Image _image;
	private Sprite _baseSprite;
	public Sprite HoverSprite;
	
	void Start () {
		_image = this.GetComponent<Image>();
		_baseSprite = _image.sprite;
	}

	public void OnPointerEnter (PointerEventData eventData){
		_image.sprite = HoverSprite;
	}
 
	public void OnPointerExit (PointerEventData eventData){
		_image.sprite = _baseSprite;;
	}
}
