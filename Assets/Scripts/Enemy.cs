using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] liveFrames;
    public int[] attackInputs;
    public string stringInput;
    public Button attackButton;
    public TextMeshProUGUI attackText;
    public Image attackButtonImage;
    public int attackNumber;
    public float attackWait;
    public bool canAttack;
    public bool dead;
    public Player player;
    public AudioClip attackSound;
    public AudioClip deathSound;
    public AudioSource audioSource;
    void Start()
    {
        //Generating attack input array first -- set numbers up in the editor based on
        //proximity to boss (no more than like 6 or 7)
        attackInputs = new int[attackNumber + 4];
        for (int i = 0; i < attackNumber+4; i++)
            attackInputs[i] = Random.Range(1, 5);
        
        attackButtonImage = attackButton.GetComponent<Image>();
        foreach (int i in attackInputs) {
            if (i == 1)
                stringInput += "W";
            else if (i == 2)
                stringInput += "A";
            else if (i == 3)
                stringInput += "S";
            else
                stringInput += "D";
        }
        attackText.text = stringInput;
        player = GameObject.Find("Player").GetComponent<Player>();
        attackWait = Random.Range(1.0f, 2.0f) + 0.5f; //Assign some random time before the enemy can attack
        canAttack = true;
        dead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (dead) {
            gameObject.GetComponent<SpriteRenderer>().sprite = liveFrames[2];
        }

        //Don't attack if the player is too far
        if (Vector3.Distance(transform.position, player.transform.position) > 10.0f || player.gameOver ||player.gameStart==false) {
            return;
        }
        
        if (attackText.text == "") // No inputs left -- trigger death sequence
        {
            if (dead == false) {
                player.hasAttacked = true;
                attackButton.gameObject.SetActive(false);
                dead = true;
            }
            //Switch to death sprite
        }
        if (dead == false && player.gameOver==false) { //Don't bother dealing with this if the entity is dead
            if (canAttack==false && player.blocking == false)
            { //If targeted and the player isn't blocking
                //Debug.Log(attackText.text);
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (attackText.text[0] == 'W')
                    {
                        attackText.text = attackText.text.Remove(0, 1);
                    }
                    else
                    {
                        attackText.text = stringInput;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    if (attackText.text[0] == 'A')
                    {
                        attackText.text = attackText.text.Remove(0, 1);
                    }
                    else
                    {
                        attackText.text = stringInput;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (attackText.text[0] == 'S')
                    {
                        attackText.text = attackText.text.Remove(0, 1);
                    }
                    else
                    {
                        attackText.text = stringInput;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (attackText.text[0] == 'D')
                    {
                        attackText.text = attackText.text.Remove(0, 1);
                    }
                    else
                    {
                        attackText.text = stringInput;
                    }
                }
            }
            if (attackWait > 0)
            {
                attackWait -= Time.deltaTime;
                if (attackWait <= 0.75f)
                {
                    //Switch back to attack sprite
                    gameObject.GetComponent<SpriteRenderer>().sprite = liveFrames[1];
                }
            }
            else
            {
                Attack();
                attackWait = Random.Range(1.0f, 2.0f) + 0.5f;
                gameObject.GetComponent<SpriteRenderer>().sprite = liveFrames[0];

            }
        }
        if (player.health <= 0)
            player.gameOver = true;
    }

    private void Attack() {
        if (player.blocking == false)
            player.health -= 5;
    }

    private void OnMouseOver()
    {
        if (player.gameOver)
            return;
        canAttack = false;
        attackButtonImage.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (player.gameOver)
            return;
        if (attackText.text != "") {
            canAttack = true;
            attackButtonImage.color = Color.white;
            attackText.text = stringInput;
        }
    }
}
