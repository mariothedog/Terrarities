using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrarity.Buffs.GreaterPotions
{
	public class GreaterSpelunkerBuff : ModBuff
	{
		private static readonly Dictionary<int, Vector3> tileColors = new Dictionary<int, Vector3>()
		{
			// Ores
			{ TileID.Copper, new Vector3(0.72f, 0.35f, 0.098f) },
			{ TileID.Tin, new Vector3(0.51f, 0.49f, 0.36f) },
			{ TileID.Iron, new Vector3(0.74f, 0.62f, 0.55f) },
			{ TileID.Lead, new Vector3(0.33f, 0.45f, 0.48f) },
			{ TileID.Silver, new Vector3(0.67f,0.71f, 0.72f) },
			{ TileID.Tungsten, new Vector3(0.3f, 0.46f, 0.29f) },
			{ TileID.Gold, new Vector3(0.91f, 0.84f, 0.25f) },
			{ TileID.Platinum, new Vector3(0.34f, 0.48f, 0.55f) },
			{ TileID.Demonite, new Vector3(0.27f, 0.27f, 0.45f) },
			{ TileID.Crimtane, new Vector3(0.85f, 0.23f, 0.25f) },
			{ TileID.Cobalt, new Vector3(0.21f, 0.66f, 0.81f) },
			{ TileID.Palladium, new Vector3(0.96f, 0.38f, 0.22f) },
			{ TileID.Mythril, new Vector3(0.4f, 0.64f, 0.38f) },
			{ TileID.Orichalcum, new Vector3(0.64f, 0.086f, 0.62f) },
			{ TileID.Adamantite, new Vector3(0.68f, 0.14f, 0.32f) },
			{ TileID.Titanium, new Vector3(0.11f, 0.24f, 0.28f) },
			{ TileID.Chlorophyte, new Vector3(0.46f, 0.85f, 0.075f) },
			{ TileID.LunarOre, new Vector3(0.37f, 0.9f, 0.64f) },

			// Gems
			{ TileID.Diamond, new Vector3(0.098f, 0.82f, 0.91f) },
			{ TileID.Ruby, new Vector3(0.98f, 0.16f, 0.32f) },
			{ TileID.Emerald, new Vector3(0.02f, 0.79f, 0.36f) },
			{ TileID.Sapphire, new Vector3(0.16f, 0.51f, 0.98f) },
			{ TileID.Topaz, new Vector3(0.78f, 0.55f, 0.035f) },
			{ TileID.Amethyst, new Vector3(0.64f, 0.04f, 0.84f) },
		};

		private static readonly int maxDist = 60;
		private static readonly int sparkleDustSpawnChance = 5; // 1 / x

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Greater Spelunker");
			Description.SetDefault("Todo");
			Main.buffNoSave[Type] = true;
			canBeCleared = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.findTreasure = true; // For spelunker highlight effect

			Vector2 playerPos = new Vector2(player.Center.X / 16, player.Center.Y / 16);

			// Iterate through tiles within maxDist range of the player
			for (int tilePosX = (int)playerPos.X - maxDist; tilePosX <= playerPos.X + maxDist; tilePosX++)
			{
				for (int tilePosY = (int)playerPos.Y - maxDist; tilePosY <= playerPos.Y + maxDist; tilePosY++)
				{
					// Exclude tiles if they're out of the boundaries
					if (tilePosX <= 0 || tilePosX >= Main.maxTilesX ||
						tilePosY <= 0 || tilePosY >= Main.maxTilesY)
					{
						continue;
					}

					// Exclude tiles if they are invalid
					Tile tile = Main.tile[tilePosX, tilePosY];
					if (tile == null || !tile.active())
					{
						continue;
					}

					// Honestly, I don't know exactly what this does but it's in the vanilla spelunker code
					bool flag9 = false;
					if (Main.tile[tilePosX, tilePosY].type == TileID.SmallPiles && Main.tile[tilePosX, tilePosY].frameY == 18)
					{
						if (Main.tile[tilePosX, tilePosY].frameX >= 576 && Main.tile[tilePosX, tilePosY].frameX <= 882)
						{
							flag9 = true;
						}
					}
					else if (Main.tile[tilePosX, tilePosY].type == TileID.LargePiles && Main.tile[tilePosX, tilePosY].frameX >= 864 && Main.tile[tilePosX, tilePosY].frameX <= 1170)
					{
						flag9 = true;
					}

					if (flag9 || Main.tileSpelunker[Main.tile[tilePosX, tilePosY].type] || (Main.tileAlch[Main.tile[tilePosX, tilePosY].type] && Main.tile[tilePosX, tilePosY].type != TileID.ImmatureHerbs))
					{
						// Spawn dust
						Vector2 pos = new Vector2(tilePosX * 16, tilePosY * 16);
						if (Main.rand.Next(sparkleDustSpawnChance) == 0)
						{
							int dustIndex = Dust.NewDust(pos, 16, 16, 204, 0f, 0f, 150, default, 0.3f);
							Main.dust[dustIndex].fadeIn = 0.75f;
							Dust dust2 = Main.dust[dustIndex];
							dust2.velocity *= 0.1f;
							Main.dust[dustIndex].noLight = true;
						}

						// Add lighting
						if (!tileColors.TryGetValue(tile.type, out Vector3 lightingColor))
						{
							lightingColor = new Vector3(0.5f, 0.5f, 0.5f);
						}

						Lighting.AddLight(pos, lightingColor);
					}
				}
			}
		}
	}
}
