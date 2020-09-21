using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrarity.Items.Accessories
{
	public class BandOfHoneyRegeneration : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(BuffID.Honey, 1);

			BuffModPlayer buffModPlayer = player.GetModPlayer<BuffModPlayer>();
			buffModPlayer.HasBandOfHoneyRegen = true;
		}
	}
}
