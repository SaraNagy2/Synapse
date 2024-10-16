using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    Animator Anim;
    Sprite CardBGSprite;
    public Sprite CardSprite;
    void Start()
    {
        Anim = GetComponent<Animator>();
        CardBGSprite = GetComponent<Image>().sprite;
    }

    public void ChangeSprite() 
    {
        GetComponent<Image>().sprite = CardSprite;
        Anim.SetBool("Flip", true);
    }
    public void ReturnSprite()
    {
        GetComponent<Image>().sprite = CardBGSprite;
    }
    public void Return()
    {
        Anim.SetBool("Flip", false);
        Anim.SetBool("Return", false);

    }
    public void FlipBack()
    {
        Anim.SetBool("Return", true);
    }
    public void Hide()
    {
        Anim.SetBool("Hide", true);
    }
    public void OnClickCard()
    {
        if (Anim.GetBool("Flip")) return;

        CardManager.Instance.PlayFlipSound();
        Anim.SetBool("Flip",true);
    }
    public void CheckCards()
    {
        CardManager.Instance.CheckCards(this);
    }
}
