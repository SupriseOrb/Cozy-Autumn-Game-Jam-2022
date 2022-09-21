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
    private bool _isTranslateAvailable = true;
    private bool _isRuneAvailable = true;
    [SerializeField] private float _abilityTranslateCooldown = 1f;
    [SerializeField] private float _abilityRuneCooldown = 3f;
    [SerializeField] private GameObject _vfx;

    public void ActivateAbility()
    {
        Translate();
        InteractRune();
    }

    private void Translate()
    {
        if (_isTranslateAvailable == false)
        {
            Debug.Log("Translate Ability on cooldown!");
            return;
        }
        else
        {
            _originalPos = PlayerScript.Instance.transform.position;
            _offset = PlayerScript.Instance.getPlayerDirection();
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
                    Debug.Log("No characters found!");
                }
                else
                {
                    //Spirit translator vfx
                    Instantiate(_vfx, PlayerScript.Instance.transform);            
                    //Spirit translator sfx
                    AkSoundEngine.PostEvent("Play_Translator", this.gameObject);
                    StartCoroutine(StartTranslateCooldown());
                }
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
        if (_isRuneAvailable == false)
        {
            Debug.Log("Rune Ability on cooldown!");
            return;
        }
        else
        {
            _originalPos = PlayerScript.Instance.transform.position;
            Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos + _offset, _runeRadius);
            int runeCount = 0;
            if (allColliders != null)
            {
                foreach (Collider2D c in allColliders)
                {
                    if(c.TryGetComponent(out ItemTagScript interactable) && c.GetComponent<ItemTagScript>().IsRune())
                    {
                        if(c.TryGetComponent(out RuneScript floatingRune))
                        {
                            c.GetComponent<RuneScript>().TranslateRune();
                        }
                        //Interacts with inky bool variable, sets it to true to start dialogue? Possibly needs to interact
                        //with c.gameobject to get the id of the item to determine the bool variable? Unsure.
                    }
                    else {}
                }
                if (runeCount == 0)
                {
                    Debug.Log("No runes found!");
                }
                else
                {
                    // Kristen TODO: spirit translator sfx
                    StartCoroutine(StartRuneCooldown());
                }
            }
        }
    }

    IEnumerator StartTranslateCooldown()
    {
        _isTranslateAvailable = false;
        yield return new WaitForSeconds (_abilityTranslateCooldown);
        _isTranslateAvailable = true;
    }

    IEnumerator StartRuneCooldown()
    {
        _isRuneAvailable = false;
        yield return new WaitForSeconds (_abilityRuneCooldown);
        _isRuneAvailable = true;
    }
}
