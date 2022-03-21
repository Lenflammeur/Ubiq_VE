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
        public string correctWord = null;
        public bool owner = true;
        public Animator animator;
        private string timeLeft; // only be updated by the other user's bomb
        public Text textElement;
        public NetworkId Id { get; set; } = NetworkId.Unique();

        private void Awake()
        {
            animator = GetComponent<Animator>();
            timeLeft = displayTime; //Only get once from local.
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
            if (owner)
            {
                correctWord = answerWord;
                textElement.text = correctWord;
                context.SendJson(new Message(transform, animator, correctWord));
            }
        }
        public struct Message
        {
            public TransformMessage transform;
            public Animator animator;
            public string correctWord;
            public string timeLeft;

            public Message(Transform transform, Animator animator, string correctWord)
            {
                this.transform = new TransformMessage(transform);
                this.animator = animator;
                this.correctWord = answerWord;
                this.timeLeft = displayTime;
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
            timeLeft = msg.timeLeft;
        }
    }
}