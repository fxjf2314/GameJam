using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Apple",menuName ="Item/Food",order = 1 )]
public class Apple : Item, IUseable
{
    public void Use()
    {
        HealthBarController.Instance.AddHeart(2+1);
        Remove();
    }


}
