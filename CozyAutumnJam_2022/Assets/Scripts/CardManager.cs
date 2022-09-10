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

    public void CycleCardsBy(int cycleBy)
    {
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
