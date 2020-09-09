using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Terrarity.Items.Potions.GreaterPotions
{
	public class GreaterSpelunkerPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Bigger and better version of the original spelunker potion");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 30;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.useAnimation = 17;
			item.useTime = 17;
			item.useTurn = true;
			item.UseSound = SoundID.Item3;
			item.maxStack = 30;
			item.consumable = true;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(silver: 10);
			item.buffType = BuffType<Buffs.GreaterPotions.GreaterSpelunkerBuff>();
			item.buffTime = 5400;
		}
	}
}
