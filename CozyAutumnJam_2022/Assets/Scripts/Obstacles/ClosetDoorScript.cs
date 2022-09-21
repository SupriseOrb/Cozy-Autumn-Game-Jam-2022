using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetDoorScript : MonoBehaviour, IInteractable
{
    private Vector3 _originalPos;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private BoolVariable _inHumanWorld;
    [SerializeField] private Vector3 _teleportDist;
    public void ActivateInteraction()
    {
        Instantiate(_vfx, PlayerScript.Instance.transform);
        Debug.Log("teleporting...");
        _originalPos = PlayerScript.Instance.transform.position;
         //Teleport sound
        AkSoundEngine.PostEvent("Play_Teleport", this.gameObject);
        PlayerScript.Instance.transform.position = (_inHumanWorld.Value)? (_originalPos + _teleportDist) : (_originalPos - _teleportDist);
        _inHumanWorld.Value = !_inHumanWorld.Value;
        if(_inHumanWorld.Value)
        {
            AkSoundEngine.SetState("Gameplay", "HumanStore");
        }
        else
        {
            AkSoundEngine.SetState("Gameplay", "SpiritStore");
        }
    }
}
