using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishCollectionController : GenericUnitySingleton<RubbishCollectionController>
{
	public static event Action<int> OnScoreChanged;
	private int rubbishGathered;

	public int RubbishGathered
	{
		get => rubbishGathered;
		set
		{
			if (rubbishGathered != value)
			{
				rubbishGathered = value;
				OnScoreChanged?.Invoke(rubbishGathered);
			}
		}
	}

	private void Start()
	{
		Gatherer.TargetHit += OnTargetHit;
	}

	private void OnTargetHit(IHitTarget target)
	{
		RubbishGathered++;
	}
}