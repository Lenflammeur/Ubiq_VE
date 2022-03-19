using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keyboard_new : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject normalButtons;
    public GameObject capsButtons;
    private bool caps;


    void start() {
        caps = true;
    }

    public void InsertChar(string c)
    {
        inputField.text += c;
    }

    public void InsertSpace() {
        inputField.text += " ";
    }

    public void DeleteChar(){
        if (inputField.text.Length > 0) {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    /*
    public void CapsPressed() {
        if (!caps)
        {
            normalButtons.SetActive(false);
            capsButtons.SetActive(true);
            caps = true;
        }
        else {
            capsButtons.SetActive(false);
            normalButtons.SetActive(true);
            caps = false;
        }
    }*/
}
