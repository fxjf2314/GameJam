using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool leavescene = false;
    Transform vicamera;
    GameObject bubblelight;
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
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
            }
        }

        if (leavescene)
        {
            vicamera.position += 0.01f*(new Vector3(1, 0.1f, 0));
        }
    }

    void Leavescene()
    {
        leavescene=true;
    }

    void Destroyall()
    {
        Destroy(GameObject.Find("Environment"));
        Destroy(GameObject.Find("Map"));
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("MonsterController"));
        Destroy(GameObject.Find("UI"));
    }

    void openlight()
    {
        bubblelight.SetActive(true);
    }
}
