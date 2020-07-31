using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterNPCs
{
    public class BetterNPCsGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            switch (type)
            {
                case NPCID.Guide:
                    shop.item[nextSlot].SetDefaults(ItemID.Book);
                    break;
            }
        }

        public static void NewButtonsClicked(int npcType, bool fifthButton)
        {
            switch (npcType)
            {
                case NPCID.Guide:
                    if (!fifthButton)
                    {
                        SetupNPCShop(npcType);
                    }
                    else
                    {
                        Main.npcChatText = "Sample text";
                    }
                    break;
            }
        }

        public static void SetupNPCShop(int npcType)
        {
            Main.playerInventory = true;
            Main.npcChatText = "";
            Main.npcShop = Main.MaxShopIDs - 1;
            Main.instance.shop[Main.npcShop].SetupShop(npcType);
            Main.PlaySound(SoundID.MenuTick);
        }
    }
}