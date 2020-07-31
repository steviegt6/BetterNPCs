using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetterNPCs
{
    public class BetterNPCsPlayer : ModPlayer
    {
        public bool displayWelcomeText;

        public override void Initialize()
        {
            displayWelcomeText = true;
        }

        public override void Load(TagCompound tag)
        {
            displayWelcomeText = tag.GetBool("displayWelcomeText");
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                { "displayWelcomeText", displayWelcomeText }
            };
        }
    }
}