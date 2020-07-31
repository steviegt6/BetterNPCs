using Terraria;
using Terraria.ModLoader;

namespace BetterNPCs
{
    public class NPCHappinessSystem : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public float npcHappinessLevel;

        public bool npcHappinessHates;
        public bool npcHappinessDislikes;
        public bool npcHappinessNeutral;
        public bool npcHappinessLikes;
        public bool npcHappinessLoves;

        public override void SetDefaults(NPC npc)
        {
            switch (npc.type)
            {
                case Terraria.ID.NPCID.Guide:
                    npcHappinessLevel = 1f;
                    break;

                default:
                    if (npc.townNPC)
                        npcHappinessLevel = Main.rand.Next(0, 24);
                    else
                        npcHappinessLevel = 0f;
                    break;
            }
        }
    }
}