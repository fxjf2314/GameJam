using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Partner : AttackableObj
{
    //protected int speed;
    protected Vector3 dir;
    

    protected abstract void Attack();

}
