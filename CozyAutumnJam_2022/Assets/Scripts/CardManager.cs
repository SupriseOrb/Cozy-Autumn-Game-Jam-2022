using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardList;
    [SerializeField] private Image leftCardImage;
    [SerializeField] private Image rightCardImage;
    [SerializeField] private int currentCard;
    [SerializeField] private int nextCard;
    [SerializeField] private int previousCard;
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
        Debug.Log(direction);
        if(direction)
        {
            cardList[currentCard].GetComponent<Animator>().Play("MidToLeft");
            cardList[nextCard].transform.SetAsLastSibling();
            cardList[nextCard].GetComponent<Animator>().Play("RightToMid");
            leftCardImage.sprite = cardList[previousCard].GetComponent<Image>().sprite;
            CycleCardsBy(1);
            cardList[nextCard].GetComponent<Animator>().Play("ToRight");
        }
        else
        {
            cardList[currentCard].GetComponent<Animator>().Play("MidToRight");
            cardList[previousCard].transform.SetAsLastSibling();
            cardList[previousCard].GetComponent<Animator>().Play("LeftToMid");
            rightCardImage.sprite = cardList[nextCard].GetComponent<Image>().sprite;
            CycleCardsBy(-1);
            cardList[previousCard].GetComponent<Animator>().Play("ToLeft");
        }
    }

    public void ViewCard()
    {
        cardList[currentCard].GetComponent<Animator>().Play("Expand");
        //Show an info sprite
    }

    //Moves the array tracker by the value of cycleBy
    public void CycleCardsBy(int cycleBy)
    {
        //Reduces cycleBy to be within the bounds of cardList
        while(cycleBy >= cardList.Count)
        {
            cycleBy -= cardList.Count;
        }
        while(cycleBy <= -cardList.Count)
        {
            cycleBy += cardList.Count;
        }
        currentCard += cycleBy;
        if(currentCard >= cardList.Count)
        {
            currentCard -= cardList.Count;
        }
        if(currentCard < 0)
        {
            currentCard = cardList.Count + cycleBy;
        }
        previousCard = currentCard - 1;
        if(previousCard < 0)
        {
            previousCard = cardList.Count - 1;
        }
        nextCard = currentCard + 1;
        if(nextCard >= cardList.Count)
        {
            nextCard = 0;
        }
    }

    public void AddCard(GameObject newCard)
    {
        cardList.Add(newCard);
        CycleCardsBy(0); 
    }    
}
