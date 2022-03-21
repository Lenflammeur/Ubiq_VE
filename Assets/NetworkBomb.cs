using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;
using UnityEngine.UI;
using static TimerCountdown;
using static quizManager;

namespace Ubiq.Samples
{
    public class NetworkBomb : MonoBehaviour, IGraspable, INetworkObject, INetworkComponent, ISpawnable
    {
        private Hand follow;
        private NetworkContext context;
        public static string correctWord = null;
        public bool owner = true;
        public Animator animator;
        private int last_time = -1;
        //private string displayTimeLeft; // only be updated by the other user's bomb
        private int time_left;

        //public Text textElement;
        public NetworkId Id { get; set; } = NetworkId.Unique();

        private void Awake()
        {
            time_left = -1;
            animator = GetComponent<Animator>();
            //displayTimeLeft = displayTime; //Only get once from local.
            Debug.Log("Bomb Time left:" + timeLeft);
            correctWord = answerWord;
        }

        public void OnSpawned(bool local)
        {
            owner = local;
        }

        public void Grasp(Hand controller)
        {
            follow = controller;
        }

        public void Release(Hand controller)
        {
            follow = null;
            
        }

        // Start is called before the first frame update
        void Start()
        {
            context = NetworkScene.Register(this);
        }

        private void FixedUpdate()
        {
            if (last_time == -1 && time_left > 5)
            {
                Debug.Log("Play Entry from" + time_left);
                this.GetComponent<Animator>().Play("Entry");
                last_time = time_left;
            }
            if (owner)
            {
                Debug.Log("owner" + timeLeft);
                time_left = timeLeft;
                correctWord = answerWord;
                //textElement.text = "word";
                context.SendJson(new Message(transform, animator, correctWord, time_left));
            }
            if (last_time > 0 && time_left < 0.5)
            {
                Debug.Log("attack01");
                this.GetComponent<Animator>().Play("attack01");
            }

            last_time = time_left;
        }
        public struct Message
        {
            public TransformMessage transform;
            public Animator animator;
            public string correctWord;
            //public string displayTimeLeft;
            public int time_left;
            public Message(Transform transform, Animator animator, string correctWord, int time_left)
            {
                this.transform = new TransformMessage(transform);
                this.animator = animator;
                this.correctWord = answerWord;
                this.time_left = time_left;
            }
        }
        public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
        {
            // the other end has taken control of this object
            owner = false;
            var msg = message.FromJson<Message>();
            transform.localPosition = msg.transform.position; // The Message constructor will take the *local* properties of the passed transform.
            transform.localRotation = msg.transform.rotation;
            animator = msg.animator;
            correctWord = msg.correctWord;
            //displayTimeLeft = msg.displayTimeLeft;
            time_left = msg.time_left;
        }
        /*IEnumerator TimerTake()
        {
            takingAway = true;
            yield return new WaitForSeconds(1);
            timeLeft -= 1;
            minutesLeft = timeLeft / 60;
            secondsLeft = timeLeft % 60;
            if (secondsLeft < 10)
            {
                //textDisplay.GetComponent<Text>().text = "0" + minutesLeft + ":" + "0" + secondsLeft;
            }
            else
            {
                //textDisplay.GetComponent<Text>().text = "0" + minutesLeft + ":" + secondsLeft;
            }

            takingAway = false;
        }*/
    }
}