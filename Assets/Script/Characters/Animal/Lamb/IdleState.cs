using UnityEngine;

namespace AIStateMachine.Animal.Lamb
{
    public class IdleState : AnimalBaseState
    {
        private float idleTimer, idleTimeInterval;
        private string[] randomIdleAnim;

        private bool goPatrol = false;

        public IdleState(AnimalStateMachine stateMachine, Transform player, float idleTimer, string[] randomIdleAnim) : base(stateMachine, player, AnimalState.Idle)
        {
            this.idleTimer = idleTimer;
            this.randomIdleAnim = randomIdleAnim;
        }

        public override void EnterState()
        {
            stateMachine.PlayRandomAnimations(randomIdleAnim);
        }

        public override void UpdateState()
        {
            idleTimeInterval += Time.deltaTime;
            if (idleTimeInterval < idleTimer) return;

            goPatrol = true;
        }

        public override void ExitState()
        {
            goPatrol = false;
            idleTimeInterval = 0;
        }

        public override AnimalState GetNextState()
        {
            if (goPatrol)
                return AnimalState.Patrol;

            return StateKey;
        }

        public override void OnTriggerStay(Collider other)
        {
        }
    }
}