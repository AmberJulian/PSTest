using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    //Script that contains settings for the designer to edit the general game. 
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [System.Serializable]
        public class EntityIntializationRestrictions
        {
            [Header("Entity Start Position Range", order = 1)]
            [Tooltip("These settings dictate the starting positions of randomly created entities")]
            [SerializeField] private float m_xPositionRange;
            public float XPositionRange { get { return m_xPositionRange; } }
            [SerializeField] private float m_yPositionRange;
            public float YPositionRange { get { return m_yPositionRange; } }

            [Header("Entity Starting Size")]
            [Tooltip("These settings dictate the starting sizes of randomly created entities")]
            [SerializeField] private float m_entityMinSize;
            public float EntityMinSize { get { return m_entityMinSize; } }

            [SerializeField] private float m_entityMaxSize;
            public float EntityMaxSize { get { return m_entityMaxSize; } }


            [Header("Entity Starting Velocity")]
            [Tooltip("These settings dictate the starting speeds of randomly created entities")]
            [SerializeField] private float m_entityMinVelocity;
            public float EntityMinVelocity { get { return m_entityMinVelocity; } }
            [SerializeField] private float m_entityMaxVelocity;

            public float EntityMaxVelocity { get { return m_entityMaxVelocity; } }
        }

        [Header("GENERAL SETTINGS")]
        [Tooltip("A higher time step will speed up the entire game.")]
        [SerializeField] private float m_fixedTimeStep = 0.016f;
        public float FixedTimeStep { get { return m_fixedTimeStep; } }

        [Header("RANDOM ENTITY GENERATION SETTINGS")]
        [SerializeField] private EntityIntializationRestrictions m_entityIntializationSettings;
        public EntityIntializationRestrictions EntityIntializationSettings { get { return m_entityIntializationSettings; } }
    }
}
