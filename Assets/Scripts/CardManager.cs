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
    public bool bEndGame = true;
    AudioSource audio;
    [SerializeField] AudioClip FlipSound;
    [SerializeField] AudioClip MismatchingSound;
    [SerializeField] AudioClip MatchingSound;
    [SerializeField] AudioClip WinSound;
    [SerializeField] AudioClip GameOverSound;
    GridLayoutGroup gridLayoutGroup;
    [SerializeField] GameObject CardPrefab;
    public int columns = 0;
    public int rows = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        AdjustGridCellSize();
        loadSprites();
        bEndGame = false;
    }
    public void InstantiateCards(int _columns, int _rows)
    {
        columns = _columns;
        rows = _rows;
        for (int i = 0; i < columns * rows; i++)
        {
            GameObject newObject = Instantiate(CardPrefab);
            newObject.transform.SetParent(this.transform);
        }
    }
    private void OnDisable()
    {
        CardsUseCount.Clear();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        bEndGame = true;
    }
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void loadSprites() 
    {
        MaxCount = (columns*rows) / 2;

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
                audio.PlayOneShot(WinSound);
                Reset();
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
    private void Reset()
    {
        MainMenu.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
        Count = 0;
    }
    void AdjustGridCellSize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        // Calculate the new cell size based on the container size
        float cellWidth = (width / columns) - (gridLayoutGroup.spacing.x * (columns - 1) / columns);
        float cellHeight = (height / rows) - (gridLayoutGroup.spacing.y * (rows - 1) / rows);

        // Set the new cell size
        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
    }
    
    
}