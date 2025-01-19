using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class Bubble : MonoBehaviour
{
    public bool leavescene = false;
    Transform vicamera;
    GameObject bubblelight;
    GameObject boss;
    Vector3 aimpos;
    // Start is called before the first frame update
    void Start()
    {
        aimpos = new Vector3(387.0562f, 47.46993f, 15.37f);
        boss = GameObject.Find("boss");
        bubblelight = GameObject.Find("bubblelight");
        bubblelight.SetActive(false);
        leavescene = false;
        vicamera=GameObject.Find("Virtual Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null)
        {
            if (boss.GetComponent<MonsterController>().hp <= 0)
            {
                Invoke("Leavescene", 5);
                Invoke("openlight", 9);
                Invoke("Destroyall", 20);
            }
        }

        if (leavescene)
        {
            vicamera.position += 0.01f*(new Vector3(1, 0.1f, 0));
        }

        if (vicamera.position.x >= 387.0562f)
        {
            leavescene = false;
            vicamera.position = aimpos;
            Invoke("bubbleburst", 3);
        }
    }

    void bubbleburst()
    {
        GameObject.Find("bubble").GetComponent<MeshExploder>().Explode();
        GameObject.Find("bubble").SetActive(false);
    }

    void Leavescene()
    {
        leavescene=true;
        GameObject.Find("Image").SetActive(false);
    }

    void Destroyall()
    {
        Destroy(GameObject.Find("Environment"));
        Destroy(GameObject.Find("Map"));
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("MonsterController"));
        //Destroy(GameObject.Find("UI"));
        //Destroy(GameObject.Find("UIManager"));
        

    }

    void openlight()
    {
        bubblelight.SetActive(true);
    }
}
