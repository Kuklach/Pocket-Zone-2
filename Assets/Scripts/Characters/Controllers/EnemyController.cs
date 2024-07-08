using Characters;
using UnityEngine;

namespace Player
{
    public class EnemyController : CharacterController
    {
        [SerializeField] protected CharacterVision vision;

        protected override void DefineInput()
        {
            if (vision.Target is null)
            {
                MoveInput = Vector2.zero;
                return;
            }
            MoveInput = (vision.Target.transform.position - transform.position).normalized;
        }
    }
}