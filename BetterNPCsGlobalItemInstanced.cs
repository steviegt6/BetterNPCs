using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ShaderLib.System;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using BetterNPCs.Content.Items;

namespace BetterNPCs
{
    public class BetterNPCsGlobalItemInstanced : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public bool outOfStock;

        /*public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (item.type == ModContent.ItemType<GrayscaleFilterItem>())
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
            }

            return true;
        }*/

        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) => spriteBatch.Restart(Main.UIScaleMatrix, false, false);
    }
}