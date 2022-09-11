using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    static private CharacterScript _instance;
    static public CharacterScript Instance { get { return _instance;}}

    //How far away the two shops are from each other in the game screen
    [SerializeField] private Vector3 teleportDistance;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportPlayer()
    {
        GetComponent<Transform>().position += teleportDistance;
    }
}
