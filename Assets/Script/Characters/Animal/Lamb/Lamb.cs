using UnityEngine;

namespace AIStateMachine.Animal.Lamb
{
    public class Lamb : AnimalStateMachine
    {
        [SerializeField] private int radius;

        protected override void RegisterState()
        {
            AssignComponent();

            States.Add(AnimalState.Idle, new IdleState(this, player, idleTimer, randomIdleAnim));
            States.Add(AnimalState.Patrol, new PatrolState(this, player, agent, radius));

            CurrentState = States[firstState];
        }
    }
}