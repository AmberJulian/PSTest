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

        public MoonBehaviour(Entity centreEntity, Entity thisEntity)
        {
            m_centreOfGravity = centreEntity != null ? centreEntity : new Entity();

            SetOrbitVelocity(thisEntity);


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

        private void SetOrbitVelocity(Entity entity)
        {
            Vector2 planetPosition = m_centreOfGravity.Position;
            Vector2 diff = planetPosition - entity.Position;

            Vector2 perfectVelocityForDistance = diff.normalized * Mathf.Sqrt(gravityForce * Vector2.Distance(entity.Position, planetPosition));

            Debug.LogError("hm" + planetPosition);

            entity.SetVelocity(new Vector2(-perfectVelocityForDistance.y, perfectVelocityForDistance.x ));
        }
    }
}