using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public int health;
    public int attackNumber; //This works like a scene count as well as difficulty mod
    public bool gameOver;
    public bool blocking;
    public bool gameStart;
    public GameObject[] enemies;
    public Image[] attackFrames;
    public TextMeshProUGUI healthText;
    public bool hasAttacked;
    const float blockTime = 0.8f;
    const float attackTime = 0.5f;
    float attackTimeLeft;
    float speed = 10f;
    float blockTimeLeft;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        gameStart = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + health;
        if (gameStart == false || gameOver)
            return;

        if (blocking) {
            blockTimeLeft -= Time.deltaTime;
            if (blockTimeLeft <= 0) {
                blocking = false;
                attackFrames[2].gameObject.SetActive(false);
                attackFrames[0].gameObject.SetActive(true);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && blocking==false && gameOver==false) {
            blocking = true;
            blockTimeLeft = blockTime;
            attackFrames[0].gameObject.SetActive(false);
            attackFrames[1].gameObject.SetActive(false); //To make sure theres no annoying overlap
            attackFrames[2].gameObject.SetActive(true);
        }

        if (hasAttacked) {
            hasAttacked = false;
            attackFrames[0].gameObject.SetActive(false);
            attackFrames[1].gameObject.SetActive(true);
            attackTimeLeft = attackTime;
        }

        if (attackTimeLeft <= 0 && blocking==false)
        {
            attackFrames[0].gameObject.SetActive(true);
            attackFrames[1].gameObject.SetActive(false);
            hasAttacked = false;
        }
        else
            attackTimeLeft -= Time.deltaTime;


        if (transform.position.z < -45f)
        {
            if (enemies[0].GetComponent<Enemy>().dead)
            {
                Vector3 moveTarget = new Vector3(transform.position.x, transform.position.y, -45.0f);
                transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);
            }
        }
        else if ( transform.position.z < 6f)
        {
            if (enemies[1].GetComponent<Enemy>().dead && enemies[2].GetComponent<Enemy>().dead)
            {
                Vector3 moveTarget = new Vector3(transform.position.x, transform.position.y, 6.0f);
                transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);

                //vector move again
            }
        }
        else if (transform.position.z > 5 && transform.position.z < 126f)
        {
            if (enemies[3].GetComponent<Enemy>().dead && enemies[4].GetComponent<Enemy>().dead && enemies[5].GetComponent<Enemy>().dead)
            {
                Vector3 moveTarget = new Vector3(transform.position.x, transform.position.y, 126f);
                transform.position = Vector3.MoveTowards(transform.position, moveTarget, (speed+10f) * Time.deltaTime);
                //vector move again
            }
        }
        else {
            if (enemies[6].GetComponent<Boss>().dead) {
                //games done
                gameOver = true;
                //display stuff on the UI that I still have to make ...

            }
        }
        
    }
}
