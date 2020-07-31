using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterNPCs
{
    public class BetterNPCsGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine stockLine = new TooltipLine(mod, mod.Name, "StockNumber")
            {
                overrideColor = new Color(235, 64, 52)
            };

            TooltipLine taxLine = new TooltipLine(mod, mod.Name, "Tax")
            {
                overrideColor = Color.Red
            };

            if (item.buy)
            {
                int storeValue = Main.HoverItem.GetStoreValue();
                int taxValue = (int)(storeValue * 0.05f); ;
                string text2 = "";
                int num38 = 0;
                int num37 = 0;
                int num36 = 0;
                int num35 = 0;
                int num34 = taxValue * Main.HoverItem.stack;
                if (!Main.HoverItem.buy)
                {
                    num34 = taxValue / 5;
                    if (num34 < 1)
                    {
                        num34 = 1;
                    }
                    num34 *= Main.HoverItem.stack;
                }
                if (num34 < 1)
                {
                    num34 = 1;
                }
                if (num34 >= 1000000)
                {
                    num38 = num34 / 1000000;
                    num34 -= num38 * 1000000;
                }
                if (num34 >= 10000)
                {
                    num37 = num34 / 10000;
                    num34 -= num37 * 10000;
                }
                if (num34 >= 100)
                {
                    num36 = num34 / 100;
                    num34 -= num36 * 100;
                }
                if (num34 >= 1)
                {
                    num35 = num34;
                }
                if (num38 > 0)
                {
                    text2 = text2 + num38.ToString() + " " + BetterNPCs.GetLegacyInterfaceText(15).Value + " ";
                    taxLine.overrideColor = Colors.CoinPlatinum;
                }
                if (num37 > 0)
                {
                    text2 = text2 + num37.ToString() + " " + BetterNPCs.GetLegacyInterfaceText(16).Value + " ";
                    taxLine.overrideColor = Colors.CoinGold;
                }
                if (num36 > 0)
                {
                    text2 = text2 + num36.ToString() + " " + BetterNPCs.GetLegacyInterfaceText(17).Value + " ";
                    taxLine.overrideColor = Colors.CoinSilver;
                }
                if (num35 > 0)
                {
                    text2 = text2 + num35.ToString() + " " + BetterNPCs.GetLegacyInterfaceText(18).Value + " ";
                    taxLine.overrideColor = Colors.CoinCopper;
                }

                tooltips.Add(taxLine);
                taxLine.text = $"Tax: {text2}(5%)";

                tooltips.Add(stockLine);
                stockLine.text = "Stock:";
            }
        }
    }
}