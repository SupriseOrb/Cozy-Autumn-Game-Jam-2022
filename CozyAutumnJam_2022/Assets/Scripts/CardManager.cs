using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
#region Active Ability Variables 
    [SerializeField] private List<GameObject> aCardList;
    [SerializeField] private int aCurrentCard;
    [SerializeField] private int aNextCard;
    [SerializeField] private int aPreviousCard;
    [SerializeField] private Animator aDeckAnimator;
#endregion

#region Passive Ability Variables 
    [SerializeField] private List<GameObject> pCardList;
    [SerializeField] private int pCurrentCard;
    [SerializeField] private int pNextCard;
    [SerializeField] private int pPreviousCard;
    [SerializeField] private int numPassiveCards;
    [SerializeField] private Animator pDeckAnimator;
#endregion
    [SerializeField] private Image LeftCardImage;
    [SerializeField] private Image RightCardImage;
    [SerializeField] private bool abilityDeckActive;
    private bool isCardExpanded;
    private bool isCardFront;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Uses animations to cycle cards in UI
    public void CycleCards(bool direction)
    {
        //Plays sound for cycling through cards
        AkSoundEngine.PostEvent("Play_CardShuffle", this.gameObject);

        isCardExpanded = false;
        if(abilityDeckActive)
        {
            CycleHelper(ref aCardList, ref aCurrentCard, ref aPreviousCard, ref aNextCard, direction);
        }
        else
        {
            CycleHelper(ref pCardList, ref pCurrentCard, ref pPreviousCard, ref pNextCard, direction);
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
            LeftCardImage.sprite = currentDeck[currentPrevious].GetComponent<Image>().sprite;
            CycleCardsBy(1);
            currentDeck[currentNext].GetComponent<Animator>().Play("ToRight");
        }
        else
        {
            currentDeck[currentCurrent].GetComponent<Animator>().Play("MidToRight");
            currentDeck[currentPrevious].transform.SetAsLastSibling();
            currentDeck[currentPrevious].GetComponent<Animator>().Play("LeftToMid");
            RightCardImage.sprite = currentDeck[currentNext].GetComponent<Image>().sprite;
            CycleCardsBy(-1);
            currentDeck[currentPrevious].GetComponent<Animator>().Play("ToLeft");
        }
    } 

    public void ViewCard()
    {
        if(!isCardExpanded)
        {
            //Plays sound for showing card info
            AkSoundEngine.PostEvent("Play_CardPull", this.gameObject);
            isCardFront = true;
            if(abilityDeckActive)
            {
                ExpandHelper(ref aCardList, aCurrentCard);
            }
            else
            {
                ExpandHelper(ref pCardList, pCurrentCard);
            }
        }
        else if(isCardExpanded)
        {
            if(abilityDeckActive)
            {
                //Plays sound for flipping a card over
                AkSoundEngine.PostEvent("Play_CardFlip", this.gameObject);
                FlipHelper(ref aCardList, aCurrentCard);
            }
        }
    }

    public void ExpandHelper(ref List<GameObject> currentDeck, int currentCurrent)
    {
        isCardExpanded = true;
        currentDeck[currentCurrent].GetComponent<Animator>().Play("Expand");
        //Show an info sprite
    }

    public void FlipHelper(ref List<GameObject> currentDeck, int currentCurrent)
    {
        if(isCardFront)
        {
            isCardFront = false;
            currentDeck[currentCurrent].GetComponent<Animator>().Play("FlipToBack");
        }
        else
        {
            isCardFront = true;
            currentDeck[currentCurrent].GetComponent<Animator>().Play("FlipToFront");
        }
    }

    public void CloseCard()
    {
        if(isCardExpanded)
        {
            isCardExpanded = false;
            if(abilityDeckActive)
            {
                CloseHelper(ref aCardList, aCurrentCard);
            }
            else
            {
                CloseHelper(ref pCardList, pCurrentCard);
            }
        }
    }

    public void CloseHelper(ref List<GameObject> currentDeck, int currentCurrent)
    {
        if(isCardFront)
        {
            currentDeck[currentCurrent].GetComponent<Animator>().Play("FrontShrink");
        }
        else
        {
            currentDeck[currentCurrent].GetComponent<Animator>().Play("BackShrink");
        }
    }

    //Moves the array tracker by the value of cycleBy
    public void CycleCardsBy(int cycleBy)
    {
        isCardExpanded = false;
        if(abilityDeckActive)
        {
            CycleByHelper(ref aCardList, ref aCurrentCard, ref aPreviousCard, ref aNextCard, cycleBy);
        }
        else
        {
            CycleByHelper(ref pCardList, ref pCurrentCard, ref pPreviousCard, ref pNextCard, cycleBy);
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
        //Plays sound for swapping decks
        AkSoundEngine.PostEvent("Play_DeckSwap", this.gameObject);

        isCardExpanded = false;
        if(abilityDeckActive)
        {
            aCardList[aCurrentCard].GetComponent<Animator>().Play("Normal");
            pDeckAnimator.gameObject.SetActive(true);
            //Play the swap back animation for aCardList
            //Play the swap forward animation for pCardList
            aDeckAnimator.Play("DeckToBack");
            pDeckAnimator.Play("DeckToFront");
        }
        else
        {
            pCardList[pCurrentCard].GetComponent<Animator>().Play("Normal");
            aDeckAnimator.gameObject.SetActive(true);
            //Play the swap back animation for pCardList
            //Play the swap forward animation for aCardList
            aDeckAnimator.Play("DeckToFront");
            pDeckAnimator.Play("DeckToBack");
        }
        abilityDeckActive = !abilityDeckActive;
    }

    public void AddActiveCard(GameObject newCard)
    {
        aCardList.Add(newCard);
        if(abilityDeckActive)
        {
            CycleCardsBy(0); 
        }
        else
        {
            abilityDeckActive = true;
            CycleCardsBy(0);
            abilityDeckActive = false;
        }
    }    

    public void AddPassiveCard(GameObject newCard)
    {
        pCardList.Add(newCard);
        if(!abilityDeckActive)
        {
            CycleCardsBy(0);
        }
        else
        {
            abilityDeckActive = false;
            CycleCardsBy(0);
            abilityDeckActive = true;
        }
    }

    public void ActivateAbilityCard()
    {
        if(abilityDeckActive)
        {
            IAbility cardAbility = aCardList[aCurrentCard].GetComponent<IAbility>();
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
