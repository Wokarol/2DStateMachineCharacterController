using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.StateSystem
{
    public abstract class State
    {
        public struct Transition
        {
            public State NextState { get; }
            public Func<bool> Evaluator { get; }
            public Action OnTransitionAction { get; }

            public Transition(Func<bool> evaluator, State nextState, Action onTransitionAction) {
                Evaluator = evaluator;
                NextState = nextState;
                OnTransitionAction = onTransitionAction;
            }
        }
        public class CantSetToNullException : Exception
        {
            public override string Message => "Transitions can't be set to null";
            public override string StackTrace {
                get {
                    string baseTrace = base.StackTrace;
                    int firstLineEnd = baseTrace.IndexOf('\n');
                    return baseTrace.Substring(firstLineEnd);
                }
            }
        }

        private List<Transition> transitions = new List<Transition>();
        public List<Transition> Transitions {
            get => transitions;
            set => transitions = value ?? throw new CantSetToNullException();
        }
        public abstract bool CanTransitionToSelf { get; }
        public virtual string Name { get; protected set; } = "No name setted";

        public event Action OnEnter;
        public event Action OnExit;

        public void Enter(StateMachine stateMachine) {
            OnEnter?.Invoke();
            EnterProcess(stateMachine);
        }
        public void Exit(StateMachine stateMachine) {
            ExitProcess(stateMachine);
            OnExit?.Invoke();
        }

        protected abstract void EnterProcess(StateMachine stateMachine);
        protected abstract void ExitProcess(StateMachine stateMachine);

        public State Tick() {
            State processedState = Process();
            if (processedState != null) return processedState;

            State nextState = CheckTransitions();
            return nextState;
        }
        protected abstract State Process();

        private State CheckTransitions() {
            foreach (var transition in Transitions) {
                if (transition.Evaluator()) {
                    transition.OnTransitionAction?.Invoke();
                    return transition.NextState;
                }
            }
            return null;
        }

        /// <summary>
        /// Adds transition to Transitions list (works exactly like adding transition manually)
        /// </summary>
        /// <param name="evaluator">Transition is active if this function returns true</param>
        /// <param name="nextState">State to transition to</param>
        /// <param name="onTransitionAction">Action that is called when transition is executed</param>
        public void AddTransition(Func<bool> evaluator, State nextState, Action onTransitionAction = null) {
            Transitions.Add(new Transition(evaluator, nextState, onTransitionAction));
        }
    }
}
