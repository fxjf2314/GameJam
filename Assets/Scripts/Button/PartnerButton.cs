using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartnerButton : MonoBehaviour
{
    private static PartnerButton instance;
    public static PartnerButton MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PartnerButton>();
            }
            return instance;
        }
    }

    public Armor partner;
    [SerializeField]
    CharButton partnerSlot;
    Image partnerIcon;

    private void Awake()
    {
        partnerIcon = GetComponent<Image>();
        
    }

    public void UpdatePartnerIcon()
    {
        
        partner = partnerSlot.partnericon;
        if(partner != null)
        {
            partnerIcon.sprite = partner.MyIcon;
        }
        
    }
}
