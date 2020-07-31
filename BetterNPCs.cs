using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using ShaderLib;
using BetterNPCs.Content.Items;
using Terraria.Localization;

namespace BetterNPCs
{
    public partial class BetterNPCs : Mod
    {
        public Texture2D[] infoIconTexture = new Texture2D[14];

        public static bool npcChatFocus4;
        public static bool npcChatFocus5;

        internal BetterNPCs Instance { get; private set; }

        public override void Load()
        {
            for (int infoIcons = 0; infoIcons < infoIconTexture.Length; infoIcons++)
            {
                infoIconTexture[infoIcons] = ModContent.GetTexture("Terraria/UI/InfoIcon_" + infoIcons.ToString());
            }

            if (!Main.dedServ)
            {
                Ref<Effect> grayscaleRef = new Ref<Effect>(GetEffect("Effects/GrayscaleShader"));
                GameShaders.Armor.BindShader(ModContent.ItemType<GrayscaleFilterItem>(), new ArmorShaderData(grayscaleRef, "GrayscaleShader"));
            }

            On.Terraria.Main.GUIChatDrawInner += Main_GUIChatDrawInner;
        }

        public static string GetNPCDialog(int l) => Language.GetTextValueWith("LegacyDialog." + l.ToString(), Lang.CreateDialogSubstitutionObject());

        public static LocalizedText GetLegacyInterfaceText(int l) => Language.GetText("LegacyInterface." + l.ToString());
    }
}