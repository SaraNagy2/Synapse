using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    List<Sprite> CardsSprite = new List<Sprite>();
    string folderPath = "Cards"; // Folder inside Resources
    private Dictionary<Sprite, int> CardsUseCount = new Dictionary<Sprite, int>();
    Card card_1;
    Card card_2;
    int Count;
    int MaxCount;
    public bool bEndGame;
    AudioSource audio;
    [SerializeField] AudioClip FlipSound;
    [SerializeField] AudioClip MismatchingSound;
    [SerializeField] AudioClip MatchingSound;
    [SerializeField] AudioClip WinSound;
    [SerializeField] AudioClip GameOverSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        MaxCount = transform.childCount / 2;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(folderPath);
        for (int i = 0; i < MaxCount; i++)
        {
            CardsSprite.Add(loadedSprites[i]);
        }

        foreach (Sprite sprite in CardsSprite)
        {
            CardsUseCount[sprite] = 0;
        }
  
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Card>().CardSprite = GetRandomSprite();
        }

    }
    Sprite GetRandomSprite()
    {
        int randomIndex = Random.Range(0, CardsSprite.Count);
        Sprite CardSprite = CardsSprite[randomIndex];
        
        CardsUseCount[CardSprite]++;
        if (CardsUseCount[CardSprite] >= 2)
        {
            CardsSprite.RemoveAt(randomIndex);
        }
        return CardSprite;
    }
    public void CheckCards(Card card) 
    {
        if (card_1 == null)
        {
            card_1 = card;
            return;
        }
        card_2 = card;
        if (card_1.CardSprite == card_2.CardSprite)
        {
            card_1.Hide();
            card_2.Hide();
            Count++;
            audio.PlayOneShot(MatchingSound);
            if (MaxCount == Count)
            {
                bEndGame = true;
                audio.PlayOneShot(WinSound);
            }
        }
        else 
        {
            audio.PlayOneShot(MismatchingSound);
            card_1.FlipBack();
            card_2.FlipBack();
        }
        card_1 = null;
        card_2 = null;
    }
    public void PlayFlipSound() 
    {
        audio.PlayOneShot(FlipSound);
    }

}
