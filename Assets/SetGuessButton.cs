using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ubiq.Samples.Social
{
    public class SetGuessButton : MonoBehaviour
    {
        public GuessManager guessManager;
        public Text nameText;

        // Expected to be called by a UI element
        public void SetName()
        {
            if (nameText && guessManager)
            {
                guessManager.set_myGuess(nameText);
            }
        }
    }
}