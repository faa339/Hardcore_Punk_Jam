using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss : Enemy
{
    public int bossHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        attackInputs = new int[attackNumber];
        for (int i = 0; i < attackInputs.Length; i++)
            attackInputs[i] = Random.Range(1, 5);

        attackButtonImage = attackButton.GetComponent<Image>();
        foreach (int i in attackInputs)
        {
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
        bossHealth = 3;
        canAttack = true;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) {
            gameObject.GetComponent<SpriteRenderer>().sprite = liveFrames[2];
            return;
        }

        if (bossHealth < 1 && dead==false)
        {
            
            dead = true;
            canAttack = false;
            attackButton.gameObject.SetActive(false);
            return;
            //switch to death sprite
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 25.0f || player.gameOver || player.gameStart==false) {
            return;
        }

        if (attackText.text == "")
        {
            bossHealth -= 1;
            for (int i = 0; i < attackInputs.Length; i++)
                attackInputs[i] = Random.Range(1, 5);
            stringInput = "";
            foreach (int i in attackInputs)
            {
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
        }

        if (dead == false)
        { 
            if (player.blocking == false)
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

            if (attackWait > 0 && dead==false)
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

    }

    private void Attack()
    {
        if (player.blocking == false && dead==false)
            player.health -= 10;
    }

    private void OnMouseOver()
    {
        if (player.gameOver || dead)
            return;
        attackButtonImage.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (player.gameOver || dead)
            return;
        if (attackText.text != "")
        {
            attackButtonImage.color = Color.white;
            attackText.text = stringInput;
        }
    }

}
