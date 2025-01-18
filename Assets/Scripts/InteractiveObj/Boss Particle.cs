using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "Player")
        {
            PlayerModel.Instance.hp -= 2;
        }
        if (PlayerModel.Instance.hp <= 0)
        {
            Invoke("Destroyplayer", 0.1f);
        }
    }

    void Destroyplayer()
    {
        Destroy(GameObject.Find("Player"));
    }
}
