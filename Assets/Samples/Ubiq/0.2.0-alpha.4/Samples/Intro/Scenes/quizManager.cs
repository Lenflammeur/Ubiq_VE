using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class quizManager : MonoBehaviour
{
    private List<string> questions = new List<string>
            { "cat", "apple", "banana", "lemon", "sun", "big", "birthday", "computer", "fire", "ladder", "stick", "egg", "eight", 
             "thunder", "elephant" , "hand", "toe", "ponytail", "bag", "basket", "orange", "kiwi", "beer", "wine", "grass", "cucumber", 
              "car", "bus", "castle", "TV", "key", "hard", "wood", "ice", "increase", "plus", "subtract" , "lion", "zebra"};
    int total_question = 0;
    private int currentAnswerIndex = 0;
    public static string answerWord;

    public Text textElement;

    void SetQuestion()
    {
        //set the answerWord string variable
        total_question = questions.Count;
        currentAnswerIndex = UnityEngine.Random.Range(0, total_question - 1);
        answerWord = questions[currentAnswerIndex];
    }

    public void SetRiddle() {
        SetQuestion();
        textElement.text = answerWord;
    }

    void start() {
        SetQuestion();
        textElement.text = answerWord;
    }
}
