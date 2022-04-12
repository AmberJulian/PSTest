using UnityEngine;

namespace Test
{
    public class MoonBehaviour : IEntityBehaviour
    {
        private Entity m_centreOfGravity;
        private float gravityForce = 10;

        public MoonBehaviour(Entity centreEntity)
        {
            m_centreOfGravity = centreEntity != null ? centreEntity : new Entity();
        }

        public void Update(Entity entity, float deltaTime)
        {
            // Apply gravity towards the centre  
            Vector2 diff = m_centreOfGravity.Position - entity.Position;


            float distanceSquared = diff.sqrMagnitude;
            Vector2 direction = diff.normalized;

            Vector2 acc = direction * (gravityForce / (distanceSquared));

            // Apply acceleration toward the centre of mass.
            entity.SetVelocity(entity.Velocity + acc * deltaTime);
        }
    }
}