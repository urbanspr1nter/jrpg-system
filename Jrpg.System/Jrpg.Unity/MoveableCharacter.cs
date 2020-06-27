using Jrpg.System;
using UnityEngine;

namespace Jrpg.Unity
{
    public abstract class MoveableCharacter : MonoBehaviour
    {
        protected class StateNames
        {
            public static string Idle = "Idle";
            public static string Up = "Up";
            public static string Down = "Down";
            public static string Left = "Left";
            public static string Right = "Right";
        }

        protected class DirectionFlagNames
        {
            public static string WalkingUp = "WalkingUp";
            public static string WalkingDown = "WalkingDown";
            public static string WalkingLeft = "WalkingLeft";
            public static string WalkingRight = "WalkingRight";
        }

        // Sprites
        public Sprite IdleDown;
        public Sprite IdleUp;
        public Sprite IdleLeft;
        public Sprite IdleRight;

        public string CurrentState { get; private set; }
        public string PreviousState { get; private set; }

        protected SpriteRenderer SpriteRenderer;
        protected Rigidbody2D RigidBody;
        protected Animator Animator;
        protected GameStore GameStore;

        protected void SetCurrentState()
        {
            var stateInfo = Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(StateNames.Up))
                CurrentState = StateNames.Up;
            else if (stateInfo.IsName(StateNames.Down))
                CurrentState = StateNames.Down;
            else if (stateInfo.IsName(StateNames.Left))
                CurrentState = StateNames.Left;
            else if (stateInfo.IsName(StateNames.Right))
                CurrentState = StateNames.Right;
        }

        protected void SetPreviousState()
        {
            var currentState = Animator.GetCurrentAnimatorStateInfo(0);

            if (currentState.IsName(StateNames.Up))
                PreviousState = StateNames.Up;
            else if (currentState.IsName(StateNames.Down))
                PreviousState = StateNames.Down;
            else if (currentState.IsName(StateNames.Left))
                PreviousState = StateNames.Left;
            else if (currentState.IsName(StateNames.Right))
                PreviousState = StateNames.Right;
        }

        protected void Idle()
        {
            Animator.SetBool(DirectionFlagNames.WalkingLeft, false);
            Animator.SetBool(DirectionFlagNames.WalkingRight, false);
            Animator.SetBool(DirectionFlagNames.WalkingUp, false);
            Animator.SetBool(DirectionFlagNames.WalkingDown, false);
        }

        protected void WalkDirection(string flagName)
        {
            Idle();
            Animator.SetBool(flagName, true);
        }

        protected void LateUpdate()
        {
            var currentState = Animator.GetCurrentAnimatorStateInfo(0);

            if (!currentState.IsName(StateNames.Idle))
                return;

            if (PreviousState.Equals(StateNames.Up))
                SpriteRenderer.sprite = IdleUp;
            else if (PreviousState.Equals(StateNames.Down))
                SpriteRenderer.sprite = IdleDown;
            else if (PreviousState.Equals(StateNames.Left))
                SpriteRenderer.sprite = IdleLeft;
            else if (PreviousState.Equals(StateNames.Right))
                SpriteRenderer.sprite = IdleRight;
            else
                SpriteRenderer.sprite = IdleDown;

            OnLateUpdate();
        }

        protected void Awake()
        {
            GameStore = GameStore.GetInstance();

            OnAwake();
        }

        // Start is called before the first frame update
        protected void Start()
        {
            PreviousState = StateNames.Idle;
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            RigidBody = GetComponent<Rigidbody2D>();

            OnStart();
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            OnObjectCollisionEnter(collision);
        }

        protected void OnCollisionExit2D(Collision2D collision)
        {
            OnObjectCollisionExit(collision);
        }

        protected virtual void OnLateUpdate()
        {
            return;
        }

        protected virtual void OnObjectCollisionEnter(Collision2D collision)
        {
            return;
        }

        protected virtual void OnObjectCollisionExit(Collision2D collision)
        {
            return;
        }

        protected abstract void OnAwake();
        protected abstract void OnStart();
    }
}
