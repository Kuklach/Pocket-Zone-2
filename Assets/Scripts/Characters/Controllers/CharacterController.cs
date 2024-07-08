using Characters;
using UnityEngine;

namespace Player
{
    public abstract class CharacterController : MonoBehaviour
    {

        [SerializeField] internal Character character;

        internal Rigidbody2D Rigidbody;
        internal Vector2 MoveInput;

        public void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected abstract void DefineInput();

        internal void Update()
        {
            DefineInput();
        }
        
        internal void FixedUpdate()
        {
            Rigidbody.velocity = character.CharacterData.MoveSpeed * MoveInput;
        }        
    }
}