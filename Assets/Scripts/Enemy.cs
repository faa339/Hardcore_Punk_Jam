using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D[] liveFrames;
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
        for (int i = 0; i < attackInputs.Length; i++)
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
        attackWait = Random.Range(0.5f, 1.5f) + 1.0f; //Assign some random time before the enemy can attack
        canAttack = true;
        dead = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Don't attack if the player is too far
        if (Vector3.Distance(transform.position, player.transform.position) > 10.0f) {
            return;
        }
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));

        if (attackText.text == "")
        {
            dead = true;
            attackButton.gameObject.SetActive(false);
            //Switch to death sprite
        }
        if (dead == false) { //Don't bother dealing with this if the entity/player is dead
            if (canAttack == false && player.blocking == false)
            { //If targeted and the player isn't blocking
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (attackText.text[0] == 'W')
                    {
                        string newStr = attackText.text.Remove(0, 1);
                        attackText.text = newStr;
                    }
                    else
                    {
                        attackText.text = stringInput;
                        player.health -= 5;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    if (attackText.text[0] == 'A')
                    {
                        string newStr = attackText.text.Remove(0, 1);
                        attackText.text = newStr;
                    }
                    else
                    {
                        attackText.text = stringInput;
                        player.health -= 5;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (attackText.text[0] == 'S')
                    {
                        string newStr = attackText.text.Remove(0, 1);
                        attackText.text = newStr;
                    }
                    else
                    {
                        attackText.text = stringInput;
                        player.health -= 5;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (attackText.text[0] == 'D')
                    {
                        string newStr = attackText.text.Remove(0, 1);
                        attackText.text = newStr;
                    }
                    else
                    {
                        attackText.text = stringInput;
                        player.health -= 5;
                    }
                }
            }
            else
            { //It's ok to attack when the player isnt targeted or is blocking  
                if (attackWait > 0)
                {
                    attackWait -= Time.deltaTime;
                    if (attackWait <= 1.0f)
                    {
                        //Switch back to attack sprite
                        gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    }
                }
                else
                {
                    Attack();
                    attackWait = Random.Range(1.0f, 2.5f) + 1.0f;
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
        
    }

    protected void Attack() {
        //Check if player's attackNumber is equal to ours
        //If so, theyre valid to be attacked; if not, theyre too far
        //Do the attack animation -- put some brainwaves on it or something
        if (player.blocking == false)
            player.health -= 10;
    }

    private void OnMouseOver()
    {
        canAttack = false; //Enemy can't attack while targeted 
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        attackWait = Random.Range(0.5f, 1.5f) + 1.0f;
        attackButtonImage.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (attackText.text != "") {
            canAttack = true; //Target gone; enemy can attack
            attackButtonImage.color = Color.white;
            attackText.text = stringInput;
        }
    }
}
