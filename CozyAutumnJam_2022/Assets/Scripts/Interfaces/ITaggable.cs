using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaggable
{
    bool IsTrash();
    bool IsTrap();
    bool IsTrappable();
    bool IsRune();
    bool IsPushable();
    bool IsInteractable();
    bool IsAnimatronic();
    bool IsCharacter();
}
