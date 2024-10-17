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
    int ComboCount = 0;
    GridLayoutGroup gridLayoutGroup;
    [SerializeField] GameObject CardPrefab;
    [System.NonSerialized] public int columns = 0;
    [System.NonSerialized] public int rows = 0;
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
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }
    private void OnEnable()
    {
        if (!GameManager.Instance.bEndGame)
        {
            AdjustGridCellSize();
            loadSprites();
            Debug.Log("OnEnableOnEnable");
        }
    }
    private void Start()
    {

        GameManager.Instance.LoadGame();
        if (!GameManager.Instance.bEndGame)
        {
            AdjustGridCellSize();
            loadPreviousSprites();
            Debug.Log("StartStartStartStartStartStart");
        }

        if (!GameManager.Instance.bEndGame) //continue game 
        {
            MainMenu.Instance.gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("StartStartStart");

        }
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
        GameManager.Instance.bEndGame = true;
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
            transform.GetChild(i).GetComponent<Card>().id = i;
        }
    }
    void HideSprite(Transform card)
    {
        card.GetComponent<Image>().enabled = false;

    }
    void loadPreviousSprites()
    {
        InstantiateCards(columns, rows);

        MaxCount = (columns * rows) / 2;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Card>().CardSprite = Resources.Load<Sprite>(folderPath+"/"+ GameManager.Instance.cardDataList[i].spriteName);
            transform.GetChild(i).GetComponent<Card>().id = i;
            if (GameManager.Instance.cardDataList[i].isMatched)
            {
                transform.GetChild(i).GetComponent<Card>().Hidden = true;
                HideSprite(transform.GetChild(i));
                Count++;
            }
            else if (GameManager.Instance.cardDataList[i].isFlipped)
            {
                transform.GetChild(i).GetComponent<Card>().OnClickCard();
            }

        }
        Count = Count / 2;
    }
    Sprite GetRandomSprite()
    {
        int randomIndex = Random.Range(0, CardsSprite.Count);
        Sprite CardSprite = CardsSprite[randomIndex];
        GameManager.Instance.SaveCard(CardSprite.name, false, false);

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
            ScoreManager.Instance.SetScore(10);
            if (ComboCount > 0)
            {
                ScoreManager.Instance.SetScore(5* ComboCount);
            }
            ComboCount++;
            card_1.Hide();
            card_2.Hide();
            Count++;
            SoundManager.Instance.PlayMatchingSound();
            if (MaxCount == Count)
            {
                SoundManager.Instance.PlayWinSound();
                Reset();
            }
        }
        else
        {
            ComboCount = 0;
            ScoreManager.Instance.SetScore(-2);
            SoundManager.Instance.PlayMismatchingSound();
            card_1.FlipBack();
            card_2.FlipBack();
        }
        card_1 = null;
        card_2 = null;
    }
    private void Reset()
    {
        MainMenu.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
        ComboCount = 0;
        Count = 0;
        SaveLoadManager.Instance.ClearSaveData();
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