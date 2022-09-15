using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilitySpiritTranslator : MonoBehaviour, IAbility
{
    [SerializeField] private float _translateRadius;
    [SerializeField] private float _runeRadius;
    private Vector3 _originalPos;
    private Vector3 _offset;

    public void ActivateAbility()
    {
        Translate();
        InteractRune();
    }

    private void Translate()
    {
        _originalPos = PlayerScript.Instance.transform.position;
        _offset = PlayerScript.Instance.transform.forward;
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos + _offset, _translateRadius);
        int charCount = 0;

        if (allColliders != null)
        {
            foreach (Collider2D c in allColliders)
            {
                if(c.TryGetComponent(out ItemTagScript character))
                {
                    //Only one character, set their bool variable to false
                    //c.gameObject.GetComponent<CharacterScriptableObject>().IsGibberish = false;
                    charCount++;   
                }
            }
            if (charCount == 0)
            {
                //No characters, so nothing to do
                Debug.Log("No characters around!");
            }
        }
    }

            //NOT NEEDED IF THERE WILL NEVER BE MORE THAN ONE CHARACTER: 
            // else if (characters.Count >= 2)
            // {
            //     //More than one character, set closest one's bool variable to false
            //     List<float> characterDistance = null;
            //     int smallestIndex = 0;
            //     float smallestDist = 0f;
            //     foreach (GameObject g in characters)
            //     {
            //         float dist = Vector3.Distance(PlayerScript.Instance.transform.position, g.transform.position);
            //         characterDistance.Add(dist);
            //     }
            //     smallestDist = characterDistance.Min();
            //     smallestIndex = characterDistance.IndexOf(smallestDist);
            //     //characters[smallestIndex].GetComponent<CharacterScriptableObject>().IsGibberish = false;   
            // }

    private void InteractRune()
    {
        _originalPos = PlayerScript.Instance.transform.position;
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos + _offset, _runeRadius);
        if (allColliders != null)
        {
            foreach (Collider2D c in allColliders)
            {
                if(c.TryGetComponent(out ItemTagScript interactable))
                {
                    //Interacts with inky bool variable, sets it to true to start dialogue? Possibly needs to interact
                    //with c.gameobject to get the id of the item to determine the bool variable? Unsure.
                }
                else {}
            }
        }
    }
}
