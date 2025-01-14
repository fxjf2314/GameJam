using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownDuration : MonoBehaviour
{
    public float cooldownDuration;
    public bool isCoolingDown = false;
    public IEnumerator Cooldown(float cooldownDuration)
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isCoolingDown = false;
    }
}
