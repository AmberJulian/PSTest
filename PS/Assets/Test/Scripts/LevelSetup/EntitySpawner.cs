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

            //Note to test marker: Definitely need to split this function up


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
                moons.Add(CreateMoon(moon.StartingPosition, moon.Color, moon.Size, new MoonBehaviour(closestPlanet),  moon.StartingVelocity));
            }

            moons.AddRange(CreateLevelsRandomMoons(levelSettings.NumberOfRandomMoons, planets, spawnRestrictions));

            //Players
            Entity player = CreatePlayerEntity(levelSettings.PlayerSettings.StartingPosition, levelSettings.PlayerSettings.Color, levelSettings.PlayerSettings.Size);

            //Satellites
            List<Entity> satellites = new List<Entity>();
            foreach (var satellite in levelSettings.HandPickedSatellites)
            {
                Entity closestPlanet = planets.OrderBy(t => (t.Position - satellite.StartingPosition).sqrMagnitude).FirstOrDefault();
                satellites.Add(CreateSmartSatellite(satellite.StartingPosition, satellite.Color, satellite.Size, new SmartSateliteBehaviour(closestPlanet), satellite.StartingVelocity));
            }


            //Add them together
            List<Entity> entities = new List<Entity>();
            entities.AddRange(planets);
            entities.AddRange(moons);
            entities.Add(player);
            entities.AddRange(satellites);

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

        public static Entity CreateMoon(Vector2 position, LevelSettings.EntityColor color, float size = 0.1f, MoonBehaviour behaviour = null, Vector2 velocity = new Vector2())
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
                newMoons.Add(CreateMoon(randomPosition, randomColor, randomSize, new MoonBehaviour(closestPlanet), randomVelocity));
            }

            return newMoons;
        }

        #endregion

        #region Player Creation
        public static Entity CreatePlayerEntity(Vector2 position, LevelSettings.EntityColor color, float size = 0.1f, Entity closestPlanet = null)
        {
            Entity player = new Entity(position, ConvertEntityColorToColor(color), size);
            player.SetBehaviour(new PlayerBehaviour(player));
            return player;

        }
        #endregion

        #region Smart Satellite Creation
        public static Entity CreateSmartSatellite(Vector2 position, LevelSettings.EntityColor color, float size = 0.1f, SmartSateliteBehaviour satelliteBehaviour = null, Vector2 velocity = new Vector2())
        {
            return new Entity(position, ConvertEntityColorToColor(color), size, satelliteBehaviour, velocity);

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
