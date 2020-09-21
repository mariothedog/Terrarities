using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrarity
{
	public class BuffModPlayer : ModPlayer
	{
		public bool HasBandOfHoneyRegen;

		public override void ResetEffects()
		{
			HasBandOfHoneyRegen = false;
		}

		public override void OnHitByNPC(NPC npc, int damage, bool crit)
		{
			if (HasBandOfHoneyRegen)
			{
				for (int i = 0; i < Main.rand.Next(2, 4); i++)
				{
					Vector2 beeVelocity = new Vector2(
						Main.rand.Next(-35, 36) * 0.02f,
						Main.rand.Next(-35, 36) * 0.02f
						);
					int beeDamage = Main.rand.Next(28, 32);
					Projectile.NewProjectile(player.position, beeVelocity, ProjectileID.Bee, beeDamage, 0, player.whoAmI);
				}
			}
		}
	}
}
