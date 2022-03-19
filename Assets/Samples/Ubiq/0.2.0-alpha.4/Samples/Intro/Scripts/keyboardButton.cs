using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keyboardButton : MonoBehaviour
{
    // Start is called before the first frame update
    keyboard_new board;
    TextMeshProUGUI buttonText;

    void Start() {
        board = GetComponentInParent<keyboard_new>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text.Length == 1) {
            NameToButtonText();
            GetComponentInChildren<ButtonVR>().onRelease.AddListener(delegate
            {
                board.InsertChar(buttonText.text);
            });
        }
    }

    public void NameToButtonText() {
        buttonText.text = gameObject.name;
    }
}
