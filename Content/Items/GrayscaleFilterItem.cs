using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShaderLib.System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterNPCs.Content.Items
{
    internal class GrayscaleFilterItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghost Dye");
            Tooltip.SetDefault("How (How)");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 24;
            item.value = 0;
            item.rare = ItemRarityID.White;
            item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<GrayscaleFilterItem>());
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Restart(Main.UIScaleMatrix, worldDraw: false);

            DrawData data = new DrawData
            {
                position = position - Main.screenPosition,
                scale = new Vector2(scale, scale),
                sourceRect = frame,
                texture = Main.itemTexture[item.type]
            };

            GameShaders.Armor.ApplySecondary(GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<GrayscaleFilterItem>()), Main.player[item.owner], data);
            return true;
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) => spriteBatch.Restart(Main.UIScaleMatrix, false, false);
    }
}