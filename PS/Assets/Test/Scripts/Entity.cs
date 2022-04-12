using UnityEngine;

namespace Test
{
    // The generic space entity.
    public class Entity
    {
        #region Entity Parameters

        private float m_size;
        public float Size { get { return m_size; } private set { m_size = value; } }

        private Vector2 m_position;
        public Vector2 Position { get { return m_position; } private set { m_position = value; } }

        private Vector2 m_velocity;
        public Vector2 Velocity { get { return m_velocity; } private set { m_velocity = value; } }

        private Color m_color;
        public Color Color { get { return m_color; } private set { m_color = value; } }

        private EntityBehaviour m_behaviour;

        #endregion

        #region Constructors
        public Entity() { }

        public Entity(Vector2 position, Vector2 velocity = new Vector2(), float size = 0.1f)
        {
            Position = position;
            Velocity = velocity;
            Size = size;
        }

        public Entity(Vector2 position, Color color, float size = 0.1f, EntityBehaviour behaviour = null, Vector2 velocity = new Vector2())
        {
            Position = position;
            Velocity = velocity;
            m_behaviour = behaviour;
            Size = size;
            Color = color;
        }

        #endregion

        #region Entity Setters
        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void SetSize(float size)
        {
            Size = size;
        }

        public void SetBehaviour(EntityBehaviour behaviour)
        {
            m_behaviour = behaviour;
        }
        #endregion

        #region Entity Lifecycle

        public void Update(float deltaTime)
        {
            if (m_behaviour != null)
            {
                m_behaviour.Update(this, deltaTime);
            }

            // Do integration over time.
            Position += Velocity * deltaTime;
        }

        public void Render()
        {
            Vector3 pos = new Vector3(Position.x, Position.y, 0);

           

            // We assume this is stationary body (planet) if its velocity is too low.
            if (Velocity.magnitude < 0.01f)
            {
                Vector3 up = new Vector3(0,1,0)* Size;
                Vector3 right = new Vector3(1,0,0)* Size;
                
                Debug.DrawLine(pos + up + right, pos - up + right, Color);
                Debug.DrawLine(pos - up + right, pos - up - right, Color);
                Debug.DrawLine(pos - up - right, pos + up - right, Color);
                Debug.DrawLine(pos + up - right, pos + up + right, Color);
            }
            else
            {
                Vector2 direction = Velocity.normalized;
                Vector3 dirEnd = new Vector3(Position.x + direction.x* Size, Position.y + direction.y* Size, 0);
                Debug.DrawLine(pos, dirEnd, Color);
            }
        }

        #endregion
    }
}