using Terraria.ModLoader;

namespace BetterNPCs
{
	public partial class BetterNPCs : Mod
	{
        public override void Load()
        {
            On.Terraria.Main.GUIChatDrawInner += Main_GUIChatDrawInner;
        }
    }
}