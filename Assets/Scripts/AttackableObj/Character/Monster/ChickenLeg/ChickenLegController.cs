using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLegController : MonsterController
{
    private void Start()
    {
        base.Start();
        characterAnimator = transform.Find("jituiA").GetComponent<Animator>();
    }
}
