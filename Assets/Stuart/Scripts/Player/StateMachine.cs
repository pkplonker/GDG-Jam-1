public abstract class StateMachine : IStateMachine
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
}