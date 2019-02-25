using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.StateSystem;

namespace Wokarol.StateSystem
{
    public class RandomSelectorState : State
    {
        private readonly State[] allStates;
        private State nextState;

        public override bool CanTransitionToSelf => true;


        public RandomSelectorState(string name, params State[] allStates) {
            Name = name;
            this.allStates = allStates;
        }

        protected override void EnterProcess(StateMachine stateMachine) {
            nextState = allStates[Random.Range(0, allStates.Length)];
        }

        protected override void ExitProcess(StateMachine stateMachine) {

        }

        protected override State Process() {
            return nextState;
        }
    } 
}
