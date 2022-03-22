using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessManager : MonoBehaviour
{
    public string Word_toGuess;
    public GameObject gameOver;
    public TimerCountdown Timer;
    private string myGuess = "";


    public void set_myGuess(Text new_guess)
    {
        myGuess = new_guess.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (myGuess.Equals(Word_toGuess))
        {
            Timer.set_stop(true);
            gameOver.GetComponent<Text>().text = "YOU WIN!";
        }
    }
}
