using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class PlayerBehaviour : IEntityBehaviour
    {
        private Entity m_controlledEntity;
        private float playerAcceleration = 2f;
        public PlayerBehaviour(Entity controlledEntity)
        {
            m_controlledEntity = controlledEntity;
            SubscribeToInputEvents();
        }

        private void SubscribeToInputEvents()
        {
            PlayerInput.Instance.ThrustKeyEvent += OnPlayerThrust;
        }

        private void OnPlayerThrust()
        {
            Vector2 playerForward = m_controlledEntity.Velocity.normalized;
            if (playerForward == Vector2.zero) playerForward = Vector2.up; //Always have a way to accelerate
            m_controlledEntity.SetVelocity(m_controlledEntity.Velocity + (playerForward * playerAcceleration * Time.deltaTime));
        }

        public void Update(Entity entity, float deltaTime)
        {       
            //Any physics for the player would go here, but putting in gravity makes the player look too similar to a moon presently so I have purposely left it blank.
        }
    }
}
