using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Test
{
    public static class EntitySpawner
    {
        public static List<Entity> SpawnLevelEntities(LevelSettings levelSettings, GameSettings.EntityIntializationRestrictions spawnRestrictions)
        {

            //Planets - Handpicked and then randoms
            List<Entity> planets = new List<Entity>();
            foreach (var planet in levelSettings.HandPickedPlanets)
            {
                planets.Add(CreatePlanet(planet.StartingPosition, planet.Color, planet.Size));
            }

            planets.AddRange(CreateLevelsRandomPlanets(levelSettings.NumberOfRandomPlanets, spawnRestrictions));


            //Moons - Handpicked and then randoms
            List<Entity> moons = new List<Entity>();
            foreach (var moon in levelSettings.HandPickedMoons)
            {
                Entity closestPlanet = planets.OrderBy(t => (t.Position - moon.StartingPosition).sqrMagnitude).FirstOrDefault();
                moons.Add(CreateMoon(moon.StartingPosition, moon.Color, moon.Size, new EntityBehaviour(closestPlanet),  moon.StartingVelocity));
            }

            moons.AddRange(CreateLevelsRandomMoons(levelSettings.NumberOfRandomMoons, planets, spawnRestrictions));

            //Add them together
            List<Entity> entities = new List<Entity>();
            entities.AddRange(planets);
            entities.AddRange(moons);

            return entities;
        }

        #region Planet Creation
        public static Entity CreatePlanet(Vector2 position, LevelSettings.EntityColor color, float size = 0.2f)
        {
            return new Entity(position, ConvertEntityColorToColor(color), size);
        }

        public static List<Entity> CreateLevelsRandomPlanets(int numberOfPlanets, GameSettings.EntityIntializationRestrictions spawnRestrictions)
        {
            List<Entity> newPlanets = new List<Entity>();

            for(int i = 0; i < numberOfPlanets; i++)
            {
                //Choose new random settings
                Vector2 randomPosition = new Vector2(Random.Range(-spawnRestrictions.XPositionRange, spawnRestrictions.XPositionRange),
                                                     Random.Range(-spawnRestrictions.YPositionRange, spawnRestrictions.YPositionRange));

                LevelSettings.EntityColor randomColor = GetRandomEntityColor();



                float randomSize = Random.Range(spawnRestrictions.EntityMinSize, spawnRestrictions.EntityMaxSize);

                //Apply random settings
                newPlanets.Add(CreatePlanet(randomPosition, randomColor, randomSize));
            }

            return newPlanets;
        }
        #endregion

        #region Moon Creation

        public static Entity CreateMoon(Vector2 position, LevelSettings.EntityColor color, float size = 0.1f, EntityBehaviour behaviour = null, Vector2 velocity = new Vector2())
        {
            return new Entity(position, ConvertEntityColorToColor(color), size, behaviour, velocity);
        }

        public static List<Entity> CreateLevelsRandomMoons(int numberOfMoons, List<Entity> potentialPlanets, GameSettings.EntityIntializationRestrictions spawnRestrictions)
        {
            List<Entity> newMoons = new List<Entity>();

            for (int i = 0; i < numberOfMoons; i++)
            {
                //Choose new random settings
                Entity planetToOrbit = potentialPlanets.RandomItem();


                Vector2 randomPosition = new Vector2(Random.Range(-spawnRestrictions.XPositionRange, spawnRestrictions.XPositionRange),
                                                     Random.Range(-spawnRestrictions.YPositionRange, spawnRestrictions.YPositionRange));

                LevelSettings.EntityColor randomColor = GetRandomEntityColor();

                float randomSize = Random.Range(spawnRestrictions.EntityMinSize, spawnRestrictions.EntityMaxSize);

                Entity closestPlanet = potentialPlanets.OrderBy(t => (t.Position - randomPosition).sqrMagnitude).FirstOrDefault();

                Vector2 randomVelocity = new Vector2(Random.Range(-spawnRestrictions.EntityMaxVelocity, spawnRestrictions.EntityMaxVelocity),
                                                     Random.Range(-spawnRestrictions.EntityMaxVelocity, spawnRestrictions.EntityMaxVelocity));


                //Apply random settings
                newMoons.Add(CreateMoon(randomPosition, randomColor, randomSize, new EntityBehaviour(closestPlanet), randomVelocity));
            }

            return newMoons;
        }

        #endregion

        private static Color ConvertEntityColorToColor(LevelSettings.EntityColor entityColor)
        {
            switch(entityColor)
            {
                case LevelSettings.EntityColor.White:
                    return Color.white;
                case LevelSettings.EntityColor.Red:
                    return Color.red;
                case LevelSettings.EntityColor.Green:
                    return Color.green;
                case LevelSettings.EntityColor.Blue:
                    return Color.blue;
                case LevelSettings.EntityColor.Yellow:
                    return Color.yellow;
                case LevelSettings.EntityColor.Magenta:
                    return Color.magenta;
            }
            return Color.gray;
        }

        private static LevelSettings.EntityColor GetRandomEntityColor()
        {
            LevelSettings.EntityColor randomColor = (LevelSettings.EntityColor)Random.Range(0, System.Enum.GetNames(typeof(LevelSettings.EntityColor)).Length);
            return randomColor;
        }
    }
}
