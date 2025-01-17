using Cinemachine;
using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Triggerenterbosslevel : MonoBehaviour
{
    private bool ifenterbosslevel=false;
    private GameObject airwall;
    private Transform vcamera;
    private Light light;
    private UnityEngine.UI.Image shader;
    private bool ifmovecamera = false;
    private bool ifaim = false;
    private bool ifrotatecamera = false;
    private bool iflight=false;
    private bool ifshader=false;
    private float coloralpha=0;
    private bool ifplayermove = true;
    public bool ifbossmove = false;
    Vector3 movedir = new Vector3(17.14f, 10.7f, 15.37f);
    Vector3 rotatedir = new Vector3(8.892f, -90, 0);
    Vector3 zanshipos = new Vector3();
    private Transform player;
    
    static Triggerenterbosslevel mInstance;
    public static Triggerenterbosslevel Instance
    { 
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Triggerenterbosslevel>();
                if (mInstance == null)
                {

                }
            }
            return mInstance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance.ifbossmove = false;
        player=GameObject.Find("Player").GetComponent<Transform>();
        light=GameObject.Find("Spot Light").GetComponent<Light>();
        light.range = 0;
        shader=GameObject.Find("Image").GetComponent<UnityEngine.UI.Image>();
        shader.color = new UnityEngine.Color((0/255f), (0/255f), (0 / 255f), (0/255f));
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
                vcamera.eulerAngles += rotatedir / 3000;
            }
            else
            {
                vcamera.eulerAngles = new Vector3(8.892f, -90, 0);
                ifrotatecamera = false;
            }
        }

        if (iflight&&light.range<150)
        {
            light.range += Time.deltaTime*20;
        }
        else if (iflight&&light.range >= 150)
        {
            light.range = 150;
            iflight = false;
        }

        if (shader.color.a < (100/255f)&&ifshader)
        {
            coloralpha += Time.deltaTime * 30;
            shader.color = new UnityEngine.Color(((coloralpha/100*255) / 255f), ((coloralpha / 100 * 255) / 255f), ((coloralpha / 100 * 255) / 255f), (coloralpha/ 255f));
        }
        else if(ifshader&&shader.color.a>=(100/255f))
        {
            shader.color = new UnityEngine.Color((255 / 255f), (255 / 255f), (255 / 255f), (100 / 255f));
            ifshader=false;
        }

        if (!ifplayermove)
        {
            player.gameObject.GetComponent<PlayerController>().enabled = false;
            player.transform.position = zanshipos;
            if (!ifaim && !iflight && !ifshader && !ifmovecamera && !ifrotatecamera)
            {
                Invoke("backplayermove", 1.5f);

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
            iflight = true;
            ifshader = true;
            zanshipos=player.transform.position;
            if (ifplayermove)
            {
                Invoke("stopplayermove", 0.5f);
            }
            if(GameObject.Find("Image (2)") != null)
            {
                GameObject.Find("Image (2)").SetActive(false);
            }
            if(GameObject.Find("Point Light") != null)
            {
                GameObject.Find("Point Light").SetActive(false);
            }
            if (GameObject.Find("Point Light (1)") != null)
            {
                GameObject.Find("Point Light (1)").SetActive(false);
            }
            if (GameObject.Find("Point Light (2)") != null)
            {
                GameObject.Find("Point Light (2)").SetActive(false);
            }
            GameObject.Find("trigger").GetComponent<BoxCollider>().enabled = false;
        }
    }

    void stopplayermove()
    {
        ifplayermove=false;
        CancelInvoke("stopplayermove");
        
    }
    void backplayermove()
    {
        player.gameObject.GetComponent<PlayerController>().enabled = true;
        ifplayermove = true;
        Instance.ifbossmove = true;
        //Invoke("killzaotai", 1);
        CancelInvoke("backplayermove");
    }

    void killzaotai()
    {
        GameObject.Find("zaotaizhuozi (1)").SetActive(false);
    }
}
