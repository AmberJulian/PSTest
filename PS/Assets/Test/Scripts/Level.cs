using UnityEngine;
using System.Collections.Generic;
namespace Test
{
    public class Level
    {
        private List<Entity> entities;

        private float m_timeStepAccumulator = 0.0f;
        private float m_fixedTimeStep;

        public Level(GameSettings gameSettings, LevelSettings levelSettings)
        {
            m_fixedTimeStep = gameSettings.FixedTimeStep;
            entities = EntitySpawner.SpawnLevelEntities(levelSettings, gameSettings.EntityIntializationSettings);
        }
        
        public void Update(float deltaTime)
        {
            m_timeStepAccumulator += deltaTime;

            for (; m_timeStepAccumulator > m_fixedTimeStep; m_timeStepAccumulator -= m_fixedTimeStep)
            {
                foreach (var entity in entities)
                {
                    entity.Update(m_fixedTimeStep);
                }
            }
        }

        public void Render()
        {
            foreach (var entity in entities)
            {
                entity.Render();
            }
        }
    }
}