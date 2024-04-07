using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   [SerializeField]
   private GameObject vfx;

   private void OnEnable()
   {
      ((DeadState) PlayerStateMachine.Instance.DeadState).Death += OnGameOver;
   }

   private void OnGameOver()
   {
      vfx.SetActive(true);
   }
}
