using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    //Script that contains settings for the designer to edit a level. 
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/EntitySetUpSettings")]
    public class LevelSettings : ScriptableObject
    {
        #region Entity Settings

        //Unfortunately since debug colors are limited, there isn't much I can do about letting a designer pick which colors are available, so I have hard coded them here.
        public enum EntityColor
        {
            Red, 
            Green, 
            Blue, 
            Yellow, 
            White, 
            Magenta
        }

        [System.Serializable]
        public struct EntitySettings
        {
            public Vector2 StartingPosition;
            public float Size;
            public EntityColor Color;
            public Vector2 StartingVelocity;
        }

  
        [Header("ENTITY SETTINGS", order = 0)]

        [Header("Planet Settings", order = 1)]

        [SerializeField] private int m_numberOfRandomPlanets; //Randomly generated planets remain still during play.
        public int NumberOfRandomPlanets { get { return m_numberOfRandomPlanets; } }
        [SerializeField]  private List<EntitySettings> m_handPickedPlanets;
        public List<EntitySettings> HandPickedPlanets { get { return m_handPickedPlanets; } }



        [Header("Moon Settings")]
        [SerializeField] private int m_NumberOfRandomMoons; //Randomly generated moons orbit the closest planet to their spawn.
        public int NumberOfRandomMoons { get { return m_NumberOfRandomMoons; } }

        [SerializeField] private List<EntitySettings> m_handPickedMoons; 
        public List<EntitySettings> HandPickedMoons { get { return m_handPickedMoons; } }

        #endregion
    }
}
