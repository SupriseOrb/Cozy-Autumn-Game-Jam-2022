using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckAnimationScript : MonoBehaviour
{
    [SerializeField] GameObject cardPositionHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShiftDisplayPriority()
    {
        this.transform.SetAsLastSibling();
    }

    public void HideHolder()
    {
        cardPositionHolder.SetActive(false);
    }

    public void ShowHolder()
    {
        cardPositionHolder.SetActive(true);
    }
    
    public void HideDeck()
    {
        //this.gameObject.SetActive(false);
    }
}
