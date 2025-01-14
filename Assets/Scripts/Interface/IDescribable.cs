using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDescribable
{
    string GetType();
    
    string GetDescription();
    
    Sprite GetSprite();
}
