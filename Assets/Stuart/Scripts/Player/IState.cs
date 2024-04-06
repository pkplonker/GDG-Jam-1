
	public interface IState
	{
		void EnterState(IStateMachine sm);
		void ExitState();
		void Tick();
	}
