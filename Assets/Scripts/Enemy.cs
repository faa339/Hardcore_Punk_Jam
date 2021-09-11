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
    public Button attackButton;
    public TextMeshProUGUI attackText;
    Image attackButtonImage;
    int attackNumbers;
    float attackWait;
    const float attackWaitTime = 2.0f;
    public bool canAttack;
    public int attackIndex; //The current part of the attack string reached 
    AudioClip attackSound;
    AudioClip deathSound;
    AudioSource audioSource;
    void Start()
    {
        //Generate the kill sequence 
        attackInputs = new int[] { 1, 1, 1, 1 };
        //inputText = inputText.Substring(0, inputText.Length);
        attackIndex = 0;
        attackButton.interactable = false;
        attackButtonImage = attackButton.GetComponent<Image>();
        attackText.text = attackInputs[attackIndex].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack == false) {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (attackInputs[attackIndex] == 1)
                {
                    
                    attackIndex++;
                    attackText.color = Color.red;
                }
            }
        }
    }

    private void OnMouseOver()
    {
        canAttack = false; //Enemy can't attack while targeted 
        attackButtonImage.color = Color.green;
    }

    private void OnMouseExit()
    {
        canAttack = true; //Target gone; enemy can attack
        attackButtonImage.color = Color.white;
        attackText.color = Color.black;
        attackIndex = 0;
    }
}
