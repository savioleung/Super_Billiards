using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Text timeT;
    [SerializeField]
    private Text BallCountT;

    public static bool menuOn;

    [SerializeField]
    public static bool canMove;
    public static bool gameStart;
    public static bool islose;
    private int min = 0;
    private float sec = 0;
    // Start is called before the first frame update
    void Start()
    {
        islose = false;
        timeT.text = "00:00";
        BallCountT.text = "Ball;0";
        gameStart = false;
        menuOn = false;
        menu.SetActive(false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuOn)
            {
                menuOn = true;
                menu.SetActive(true);
            }
            else
            {
                menuOn = false;
                menu.SetActive(false);
            }
        }
        if (gameStart&& !islose)
        {
            timerC();
        }
        else if(islose){

            timeT.text = "you failed.";
            timeT.transform.localPosition = Vector2.zero;
            timeT.transform.localScale = Vector3.one * 2;
        }
        BallCountT.text = "Ball:"+checkBall().ToString();
    }
    int checkBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        return balls.Length;

    }

    void timerC()
    {

        if (checkBall() > 0)
        {
            sec += Time.deltaTime;
            if (sec >= 60f)
            {
                min++;
                sec -= 60;
            }
            timeT.text = min.ToString("00") + ":" + ((int)sec).ToString("00");
        }
        else
        {
            timeT.transform.localPosition=Vector2.zero ;
            timeT.transform.localScale = Vector3.one * 2;
        }

    }

    public void OnResetButtonClick()
    {
        // Œ»İ‚ÌScene‚ğæ“¾
        Scene loadScene = SceneManager.GetActiveScene();
        // Œ»İ‚ÌƒV[ƒ“‚ğÄ“Ç‚İ‚İ‚·‚é
        SceneManager.LoadScene(loadScene.name);
    }
    
}
