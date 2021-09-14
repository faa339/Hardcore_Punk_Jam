using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    //I dont even care that I'm breaking convention here I'm TIRED
    public GameObject UIPlane;
    public GameObject Title;
    public GameObject Description;
    public GameObject HPText;
    public GameObject StartButton;
    public GameObject RestartText;
    public GameObject RestartButton;
    public GameObject SuccessText;
    Player player;
    Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        UIPlane.transform.position = player.transform.position + new Vector3(0, 1.0f, 0.5f);

        if (player.health <= 0) {
            HPText.SetActive(false);
            UIPlane.SetActive(true);
            RestartText.SetActive(true);
            RestartButton.SetActive(true);
        }

        if (boss.bossHealth <= 0) {
            //UIPlane.SetActive(true);
            HPText.SetActive(false);
            SuccessText.SetActive(true);
            RestartButton.SetActive(true);
        }

    }

    public void StartGame() {
        UIPlane.SetActive(false);
        Title.SetActive(false);
        Description.SetActive(false);
        HPText.SetActive(true);
        StartButton.SetActive(false);
        player.gameStart = true;
    }

    public void RestartGame() {
        SceneManager.LoadScene("SampleScene");
    }
}
