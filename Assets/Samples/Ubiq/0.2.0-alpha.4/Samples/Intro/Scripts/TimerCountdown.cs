using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    //Builder's property

    //public GameObject textDisplay;
    //public GameObject gameOver;
    //public GameObject bomb;
    public static int timeLeft = -1;
    public bool takingAway = false;
    private int minutesLeft;
    private int secondsLeft;

    public static bool stop = false;
    public static string displayTime;
    public Button start;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start CountDown");
        if (timeLeft == -1)
        {
            displayTime = "Display Time";
            //textDisplay.GetComponent<Text>().text = "Display Time";
            return;
        }
        minutesLeft = timeLeft / 60;
        secondsLeft = timeLeft % 60;
        if (secondsLeft < 10)
        {
            displayTime = "0" + minutesLeft + ":" + "0" + secondsLeft;
            //textDisplay.GetComponent<Text>().text = "0" + minutesLeft + ":" + "0" + secondsLeft;
        }
        else
        {
            displayTime = "0" + minutesLeft + ":" + secondsLeft;
            //textDisplay.GetComponent<Text>().text = "0" + minutesLeft + ":" + secondsLeft;
        }
    }

    public void set_secondsLeft(int value)
    {
        timeLeft = value;
        Start();
    }

    public void set_stop(bool bool_value)
    {
        stop = bool_value;
    }

    private void Update()
    {
        if (takingAway == false && timeLeft > 0 && stop == false)
        {
            StartCoroutine(TimerTake());
        }
        if (timeLeft == 0)
        {
            //bomb.GetComponent<Animator>().Play("attack01");
            //timeLeft = -1;
        } 
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        timeLeft -= 1;
        minutesLeft = timeLeft / 60;
        secondsLeft = timeLeft % 60;
        if (secondsLeft < 10)
        {
            displayTime = "0" + minutesLeft + ":" + "0" + secondsLeft;
            //textDisplay.GetComponent<Text>().text = "0" + minutesLeft +":"+ "0" +secondsLeft;
        }
        else
        {
            displayTime = "0" + minutesLeft + ":" + secondsLeft;
            //textDisplay.GetComponent<Text>().text = "0" + minutesLeft + ":" + secondsLeft;
        }
        takingAway = false;
    }
}
