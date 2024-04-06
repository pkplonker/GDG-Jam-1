using UnityEngine;

public abstract class StateMachine : MonoBehaviour, IStateMachine
{
	protected IState currentState;

	protected virtual void Update()
	{
		currentState.Tick();
	}

	public virtual void ChangeState(IState state)
	{
		currentState?.ExitState();
		currentState = state;
		currentState.EnterState(this);
	}

	public Transform GetTransform() => transform;
}