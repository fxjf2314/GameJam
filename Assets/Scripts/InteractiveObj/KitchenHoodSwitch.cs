using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void KitchenHoodSwitchDelegate();
public class KitchenHoodSwitch : MonoBehaviour
{
    public event KitchenHoodSwitchDelegate SwitchEvent;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            SwitchEvent.Invoke();
        }
    }
}
