using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCG : MonoBehaviour
{
    public float UpDuringTime;
    public float DownDuringTime;
    public CinemachineVirtualCamera virtualCamera;
    public Transform viewPoint;
    public Transform rotationPoint;
    public Transform Knife;
    Transform player;
    Coroutine knifeCoroutine;
    Coroutine playerCoroutine;
    Coroutine playerStandCoroutine;

    private void Start()
    {
        virtualCamera.transform.position = viewPoint.position;
        virtualCamera.transform.rotation = viewPoint.rotation;
        player = GameObject.Find("Player").transform;
        PlayerController.Instance.playerCanMove = false;
        Invoke("MyStartCoroutine", 1);
    }

    void MyStartCoroutine()
    {
        StartCoroutine(KnifeRotate(Vector3.right,5, UpDuringTime));
    }

    IEnumerator KnifeRotate(Vector3 aixs ,float rotEuler, float duringTime)
    {
        float time = 0;
        while (time < duringTime)
        {
            
            Knife.RotateAround(rotationPoint.position, aixs, rotEuler/duringTime * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        if(knifeCoroutine == null)
        {
            knifeCoroutine = StartCoroutine(KnifeRotate(Vector3.left, 35, DownDuringTime));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(PlayerHash(player.position, new Vector3(-230.7f, 130.5f, -585.7f), player.rotation.eulerAngles, new Vector3(90, -90,0)));
        }
    }
    
    IEnumerator PlayerHash(Vector3 originPos,Vector3 finalPos, Vector3 originRot, Vector3 finalRot)
    {
        float time = 0;
        //ÉÏÉý½×¶Î
        while (time < 0.5)
        {
            player.transform.position = Vector3.Lerp(originPos, finalPos, (float)(time / 0.5));
            player.rotation = Quaternion.Lerp(Quaternion.Euler(originRot), Quaternion.Euler(finalRot), (float)(time / 0.5));
            time += Time.deltaTime;
            yield return null;
        }
        //ÏÂ½µ½×¶Î
        if(playerCoroutine == null)
        {
            playerCoroutine = StartCoroutine(PlayerHash(player.position, new Vector3(-247.6f, 129.4506f, -585.7f), player.rotation.eulerAngles, new Vector3(90, -90, 0)));
        }
        else
        {
            if (playerStandCoroutine == null)
            {
                yield return new WaitForSeconds(0.5f);
                playerStandCoroutine = StartCoroutine(PlayerHash(player.position, new Vector3(-239.6f, 122.1506f, -585.7f), player.rotation.eulerAngles, new Vector3(0, -90, 0)));
            }
            else
            {
                CameraFollow.instance.MyStartCoroutine();
            }
        }
    }

}
