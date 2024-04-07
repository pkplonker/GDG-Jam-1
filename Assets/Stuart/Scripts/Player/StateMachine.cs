using System;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour, IStateMachine
{
	protected IState currentState;
	private PauseController pauseController;

	private void Start()
	{
		pauseController = FindObjectOfType<PauseController>();
	}

	protected virtual void Update()
	{
		if (!pauseController.IsPaused) currentState.Tick();
	}

	protected virtual void FixedUpdate()
	{
		if (!pauseController.IsPaused) currentState.FixedTick();
	}

	public virtual void ChangeState(IState state)
	{
		currentState?.ExitState();
		currentState = state;
		currentState.EnterState(this);
	}

	public Transform GetTransform() => transform;
}