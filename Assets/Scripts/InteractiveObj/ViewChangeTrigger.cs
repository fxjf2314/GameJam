using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewChangeTrigger : MonoBehaviour
{
    public float duringTime;
    CinemachineFramingTransposer framingTransposer;
    public Vector3 targetRot;
    public Vector3 targetOffset;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController.Instance.isMoveOnZ = true;
            PlayerController.Instance.transform.Rotate(Vector3.up, 90);
            PlayerController.Instance.ChangeMoveDir();
            framingTransposer = PlayerController.Instance.virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            PlayerController.Instance.playerCanMove = false;
            PlayerController.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(ViewChange(PlayerController.Instance.virtualCamera.transform.rotation.eulerAngles, targetRot, framingTransposer.m_TrackedObjectOffset, targetOffset));
        }

        IEnumerator ViewChange(Vector3 originRot, Vector3 finalRot, Vector3 originOffset, Vector3 finalOffset)
        {
            float offX, offY, offZ;
            float time = 0;
            while (time <= 1)
            {
                offX = Mathf.Lerp(originOffset.x, finalOffset.x, time); offY = Mathf.Lerp(originOffset.y, finalOffset.y, time); offZ = Mathf.Lerp(originOffset.z, finalOffset.z, time);
                PlayerController.Instance.virtualCamera.transform.rotation = Quaternion.Lerp(Quaternion.Euler(originRot), Quaternion.Euler(finalRot), time);
                framingTransposer.m_TrackedObjectOffset = new Vector3(offX,offY,offZ);
                time += Time.deltaTime/duringTime;
                yield return null;
            }
            PlayerController.Instance.playerCanMove = true;
        }
    }
}
