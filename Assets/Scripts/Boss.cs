using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss : Enemy
{
    public int bossHealth = 3;
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
        attackWait = Random.Range(1.0f, 2.5f) + 1.0f; //Assign some random time before the enemy can attack
        canAttack = true;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 20.0f || player.gameOver) {
            return;
        }

        if (attackText.text == "")
        {
            if (bossHealth <= 0)
            {
                dead = true;
                attackButton.gameObject.SetActive(false);
                //switch to death sprite
                
            }
            else {
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
        }

        if (dead == false)
        { //Boss cases are a bit special -- it can attack while being targeted
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
                        player.health -= 10;
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
                        player.health -= 10;
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
                        player.health -= 10;
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
                        player.health -= 10;
                    }

                }
            }

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
                Debug.Log("Boss Attacked player");
                attackWait = Random.Range(1.0f, 2.5f) + 1.0f;
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }

    }

    private void OnMouseOver()
    {
        if (player.gameOver)
            return;
        attackButtonImage.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (player.gameOver)
            return;
        if (attackText.text != "")
        {
            attackButtonImage.color = Color.white;
            attackText.text = stringInput;
        }
    }

}
