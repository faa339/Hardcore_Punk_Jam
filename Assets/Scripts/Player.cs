using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int attackNumber; //This works like a scene count as well as difficulty mod
    public bool gameOver;
    public bool blocking;
    public Sprite[] attackFrames;
    public GameObject[] enemies;
    const float blockTime = 0.8f;
    float blockTimeLeft;
    AudioClip attackSound;
    AudioClip hitSound;
    AudioClip deathSound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (blocking) {
            blockTimeLeft -= Time.deltaTime;
            Debug.Log("Blocktime: " + blockTimeLeft);
            if (blockTimeLeft <= 0)
                blocking = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && blocking==false && gameOver==false) {
            blocking = true;
            blockTimeLeft = blockTime;
        }

        if (transform.position.z < 4f)
        {
            if (enemies[0].GetComponent<Enemy>().dead)
            {
                Vector3 moveTarget = new Vector3(transform.position.x, transform.position.y, 4.0f);
                transform.position = Vector3.MoveTowards(transform.position, moveTarget, 20f * Time.deltaTime);
            }
        }
        else if ( transform.position.z < 60f)
        {
            if (enemies[1].GetComponent<Enemy>().dead && enemies[2].GetComponent<Enemy>().dead)
            {
                Vector3 moveTarget = new Vector3(transform.position.x, transform.position.y, 60.0f);
                transform.position = Vector3.MoveTowards(transform.position, moveTarget, 20f * Time.deltaTime);

                //vector move again
            }
        }
        else if (transform.position.z > 5 && transform.position.z < 125f)
        {
            if (enemies[3].GetComponent<Enemy>().dead && enemies[4].GetComponent<Enemy>().dead && enemies[5].GetComponent<Enemy>().dead)
            {
                Vector3 moveTarget = new Vector3(transform.position.x, transform.position.y, 125f);
                transform.position = Vector3.MoveTowards(transform.position, moveTarget, 20f * Time.deltaTime);
                //vector move again
            }
        }
        else {
            if (enemies[6].GetComponent<Boss>().dead) { 
                //games done
            }
        }
        Debug.Log(health);
    }
}
