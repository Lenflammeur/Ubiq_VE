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
        
        public bool owner = true;
        public Animator animator;
        private int last_time = -1;
        public GameObject InputWord; //Find input
        public GameObject displayTimeObject;
        // only be updated by the other user's bomb

        // NETWORK OBJECTS
        public static string correctWord = null; //NETWORK
        private string displayTimeLeft; //NETWORK
        private int time_left; //NETWORK
        private bool isStarted = false; //NETWORK
        private bool localStop; //NETWORK
        public GameObject gameOver;

        public NetworkId Id { get; set; } = NetworkId.Unique();

        private void Awake()
        {
            Debug.Log("Bomb Awaken");
            time_left = -1;

            animator = GetComponent<Animator>();
            InputWord = GameObject.Find("InputWord");
            displayTimeObject = GameObject.Find("DisplayTime");
            
            gameOver = GameObject.Find("GameOver");
            displayTimeLeft = displayTime; //Only get once from local.
            displayTimeObject.GetComponent<Text>().text = displayTimeLeft;
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
            // Judge win for owner
            if(time_left == -1000)
            {
                gameOver.GetComponent<Text>().text = "YOUR GROUP WIN!";//Win for owner
                //Back to original state and notice the non-owner
                time_left = -1;
                stop = true;
                context.SendJson(new Message(transform, correctWord, time_left, displayTimeLeft, stop, isStarted));
            }

            //Sync with timer
            time_left = timeLeft;
            displayTimeObject.GetComponent<Text>().text = displayTimeLeft;

            // During time for owner
            if (!owner)
            {
                Debug.Log("I am not owner");
            }
            if (owner)
            {
                Debug.Log("owner" + timeLeft);
                displayTimeLeft = displayTime;
                correctWord = answerWord;
                //non-owner time_left sync with timeLeft on owner's timer
                context.SendJson(new Message(transform, correctWord, time_left, displayTimeLeft, stop, isStarted)); 
            }
            if (last_time - time_left == 1 && !isStarted && !stop)
            {
                Debug.Log("Play Entry from" + time_left + owner);
                this.GetComponent<Animator>().Play("Entry");
                isStarted = true;
            }
            //During time for non-owner
            if (!owner && time_left > 0 && !stop)
            {
                if (InputWord.GetComponent<Text>().text == correctWord)
                {
                    stop = true;
                    timeLeft = -1;
                    time_left = -1000;
                    gameOver.GetComponent<Text>().text = "YOUR GROUP WIN!";
                    context.SendJson(new Message(transform, correctWord, time_left, displayTimeLeft, stop, isStarted));//Notice the owner that we win
                }
            }
            //Bomb exploared
            if (time_left == 0 && !stop)
            {
                //Lose for Both players
                Debug.Log("attack01" + time_left + owner);
                this.GetComponent<Animator>().Play("attack01"); 
                gameOver.GetComponent<Text>().text = "GAME OVER!";
                
                stop = true;
                isStarted = false;
                timeLeft = -1;//Game end with initial timeLeft
            }
            
            last_time = time_left;
        }
        public struct Message
        {
            public TransformMessage transform;
            public string correctWord;
            public string displayTimeLeft;
            public int time_left;
            public bool stop;
            public bool isStarted;
            public Message(Transform transform, string correctWord, int time_left, string displayTimeLeft, bool stop, bool isStarted)
            {
                this.transform = new TransformMessage(transform);
                this.correctWord = answerWord;
                this.time_left = time_left;
                this.displayTimeLeft = displayTimeLeft;
                this.stop = stop;
                this.isStarted = isStarted;
            }
        }
        public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
        {
            // the other end has taken control of this object
            owner = false;
            var msg = message.FromJson<Message>();
            transform.localPosition = msg.transform.position; // The Message constructor will take the *local* properties of the passed transform.
            transform.localRotation = msg.transform.rotation;
            correctWord = msg.correctWord;
            displayTimeLeft = msg.displayTimeLeft;
            time_left = msg.time_left;
            stop = msg.stop;
            isStarted = msg.isStarted;
        }
    }
}