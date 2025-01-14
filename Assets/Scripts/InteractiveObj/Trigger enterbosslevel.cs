using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Triggerenterbosslevel : MonoBehaviour
{
    private bool ifenterbosslevel=false;
    private GameObject airwall;
    private Transform vcamera;
    private bool ifmovecamera = false;
    private bool ifaim = false;
    private bool ifrotatecamera = false;
    Vector3 movedir = new Vector3(17.14f, 10.7f, 15.37f);
    Vector3 rotatedir = new Vector3(8.892f, -90, 0);

    // Start is called before the first frame update
    void Start()
    {
        vcamera = GameObject.Find("Virtual Camera").GetComponent<Transform>();
        vcamera.eulerAngles = new Vector3(0, -90, 0);
        airwall = GameObject.Find("airwall");
        airwall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

            if (ifaim)
            {

                movedir = new Vector3(movedir.x - vcamera.position.x, movedir.y - vcamera.position.y, movedir.z - vcamera.position.z);
                rotatedir = new Vector3((rotatedir.x - vcamera.rotation.x), 0 , 0 );
                ifaim = false;
            }
        if (ifmovecamera)
        {
            if (vcamera.position.z < 15.37f)
            {
                vcamera.position += movedir / 3000;

            }
            else
            {
                vcamera.position = new Vector3(17.14f, 10.7f, 15.37f);
                ifmovecamera = false;
            }
        }
           

        if (ifrotatecamera)
        {
            if (vcamera.eulerAngles.x < 8.892f)
            {
                vcamera.eulerAngles += rotatedir/3000;
            }
            else
            {
                vcamera.eulerAngles = new Vector3(8.892f, -90, 0);
                ifrotatecamera = false;
            }
        }
           
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerController.Instance.jumpSpeed = 22;
            GameObject.Find("Virtual Camera").GetComponent<CameraFollow>().enabled = false;
            GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Follow = null;
            airwall.SetActive(true);
            ifmovecamera = true;
            ifaim = true;
            ifrotatecamera = true;
        }
    }
}
