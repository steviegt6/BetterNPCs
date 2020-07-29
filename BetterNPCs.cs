using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ModLoader;

namespace BetterNPCs
{
    public partial class BetterNPCs : Mod
    {
        public Texture2D[] infoIconTexture = new Texture2D[14];

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void Load()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            for (int infoIcons = 0; infoIcons < infoIconTexture.Length; infoIcons++)
                infoIconTexture[infoIcons] = ModContent.GetTexture("Terraria/UI/InfoIcon_" + infoIcons.ToString());

            //TODO: Convert all obsolete methods to non-obsolete ones.
            On.Terraria.Main.GUIChatDrawInner += Main_GUIChatDrawInner;

            base.Load();
        }
    }
}