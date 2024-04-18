using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjetPkmn.Assets.Map;
using ProjetPkmn.Assets.Map.TilesType;
using ProjetPkmn.Assets.Mons;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Map.Plans
{
    public class PlanConverter : JsonConverter<Plan>
    {
        public override void WriteJson(JsonWriter writer, Plan value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("x");
            writer.WriteValue(value.X);

            writer.WritePropertyName("y");
            writer.WriteValue(value.Y);

            // Sérialiser Tiles
            writer.WritePropertyName("tiles");
            serializer.Serialize(writer, value.Tiles);

            // Sérialiser Player
            writer.WritePropertyName("player");
            serializer.Serialize(writer, value.Player);

            // Sérialiser Encounters
            writer.WritePropertyName("encounters");
            serializer.Serialize(writer, value.Encounters);

            // Sérialiser TrainerTiles
            writer.WritePropertyName("trainerTiles");
            serializer.Serialize(writer, value.TrainerTiles);

            // Sérialiser TeleportationPoints
            writer.WritePropertyName("teleportationPoints");
            serializer.Serialize(writer, value.TeleportationPoints);

            // Sérialiser DeletedTiles
            writer.WritePropertyName("deletedTiles");
            serializer.Serialize(writer, value.DeletedTiles);

            writer.WriteEndObject();
        }
        public override Plan ReadJson(JsonReader reader, Type objectType, Plan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            int x = (int)obj["x"];
            int y = (int)obj["y"];

            // Désérialiser Tiles
            Tile[,] tiles = obj["tiles"].ToObject<Tile[,]>();

            // Désérialiser Player
            Trainer player = obj["player"].ToObject<Trainer>();

            // Désérialiser Encounters
            List<Pokemon> encounters = obj["encounters"].ToObject<List<Pokemon>>();

            // Désérialiser TrainerTiles
            List<TrainerNPC> trainerTiles = obj["trainerTiles"].ToObject<List<TrainerNPC>>();

            // Désérialiser TeleportationPoints
            List<TeleportationPoint> teleportationPoints = obj["teleportationPoints"].ToObject<List<TeleportationPoint>>();

            // Désérialiser DeletedTiles
            List<Dictionary<TrainerNPC, Tile>> deletedTiles = obj["deletedTiles"].ToObject<List<Dictionary<TrainerNPC, Tile>>>();

            Plan plan = new Plan(x, y, tiles, trainerTiles, teleportationPoints, player, encounters);
            plan.DeletedTiles = deletedTiles;

            return plan;
        }
    }
}
