using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    Animator Anim;
    Sprite CardBGSprite;
    public Sprite CardSprite;
    [System.NonSerialized] public int id;
    [System.NonSerialized] public bool Hidden;
    private void Awake()
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
        GameManager.Instance.cardDataList[id].isFlipped = false;
    }
    public void Hide()
    {
        Anim.SetBool("Hide", true);
        Hidden = true;
        GameManager.Instance.cardDataList[id].isMatched = true;
        Debug.Log("id: "+id);
    }
    public void OnClickCard()
    {
        if (Hidden || Anim.GetBool("Flip")) return;

        SoundManager.Instance.PlayFlipSound();
        Anim.SetBool("Flip",true);
        GameManager.Instance.cardDataList[id].isFlipped = true;
    }
    public void CheckCards()
    {
        CardManager.Instance.CheckCards(this);
    }
}
