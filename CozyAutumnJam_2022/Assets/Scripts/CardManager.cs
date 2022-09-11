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
#endregion

#region Active Ability Variables 
    [SerializeField] private List<GameObject> pCardList;
    [SerializeField] private int pCurrentCard;
    [SerializeField] private int pNextCard;
    [SerializeField] private int pPreviousCard;
    [SerializeField] private int numPassiveCards;
#endregion
    [SerializeField] private Image LeftCardImage;
    [SerializeField] private Image RightCardImage;
    [SerializeField] private bool activeDeckActive;

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
        if(activeDeckActive)
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
        Debug.Log(cycleDirection);
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
        if(activeDeckActive)
        {
            ViewHelper(ref aCardList, aCurrentCard);
        }
        else
        {
            ViewHelper(ref pCardList, pCurrentCard);
        }
    }

    public void ViewHelper(ref List<GameObject> currentDeck, int currentCurrent)
    {
        currentDeck[currentCurrent].GetComponent<Animator>().Play("Expand");
        //Show an info sprite
    }

    //Moves the array tracker by the value of cycleBy
    public void CycleCardsBy(int cycleBy)
    {
        if(activeDeckActive)
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
        if(activeDeckActive)
        {
            //Play the swap back animation for aCardList
            //Play the swap forward animation for pCardList
        }
        else
        {
            //Play the swap back animation for pCardList
            //Play the swap forward animation for aCardList
        }
        activeDeckActive = !activeDeckActive;
    }

    public void AddActiveCard(GameObject newCard)
    {
        aCardList.Add(newCard);
        if(activeDeckActive)
        {
            CycleCardsBy(0); 
        }
        else
        {
            activeDeckActive = true;
            CycleCardsBy(0);
            activeDeckActive = false;
        }
    }    

    public void AddPassiveCard(GameObject newCard)
    {
        pCardList.Add(newCard);
        if(!activeDeckActive)
        {
            CycleCardsBy(0);
        }
        else
        {
            activeDeckActive = false;
            CycleCardsBy(0);
            activeDeckActive = true;
        }
    }
}
