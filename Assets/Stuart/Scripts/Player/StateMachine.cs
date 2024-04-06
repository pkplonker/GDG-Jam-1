using UnityEngine;

public abstract class StateMachine : MonoBehaviour, IStateMachine
{
	protected IState currentState;

	protected virtual void Update()
	{
		currentState.Tick();
	}

    protected virtual void FixedUpdate()
    {
        currentState.FixedTick();
    }

    public virtual void ChangeState(IState state)
	{
		currentState?.ExitState();
		currentState = state;
		currentState.EnterState(this);
	}

	public Transform GetTransform() => transform;
}