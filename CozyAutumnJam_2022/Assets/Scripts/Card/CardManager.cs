using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    static private CardManager _instance;
    static public CardManager Instance { get { return _instance;}}
#region Oracle Card Variables 
    [SerializeField] private List<GameObject> _oracleCardList;
    [SerializeField] private int _oracleCurrentCard;
    [SerializeField] private int _oracleNextCard;
    [SerializeField] private int _oraclePreviousCard;
    [SerializeField] private Animator _oracleDeckAnimator;
#endregion

#region Prop Variables 
    [SerializeField] private List<GameObject> _propCardList;
    [SerializeField] private int _propCurrentCard;
    [SerializeField] private int _propNextCard;
    [SerializeField] private int _propPreviousCard;
    [SerializeField] private int _propnumPassiveCards;
    [SerializeField] private Animator _propDeckAnimator;
#endregion

    [SerializeField] private Image _leftCardImage;
    [SerializeField] private Image _rightCardImage;
    [SerializeField] private bool _isOracleDeckActive;
    private bool _isCardExpanded;

    [SerializeField] private GameObject _hintHolder;
    [SerializeField] private TextMeshProUGUI _hintText;
    private int _newCardOldInt;
    private bool _gotNewCard;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }

    private void Start() 
    {
        CycleCardsBy(0);    
    }

    //Uses animations to cycle cards in UI
    public void CycleCards(bool direction)
    {
        //Plays sound for cycling through cards
        AkSoundEngine.PostEvent("Play_CardShuffle", this.gameObject);
        if(_gotNewCard)
        {
            CloseCard();
        }
        _isCardExpanded = false;
        if(_isOracleDeckActive)
        {
            CycleHelper(ref _oracleCardList, ref _oracleCurrentCard, ref _oraclePreviousCard, ref _oracleNextCard, direction);
        }
        else
        {
            CycleHelper(ref _propCardList, ref _propCurrentCard, ref _propPreviousCard, ref _propNextCard, direction);
        }
    }

    public void CycleHelper
    (
        ref List<GameObject> currentDeck, 
        ref int currentCurrent, 
        ref int currentPrevious, 
        ref int currentNext, 
        bool cycleDirection
    )
    {
        if(cycleDirection)
        {
            currentDeck[currentCurrent].GetComponent<Animator>().Play("MidToLeft");
            currentDeck[currentNext].transform.SetAsLastSibling();
            currentDeck[currentNext].GetComponent<Animator>().Play("RightToMid");
            Debug.Log(currentDeck[currentPrevious]);
            _leftCardImage.sprite = currentDeck[currentPrevious].GetComponent<CardArtImplementation>().Icon;
            CycleCardsBy(1);
            currentDeck[currentNext].GetComponent<Animator>().Play("ToRight");
        }
        else
        {
            currentDeck[currentCurrent].GetComponent<Animator>().Play("MidToRight");
            currentDeck[currentPrevious].transform.SetAsLastSibling();
            currentDeck[currentPrevious].GetComponent<Animator>().Play("LeftToMid");
            _rightCardImage.sprite = currentDeck[currentNext].GetComponent<CardArtImplementation>().Icon;
            CycleCardsBy(-1);
            currentDeck[currentPrevious].GetComponent<Animator>().Play("ToLeft");
        }
    } 

    public void EditCardSize()
    {
        if(!_isCardExpanded)
        {
            OpenCard();
        }
        else //Card is not expanded
        {
            CloseCard();
        }
    }

    public void ExpandHelper(ref List<GameObject> currentDeck, int currentCurrent)
    {
        _isCardExpanded = true;
        currentDeck[currentCurrent].GetComponent<Animator>().Play("Expand");
        //Show an info sprite
    }

    private void OpenCard()
    {
        //Plays sound for showing card info
        AkSoundEngine.PostEvent("Play_CardPull", this.gameObject);
        if(_isOracleDeckActive)
        {
            ExpandHelper(ref _oracleCardList, _oracleCurrentCard);
        }
        else
        {
            ExpandHelper(ref _propCardList, _propCurrentCard);
        }
    }

    private void CloseCard()
    {
        if(_isCardExpanded)
        {
            _isCardExpanded = false;
            if(_isOracleDeckActive)
            {
                CloseHelper(ref _oracleCardList, _oracleCurrentCard);
            }
            else
            {
                CloseHelper(ref _propCardList, _propCurrentCard);
            }
        }
        if(_gotNewCard)
        {
            _oracleCurrentCard = _newCardOldInt;
            _gotNewCard = false;
        }
    }

    private void CloseHelper(ref List<GameObject> currentDeck, int currentCurrent)
    {
        currentDeck[currentCurrent].GetComponent<Animator>().Play("FrontShrink");
    }

    //Moves the array tracker by the value of cycleBy
    public void CycleCardsBy(int cycleBy)
    {
        _isCardExpanded = false;
        if(_isOracleDeckActive)
        {
            CycleByHelper(ref _oracleCardList, ref _oracleCurrentCard, ref _oraclePreviousCard, ref _oracleNextCard, cycleBy);
        }
        else
        {
            CycleByHelper(ref _propCardList, ref _propCurrentCard, ref _propPreviousCard, ref _propNextCard, cycleBy);
        }
    }

    public void CycleByHelper
    (
        ref List<GameObject> currentDeck, 
        ref int currentCurrent, 
        ref int currentPrevious, 
        ref int currentNext, 
        int cycleAmount
    )
    {
        //Reduces cycleBy to be within the bounds of cardList
        while(cycleAmount >= currentDeck.Count)
        {
            cycleAmount -= currentDeck.Count;
        }
        while(cycleAmount <= -currentDeck.Count)
        {
            cycleAmount += currentDeck.Count;
        }
        currentCurrent += cycleAmount;
        if(currentCurrent >= currentDeck.Count)
        {
            currentCurrent -= currentDeck.Count;
        }
        if(currentCurrent < 0)
        {
            currentCurrent = currentDeck.Count + cycleAmount;
        }
        currentPrevious = currentCurrent - 1;
        if(currentPrevious < 0)
        {
            currentPrevious = currentDeck.Count - 1;
        }
        currentNext = currentCurrent + 1;
        if(currentNext >= currentDeck.Count)
        {
            currentNext = 0;
        }
    }

    public void SwapDecks()
    {
        _isCardExpanded = false;
        if(_isOracleDeckActive)
        {
            if(_propCardList.Count <= 0)
            {
                return;
            }
            _oracleCardList[_oracleCurrentCard].GetComponent<Animator>().Play("Normal");
            _propDeckAnimator.gameObject.SetActive(true);
            //Play the swap back animation for aCardList
            //Play the swap forward animation for pCardList
            _oracleDeckAnimator.Play("DeckToBack");
            _propDeckAnimator.Play("DeckToFront");
        }
        else
        {
            _propCardList[_propCurrentCard].GetComponent<Animator>().Play("Normal");
            _oracleDeckAnimator.gameObject.SetActive(true);
            //Play the swap back animation for pCardList
            //Play the swap forward animation for aCardList
            _oracleDeckAnimator.Play("DeckToFront");
            _propDeckAnimator.Play("DeckToBack");
        }
        _isOracleDeckActive = !_isOracleDeckActive;

        //Plays sound for swapping decks
        AkSoundEngine.PostEvent("Play_DeckSwap", this.gameObject);
    }

    public void AddOracleCard(GameObject newCard)
    {
        _oracleCardList.Add(newCard);
        if(_isOracleDeckActive)
        {
            CycleCardsBy(0); 
        }
        else
        {
            _isOracleDeckActive = true;
            CycleCardsBy(0);
            _isOracleDeckActive = false;
        }
    }    

    public void GetOracleCard(string cardName)
    {
        for(int i = 0; i < _oracleCardList.Count; i++)
        {
            if(_oracleCardList[i].name == cardName)
            {
                _oracleCardList[i].GetComponent<CardArtImplementation>().ShowCardInfo();
                _newCardOldInt = _oracleCurrentCard;
                _gotNewCard = true;
                _oracleCurrentCard = i;
                OpenCard();
            }
        }
    }

    public void AddPropCard(GameObject newCard)
    {
        newCard.SetActive(true);
        _propCardList.Add(newCard);
        if(!_isOracleDeckActive)
        {
            CycleCardsBy(0);
        }
        else
        {
            _isOracleDeckActive = false;
            CycleCardsBy(0);
            _isOracleDeckActive = true;
        }
    }

    public bool GetPropCard(string cardName)
    {
        foreach(GameObject card in _propCardList)
        {
            if(card.name == cardName)
            {
                return true;
            }
        }
        return false;
    }

    public void ActivateAbilityCard()
    {
        if(_isOracleDeckActive)
        {
            IAbility cardAbility = _oracleCardList[_oracleCurrentCard].GetComponent<IAbility>();
            if(cardAbility != null)
            {
                cardAbility.ActivateAbility();
            }
            else
            {
                Debug.Log("ABILITY SCRIPT NOT FOUND. YOU'RE A DINGUS");
            }
        }
    }
}
