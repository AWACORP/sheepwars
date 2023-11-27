using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
  private NetworkCharacterControllerPrototype _cc;
  private Character _character;
  private Vector3 _forward;
  [SerializeField] private Ball _prefabBall;
  [SerializeField] private PhysxBall _prefabPhysxBall;

  [Networked] private TickTimer delay { get; set; }

  private void Awake()
  {
    _cc = GetComponent<NetworkCharacterControllerPrototype>();
    _character = GetComponent<Character>();
    _forward = transform.forward;
  }

  public override void FixedUpdateNetwork()
  {
    if (GetInput(out NetworkInputData data))
    {
      data.direction.Normalize();
      _cc.Move(5 * data.direction * Runner.DeltaTime);

      if (data.direction.sqrMagnitude > 0)
        _forward = data.direction;

      if (data.jump && _cc.IsGrounded)
      {
        _cc.Jump();
        data.jump = false;
      }

      if (delay.ExpiredOrNotRunning(Runner))
      {
        if ((data.attack & NetworkInputData.MOUSEBUTTON1) != 0)
        {
          delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
          Runner.Spawn(_prefabBall,
            transform.position + _forward,
            Quaternion.LookRotation(_forward),
            Object.InputAuthority,
            (runner, o) =>
            {
              // Initialize the Ball before synchronizing it
              o.GetComponent<Ball>().Init();
            });
          _character.BaseAttack();
        }
        else if ((data.throwSlime & NetworkInputData.MOUSEBUTTON2) != 0)
        {
          delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
          Runner.Spawn(_prefabPhysxBall,
            transform.position + _forward,
            Quaternion.LookRotation(_forward),
            Object.InputAuthority,
            (runner, o) =>
            {
              o.GetComponent<PhysxBall>().Init(10 * _forward);
            });
          _character.ThrowSlime();
        }
      }
    }
  }
}