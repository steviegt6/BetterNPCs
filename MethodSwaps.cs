using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using static Terraria.Main;

namespace BetterNPCs
{
    public partial class BetterNPCs : Mod
    {
        [Obsolete]
        private void Main_GUIChatDrawInner(On.Terraria.Main.orig_GUIChatDrawInner orig, Terraria.Main self)
        {
            if (player[myPlayer].talkNPC < 0 && player[myPlayer].sign == -1)
            {
                npcChatText = "";
                return;
            }

            if (netMode == NetmodeID.SinglePlayer && autoPause && player[myPlayer].talkNPC >= 0)
            {
                if (npc[player[myPlayer].talkNPC].type == NPCID.BoundGoblin)
                    npc[player[myPlayer].talkNPC].Transform(NPCID.GoblinTinkerer);

                if (npc[player[myPlayer].talkNPC].type == NPCID.BoundWizard)
                    npc[player[myPlayer].talkNPC].Transform(NPCID.Wizard);

                if (npc[player[myPlayer].talkNPC].type == NPCID.BoundMechanic)
                    npc[player[myPlayer].talkNPC].Transform(NPCID.Mechanic);

                if (npc[player[myPlayer].talkNPC].type == NPCID.WebbedStylist)
                    npc[player[myPlayer].talkNPC].Transform(NPCID.Stylist);

                if (npc[player[myPlayer].talkNPC].type == NPCID.SleepingAngler)
                    npc[player[myPlayer].talkNPC].Transform(NPCID.Angler);

                if (npc[player[myPlayer].talkNPC].type == NPCID.BartenderUnconscious)
                    npc[player[myPlayer].talkNPC].Transform(NPCID.DD2Bartender);
            }
            List<List<TextSnippet>> snippets = Utils.WordwrapStringSmart(npcChatText, Color.White, fontMouseText, 460, 10);

            Color color = new Color(200, 200, 200, 200);
            Color textColor2;

            int lineAmount = snippets.Count;

            bool flag3 = InGameUI.CurrentState is UIVirtualKeyboard && PlayerInput.UsingGamepad;

            if (editSign)
            {
                self.textBlinkerCount++;
                if (self.textBlinkerCount >= 20)
                {
                    if (self.textBlinkerState == 0)
                        self.textBlinkerState = 1;
                    else
                        self.textBlinkerState = 0;

                    self.textBlinkerCount = 0;
                }
                if (self.textBlinkerState == 1)
                    snippets[lineAmount - 1].Add(new TextSnippet("|", Color.White));

                instance.DrawWindowsIMEPanel(new Vector2(screenWidth / 2, 90f), 0.5f);
            }

            spriteBatch.Draw(chatBackTexture, new Vector2(screenWidth / 2 - chatBackTexture.Width / 2, 100f), new Rectangle(0, 0, chatBackTexture.Width, (lineAmount + 1) * 30), color, 0f, default, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(chatBackTexture, new Vector2(screenWidth / 2 - chatBackTexture.Width / 2, 100 + (lineAmount + 1) * 30), new Rectangle(0, chatBackTexture.Height - 30, chatBackTexture.Width, 30), color, 0f, default, 1f, SpriteEffects.None, 0f);

            TextSnippet hoveredTextSnippet = null;

            for (int k = 0; k < lineAmount; k++)
            {
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, fontMouseText, snippets[k].ToArray(), new Vector2(170 + (screenWidth - 800) / 2, 120 + k * 30), 0f, Vector2.Zero, Vector2.One, out int hoveredSnippet);
                if (hoveredSnippet > -1)
                    hoveredTextSnippet = snippets[k][hoveredSnippet];
            }

            if (hoveredTextSnippet != null)
            {
                hoveredTextSnippet.OnHover();

                if (mouseLeft && mouseLeftRelease)
                    hoveredTextSnippet.OnClick();
            }
            Rectangle rectangle = new Rectangle(screenWidth / 2 - chatBackTexture.Width / 2, 100, chatBackTexture.Width, (lineAmount + 2) * 30);

            int num31 = 120 + lineAmount * 30 + 30;
            num31 -= 235;

            if (!PlayerInput.UsingGamepad)
                num31 = 9999;

            UIVirtualKeyboard.OffsetDown = num31;

            if (npcChatCornerItem != 0)
            {
                Vector2 position = new Vector2(screenWidth / 2 + chatBackTexture.Width / 2, 100 + (lineAmount + 1) * 30 + 30);
                position -= Vector2.One * 8f;

                Item item = new Item();
                item.netDefaults(npcChatCornerItem);

                float num29 = 1f;

                Texture2D texture2D = itemTexture[item.type];

                if (texture2D.Width > 32 || texture2D.Height > 32)
                    num29 = ((texture2D.Width <= texture2D.Height) ? (32f / (float)texture2D.Height) : (32f / (float)texture2D.Width));

                spriteBatch.Draw(texture2D, position, null, item.GetAlpha(Microsoft.Xna.Framework.Color.White), 0f, new Vector2(texture2D.Width, texture2D.Height), num29, SpriteEffects.None, 0f);

                if (item.color != default)
                    spriteBatch.Draw(texture2D, position, null, item.GetColor(item.color), 0f, new Vector2(texture2D.Width, texture2D.Height), num29, SpriteEffects.None, 0f);

                if (new Rectangle((int)position.X - (int)(texture2D.Width * num29), (int)position.Y - (int)(texture2D.Height * num29), (int)(texture2D.Width * num29), (int)(texture2D.Height * num29)).Contains(new Point(mouseX, mouseY)))
                    self.MouseText(item.Name, -11, 0);
            }

            if (player[myPlayer].talkNPC >= 0)
            {
                Vector2 position = new Vector2(screenWidth / 2 + chatBackTexture.Width / 2, 130);
                position -= Vector2.One * 8f;

                float num29 = 1f;

                Texture2D texture2D = infoIconTexture[7];

                if (texture2D.Width > 32 || texture2D.Height > 32)
                    num29 = ((texture2D.Width <= texture2D.Height) ? (32f / (float)texture2D.Height) : (32f / (float)texture2D.Width));

                spriteBatch.Draw(texture2D, position, null, Color.LightYellow, 0f, new Vector2(texture2D.Width, texture2D.Height), num29, SpriteEffects.None, 0f);

                if (new Rectangle((int)position.X - (int)(texture2D.Width * num29), (int)position.Y - (int)(texture2D.Height * num29), (int)(texture2D.Width * num29), (int)(texture2D.Height * num29)).Contains(new Point(mouseX, mouseY)))
                {
                    string moonPhaseText;

                    if (moonPhase == 0)
                        moonPhaseText = Language.GetTextValue("GameUI.FullMoon");
                    else if (moonPhase == 1)
                        moonPhaseText = Language.GetTextValue("GameUI.WaningGibbous");
                    else if (moonPhase == 2)
                        moonPhaseText = Language.GetTextValue("GameUI.ThirdQuarter");
                    else if (moonPhase == 3)
                        moonPhaseText = Language.GetTextValue("GameUI.WaningCrescent");
                    else if (moonPhase == 4)
                        moonPhaseText = Language.GetTextValue("GameUI.NewMoon");
                    else if (moonPhase == 5)
                        moonPhaseText = Language.GetTextValue("GameUI.WaxingCrescent");
                    else if (moonPhase == 6)
                        moonPhaseText = Language.GetTextValue("GameUI.FirstQuarter");
                    else if (moonPhase == 7)
                        moonPhaseText = Language.GetTextValue("GameUI.WaxingGibbous");
                    else
                        moonPhaseText = "Unknown Phase!";

                    texture2D = infoIconTexture[13];
                    spriteBatch.Draw(texture2D, new Vector2(position.X + 2, position.Y + 2), null, color, 0f, new Vector2(texture2D.Width, texture2D.Height), num29, SpriteEffects.None, 0f);
                    self.MouseText("Moon Phase: " + moonPhaseText);
                }

                position = new Vector2(screenWidth / 2 + chatBackTexture.Width / 2, 150);
                position -= Vector2.One * 8f;

                num29 = 1f;

                texture2D = infoIconTexture[0];

                if (texture2D.Width > 32 || texture2D.Height > 32)
                    num29 = (texture2D.Width <= texture2D.Height) ? (32f / texture2D.Height) : (32f / texture2D.Width);

                spriteBatch.Draw(texture2D, position, null, Color.LightYellow, 0f, new Vector2(texture2D.Width, texture2D.Height), num29, SpriteEffects.None, 0f);

                if (new Rectangle((int)position.X - (int)(texture2D.Width * num29), (int)position.Y - (int)(texture2D.Height * num29), (int)(texture2D.Width * num29), (int)(texture2D.Height * num29)).Contains(new Point(mouseX, mouseY)))
                {
                    string text10 = "AM";

                    double num69 = time;
                    if (!dayTime)
                        num69 += 54000.0;

                    num69 = num69 / 86400.0 * 24.0;

                    double num67 = 7.5;

                    num69 = num69 - num67 - 12.0;

                    if (num69 < 0.0)
                        num69 += 24.0;

                    if (num69 >= 12.0)
                        text10 = "PM";

                    int num65 = (int)num69;

                    double num64 = num69 - (double)num65;

                    num64 = (int)(num64 * 60.0);

                    string text9 = string.Concat(num64);

                    if (num64 < 10.0)
                        text9 = "0" + text9;

                    if (num65 > 12)
                        num65 -= 12;

                    if (num65 == 0)
                        num65 = 12;

                    if (player[myPlayer].accWatch == 1)
                        text9 = "00";
                    else if (player[myPlayer].accWatch == 2)
                        text9 = ((!(num64 < 30.0)) ? "30" : "00");

                    texture2D = infoIconTexture[13];
                    spriteBatch.Draw(texture2D, new Vector2(position.X + 2, position.Y + 2), null, OurFavoriteColor, 0f, new Vector2(texture2D.Width, texture2D.Height), num29, SpriteEffects.None, 0f);
                    self.MouseText("Time: " + num65.ToString() + ":" + text9 + " " + text10);
                }

                if (player[myPlayer].GetModPlayer<BetterNPCsPlayer>().displayWelcomeText)
                {
                    position = new Vector2(screenWidth / 2 + chatBackTexture.Width / 2, 100 + (lineAmount + 1) * 30 + 30);
                    position -= Vector2.One * 8f;

                    Item item = new Item();
                    item.netDefaults(npcChatCornerItem);

                    num29 = 1f;

                    texture2D = itemTexture[ItemID.Book];

                    if (texture2D.Width > 32 || texture2D.Height > 32)
                        num29 = (texture2D.Width <= texture2D.Height) ? (32f / texture2D.Height) : (32f / texture2D.Width);

                    spriteBatch.Draw(texture2D, position, null, item.GetAlpha(Color.White), 0f, new Vector2(texture2D.Width, texture2D.Height), num29, SpriteEffects.None, 0f);
                    if (item.color != default)
                        spriteBatch.Draw(texture2D, position, null, item.GetColor(item.color), 0f, new Vector2(texture2D.Width, texture2D.Height), num29, SpriteEffects.None, 0f);

                    if (new Rectangle((int)position.X - (int)(texture2D.Width * num29), (int)position.Y - (int)(texture2D.Height * num29), (int)(texture2D.Width * num29), (int)(texture2D.Height * num29)).Contains(new Point(mouseX, mouseY)))
                    {
                        self.MouseText("Hey! Thanks for checking out Better NPCs.\nI really appreciate it. Hope you enjoy! :)\n(Click on this book to make this message go away)");

                        if (mouseLeft && mouseLeftRelease)
                            player[myPlayer].GetModPlayer<BetterNPCsPlayer>().displayWelcomeText = false;
                    }
                }
            }

            int num32 = mouseTextColor;
            int num27 = player[myPlayer].statLifeMax2 - player[myPlayer].statLife;

            string focusText3 = "";
            string focusText2 = "";

            textColor2 = new Color(num32, (int)((double)num32 / 1.1), num32 / 2, num32);

            for (int j = 0; j < Player.MaxBuffs; j++)
            {
                int num5 = player[myPlayer].buffType[j];

                if (debuff[num5] && player[myPlayer].buffTime[j] > 5 && BuffLoader.CanBeCleared(num5))
                    num27 += 1000;
            }

            int health = player[myPlayer].statLifeMax2 - player[myPlayer].statLife;

            bool removeDebuffs = true;
            bool canHeal = true;

            string reason = "";

            if (player[myPlayer].sign > -1)
                focusText3 = (!editSign) ? Lang.inter[48].Value : Lang.inter[47].Value;
            else if (npc[player[myPlayer].talkNPC].type == NPCID.Dryad)
            {
                focusText3 = Lang.inter[28].Value;
                focusText2 = Lang.inter[49].Value;
            }
            else if (npc[player[myPlayer].talkNPC].type == NPCID.DyeTrader)
            {
                focusText3 = Lang.inter[28].Value;
                focusText2 = Lang.inter[107].Value;
            }
            else if (npc[player[myPlayer].talkNPC].type == NPCID.SkeletonMerchant)
                focusText3 = Lang.inter[28].Value;
            else if (npc[player[myPlayer].talkNPC].type == NPCID.DD2Bartender)
            {
                focusText3 = Lang.inter[28].Value;
                focusText2 = Language.GetTextValue("UI.BartenderHelp");
            }
            else if (npc[player[myPlayer].talkNPC].type == NPCID.Stylist)
            {
                focusText3 = Lang.inter[28].Value;
                focusText2 = Language.GetTextValue("GameUI.HairStyle");
            }
            else if (npc[player[myPlayer].talkNPC].type == NPCID.TravellingMerchant)
                focusText3 = Lang.inter[28].Value;
            else if (npc[player[myPlayer].talkNPC].type == NPCID.Angler)
                focusText3 = Lang.inter[64].Value;
            else if (npc[player[myPlayer].talkNPC].type == NPCID.Merchant || npc[player[myPlayer].talkNPC].type == NPCID.ArmsDealer || npc[player[myPlayer].talkNPC].type == NPCID.Demolitionist || npc[player[myPlayer].talkNPC].type == NPCID.Clothier || npc[player[myPlayer].talkNPC].type == NPCID.GoblinTinkerer || npc[player[myPlayer].talkNPC].type == NPCID.Wizard || npc[player[myPlayer].talkNPC].type == NPCID.Mechanic || npc[player[myPlayer].talkNPC].type == NPCID.SantaClaus || npc[player[myPlayer].talkNPC].type == NPCID.Truffle || npc[player[myPlayer].talkNPC].type == NPCID.Steampunker || npc[player[myPlayer].talkNPC].type == NPCID.DyeTrader || npc[player[myPlayer].talkNPC].type == NPCID.PartyGirl || npc[player[myPlayer].talkNPC].type == NPCID.Cyborg || npc[player[myPlayer].talkNPC].type == NPCID.Painter || npc[player[myPlayer].talkNPC].type == NPCID.WitchDoctor || npc[player[myPlayer].talkNPC].type == NPCID.Pirate)
            {
                focusText3 = Lang.inter[28].Value;
                if (npc[player[myPlayer].talkNPC].type == NPCID.GoblinTinkerer)
                    focusText2 = Lang.inter[19].Value;
            }
            else if (npc[player[myPlayer].talkNPC].type == NPCID.OldMan)
            {
                if (!dayTime)
                    focusText3 = Lang.inter[50].Value;
            }
            else if (npc[player[myPlayer].talkNPC].type == NPCID.Guide)
            {
                focusText3 = Lang.inter[51].Value;
                focusText2 = Lang.inter[25].Value;
            }
            else if (npc[player[myPlayer].talkNPC].type == NPCID.TaxCollector)
            {
                if (player[myPlayer].taxMoney <= 0)
                    focusText3 = Lang.inter[89].Value;
                else
                {
                    string text5 = "";

                    int num26 = 0;
                    int num25 = 0;
                    int num24 = 0;
                    int num23 = 0;
                    int num22 = player[myPlayer].taxMoney;

                    if (num22 < 0)
                        num22 = 0;

                    num27 = num22;

                    if (num22 >= 1000000)
                    {
                        num26 = num22 / 1000000;
                        num22 -= num26 * 1000000;
                    }

                    if (num22 >= 10000)
                    {
                        num25 = num22 / 10000;
                        num22 -= num25 * 10000;
                    }

                    if (num22 >= 100)
                    {
                        num24 = num22 / 100;
                        num22 -= num24 * 100;
                    }

                    if (num22 >= 1)
                        num23 = num22;

                    if (num26 > 0)
                        text5 = text5 + num26.ToString() + " " + Lang.inter[15].Value + " ";

                    if (num25 > 0)
                        text5 = text5 + num25.ToString() + " " + Lang.inter[16].Value + " ";
                    if (num24 > 0)
                        text5 = text5 + num24.ToString() + " " + Lang.inter[17].Value + " ";

                    if (num23 > 0)
                        text5 = text5 + num23.ToString() + " " + Lang.inter[18].Value + " ";

                    float num21 = (float)(int)mouseTextColor / 255f;

                    if (num26 > 0)
                        textColor2 = new Microsoft.Xna.Framework.Color((byte)(220f * num21), (byte)(220f * num21), (byte)(198f * num21), mouseTextColor);
                    else if (num25 > 0)
                        textColor2 = new Microsoft.Xna.Framework.Color((byte)(224f * num21), (byte)(201f * num21), (byte)(92f * num21), mouseTextColor);
                    else if (num24 > 0)
                        textColor2 = new Microsoft.Xna.Framework.Color((byte)(181f * num21), (byte)(192f * num21), (byte)(193f * num21), mouseTextColor);
                    else if (num23 > 0)
                        textColor2 = new Microsoft.Xna.Framework.Color((byte)(246f * num21), (byte)(138f * num21), (byte)(96f * num21), mouseTextColor);

                    if (text5 == "")
                        focusText3 = Lang.inter[89].Value;
                    else
                    {
                        text5 = text5.Substring(0, text5.Length - 1);
                        focusText3 = Lang.inter[89].Value + " (" + text5 + ")";
                    }
                }
            }
            else if (npc[player[myPlayer].talkNPC].type == NPCID.Nurse)
            {
                string text3 = "";

                int num20 = 0;
                int num19 = 0;
                int num18 = 0;
                int num17 = 0;
                int num16 = num27;

                if (num16 > 0)
                {
                    num16 = (int)((double)num16 * 0.75);
                    if (num16 < 1)
                        num16 = 1;
                }
                reason = Language.GetTextValue("tModLoader.DefaultNurseCantHealChat");
                canHeal = PlayerHooks.ModifyNurseHeal(player[myPlayer], npc[player[myPlayer].talkNPC], ref health, ref removeDebuffs, ref reason);

                PlayerHooks.ModifyNursePrice(player[myPlayer], npc[player[myPlayer].talkNPC], health, removeDebuffs, ref num16);

                if (num16 < 0)
                    num16 = 0;

                num27 = num16;

                if (num16 >= 1000000)
                {
                    num20 = num16 / 1000000;
                    num16 -= num20 * 1000000;
                }
                if (num16 >= 10000)
                {
                    num19 = num16 / 10000;
                    num16 -= num19 * 10000;
                }
                if (num16 >= 100)
                {
                    num18 = num16 / 100;
                    num16 -= num18 * 100;
                }
                if (num16 >= 1)
                    num17 = num16;

                if (num20 > 0)
                    text3 = text3 + num20.ToString() + " " + Lang.inter[15].Value + " ";

                if (num19 > 0)
                    text3 = text3 + num19.ToString() + " " + Lang.inter[16].Value + " ";

                if (num18 > 0)
                    text3 = text3 + num18.ToString() + " " + Lang.inter[17].Value + " ";

                if (num17 > 0)
                    text3 = text3 + num17.ToString() + " " + Lang.inter[18].Value + " ";

                float num15 = (float)(int)mouseTextColor / 255f;

                if (num20 > 0)
                    textColor2 = new Microsoft.Xna.Framework.Color((byte)(220f * num15), (byte)(220f * num15), (byte)(198f * num15), mouseTextColor);
                else if (num19 > 0)
                    textColor2 = new Microsoft.Xna.Framework.Color((byte)(224f * num15), (byte)(201f * num15), (byte)(92f * num15), mouseTextColor);
                else if (num18 > 0)
                    textColor2 = new Microsoft.Xna.Framework.Color((byte)(181f * num15), (byte)(192f * num15), (byte)(193f * num15), mouseTextColor);
                else if (num17 > 0)
                    textColor2 = new Microsoft.Xna.Framework.Color((byte)(246f * num15), (byte)(138f * num15), (byte)(96f * num15), mouseTextColor);

                if (text3 == "")
                    focusText3 = Lang.inter[54].Value;
                else
                {
                    text3 = text3.Substring(0, text3.Length - 1);
                    focusText3 = Lang.inter[54].Value + " (" + text3 + ")";
                }
            }

            NPCLoader.SetChatButtons(ref focusText3, ref focusText2);

            if (!flag3)
                DrawNPCChatButtons(num32, textColor2, lineAmount, focusText3, focusText2);

            if (PlayerInput.IgnoreMouseInterface)
                return;

            if (rectangle.Contains(new Microsoft.Xna.Framework.Point(mouseX, mouseY)))
                player[myPlayer].mouseInterface = true;

            if (!mouseLeft || !mouseLeftRelease || !rectangle.Contains(new Microsoft.Xna.Framework.Point(mouseX, mouseY)))
                return;

            mouseLeftRelease = false;
            player[myPlayer].releaseUseItem = false;
            player[myPlayer].mouseInterface = true;

            if (npcChatFocus1)
                CloseNPCChatOrSign();
            else if (npcChatFocus2)
            {
                if (player[myPlayer].sign != -1)
                {
                    if (editSign)
                        SubmitSignText();
                    else
                        IngameFancyUI.OpenVirtualKeyboard(1);
                }

                if (!NPCLoader.PreChatButtonClicked(firstButton: true))
                    return;

                NPCLoader.OnChatButtonClicked(firstButton: true);

                if (npc[player[myPlayer].talkNPC].type == NPCID.Angler)
                {
                    npcChatCornerItem = 0;
                    PlaySound(12);
                    bool flag2 = false;

                    if (!anglerQuestFinished && !anglerWhoFinishedToday.Contains(player[myPlayer].name))
                    {
                        int num14 = player[myPlayer].FindItem(anglerQuestItemNetIDs[anglerQuest]);
                        if (num14 != -1)
                        {
                            player[myPlayer].inventory[num14].stack--;

                            if (player[myPlayer].inventory[num14].stack <= 0)
                                player[myPlayer].inventory[num14] = new Item();

                            flag2 = true;
                            PlaySound(24);
                            player[myPlayer].anglerQuestsFinished++;
                            player[myPlayer].GetAnglerReward();
                        }
                    }
                    npcChatText = Lang.AnglerQuestChat(flag2);

                    if (flag2)
                    {
                        anglerQuestFinished = true;

                        if (netMode == NetmodeID.MultiplayerClient)
                            NetMessage.SendData(MessageID.AnglerQuestFinished);
                        else
                            anglerWhoFinishedToday.Add(player[myPlayer].name);

                        AchievementsHelper.HandleAnglerService();
                    }
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Merchant)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 1;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.ArmsDealer)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 2;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Mechanic)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 8;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.SantaClaus)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 9;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Stylist)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 18;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.TravellingMerchant)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 19;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.SkeletonMerchant)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 20;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.OldMan)
                {
                    if (netMode == NetmodeID.SinglePlayer)
                        NPC.SpawnSkeletron();
                    else
                        NetMessage.SendData(MessageID.Assorted1, -1, -1, null, myPlayer, 1f);

                    npcChatText = "";
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Dryad)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 3;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Demolitionist)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 4;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Clothier)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 5;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.GoblinTinkerer)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 6;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Wizard)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 7;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Truffle)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 10;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Steampunker)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 11;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.DyeTrader)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 12;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.PartyGirl)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 13;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Cyborg)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 14;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Painter)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 15;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.WitchDoctor)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 16;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Pirate)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 17;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Guide)
                {
                    PlaySound(12);
                    HelpText();
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.TaxCollector)
                {
                    if (player[myPlayer].taxMoney > 0)
                    {
                        int num13 = player[myPlayer].taxMoney;
                        while (num13 > 0)
                        {
                            if (num13 > 1000000)
                            {
                                int num12 = num13 / 1000000;
                                num13 -= 1000000 * num12;

                                int number7 = Item.NewItem((int)player[myPlayer].position.X, (int)player[myPlayer].position.Y, player[myPlayer].width, player[myPlayer].height, 74, num12);

                                if (netMode == NetmodeID.MultiplayerClient)
                                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number7, 1f);

                                continue;
                            }

                            if (num13 > 10000)
                            {
                                int num11 = num13 / 10000;
                                num13 -= 10000 * num11;

                                int number6 = Item.NewItem((int)player[myPlayer].position.X, (int)player[myPlayer].position.Y, player[myPlayer].width, player[myPlayer].height, 73, num11);

                                if (netMode == NetmodeID.MultiplayerClient)
                                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number6, 1f);

                                continue;
                            }

                            if (num13 > 100)
                            {
                                int num10 = num13 / 100;
                                num13 -= 100 * num10;

                                int number5 = Item.NewItem((int)player[myPlayer].position.X, (int)player[myPlayer].position.Y, player[myPlayer].width, player[myPlayer].height, 72, num10);

                                if (netMode == NetmodeID.MultiplayerClient)
                                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number5, 1f);

                                continue;
                            }

                            int num9 = num13;

                            if (num9 < 1)
                                num9 = 1;

                            num13 -= num9
                                ;
                            int number4 = Item.NewItem((int)player[myPlayer].position.X, (int)player[myPlayer].position.Y, player[myPlayer].width, player[myPlayer].height, 71, num9);

                            if (netMode == NetmodeID.MultiplayerClient)
                                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number4, 1f);
                        }
                        npcChatText = Lang.dialog(rand.Next(380, 382));
                        player[myPlayer].taxMoney = 0;
                    }
                    else
                    {
                        npcChatText = Lang.dialog(rand.Next(390, 401));
                    }
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Nurse)
                {
                    PlaySound(12);
                    if (num27 > 0)
                    {
                        if (!canHeal)
                        {
                            npcChatText = reason;
                        }
                        else if (player[myPlayer].BuyItem(num27))
                        {
                            AchievementsHelper.HandleNurseService(num27);
                            PlaySound(SoundID.Item4);
                            player[myPlayer].HealEffect(health);
                            if ((double)player[myPlayer].statLife < (double)player[myPlayer].statLifeMax2 * 0.25)
                            {
                                npcChatText = Lang.dialog(227);
                            }
                            else if ((double)player[myPlayer].statLife < (double)player[myPlayer].statLifeMax2 * 0.5)
                            {
                                npcChatText = Lang.dialog(228);
                            }
                            else if ((double)player[myPlayer].statLife < (double)player[myPlayer].statLifeMax2 * 0.75)
                            {
                                npcChatText = Lang.dialog(229);
                            }
                            else
                            {
                                npcChatText = Lang.dialog(230);
                            }
                            player[myPlayer].statLife += health;
                            if (removeDebuffs)
                            {
                                for (int i = 0; i < Player.MaxBuffs; i++)
                                {
                                    int num8 = player[myPlayer].buffType[i];
                                    if (debuff[num8] && player[myPlayer].buffTime[i] > 0 && BuffLoader.CanBeCleared(num8))
                                    {
                                        player[myPlayer].DelBuff(i);
                                        i = -1;
                                    }
                                }
                            }
                            PlayerHooks.PostNurseHeal(player[myPlayer], npc[player[myPlayer].talkNPC], health, removeDebuffs, num27);
                        }
                        else
                        {
                            int num33 = rand.Next(3);
                            if (num33 == 0)
                            {
                                npcChatText = Lang.dialog(52);
                            }
                            if (num33 == 1)
                            {
                                npcChatText = Lang.dialog(53);
                            }
                            if (num33 == 2)
                            {
                                npcChatText = Lang.dialog(54);
                            }
                        }
                    }
                    else
                    {
                        int num7 = rand.Next(3);
                        if (!ChildSafety.Disabled)
                        {
                            num7 = rand.Next(1, 3);
                        }
                        switch (num7)
                        {
                            case 0:
                                npcChatText = Lang.dialog(55);
                                break;

                            case 1:
                                npcChatText = Lang.dialog(56);
                                break;

                            case 2:
                                npcChatText = Lang.dialog(57);
                                break;
                        }
                    }
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.DD2Bartender)
                {
                    playerInventory = true;
                    npcChatText = "";
                    npcShop = 21;
                    self.shop[npcShop].SetupShop(npcShop);
                    PlaySound(12);
                }
            }
            else
            {
                if (!npcChatFocus3 || player[myPlayer].talkNPC < 0 || !NPCLoader.PreChatButtonClicked(firstButton: false))
                {
                    return;
                }
                NPCLoader.OnChatButtonClicked(firstButton: false);
                if (npc[player[myPlayer].talkNPC].type == NPCID.Dryad)
                {
                    PlaySound(12);
                    npcChatText = Lang.GetDryadWorldStatusDialog();
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Guide)
                {
                    playerInventory = true;
                    npcChatText = "";
                    PlaySound(12);
                    InGuideCraftMenu = true;
                    UILinkPointNavigator.GoToDefaultPage();
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.GoblinTinkerer)
                {
                    playerInventory = true;
                    npcChatText = "";
                    PlaySound(12);
                    InReforgeMenu = true;
                    UILinkPointNavigator.GoToDefaultPage();
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.Stylist)
                {
                    OpenHairWindow();
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.DyeTrader)
                {
                    npcChatCornerItem = 0;
                    PlaySound(12);
                    bool gotDye = false;
                    int num6 = player[myPlayer].FindItem(ItemID.Sets.ExoticPlantsForDyeTrade);
                    if (num6 != -1)
                    {
                        player[myPlayer].inventory[num6].stack--;
                        if (player[myPlayer].inventory[num6].stack <= 0)
                        {
                            player[myPlayer].inventory[num6] = new Item();
                        }
                        gotDye = true;
                        PlaySound(24);
                        player[myPlayer].GetDyeTraderReward();
                    }
                    npcChatText = Lang.DyeTraderQuestChat(gotDye);
                }
                else if (npc[player[myPlayer].talkNPC].type == NPCID.DD2Bartender)
                {
                    PlaySound(12);
                    HelpText();
                    npcChatText = Lang.BartenderHelpText(npc[player[myPlayer].talkNPC]);
                }
            }
        }

        [Obsolete]
        private static void DrawNPCChatButtons(int superColor, Color chatColor, int numLines, string focusText, string focusText3)
        {
            int num = 180 + (screenWidth - 800) / 2;

            float y = 130 + numLines * 30;

            string text3 = focusText;

            DynamicSpriteFont font3 = fontMouseText;

            Vector2 vec = new Vector2(mouseX, mouseY);
            Vector2 vector10 = new Vector2(num, y);
            Vector2 vector9 = vector10;
            Vector2 vector8 = new Vector2(0.9f);
            Vector2 stringSize3 = ChatManager.GetStringSize(font3, text3, vector8);
            Vector2 value = new Vector2(1f);

            Color baseColor3 = chatColor;

            Player player = Main.player[myPlayer];

            if (stringSize3.X > 260f)
                value.X *= 260f / stringSize3.X;

            if (vec.Between(vector9, vector9 + stringSize3 * vector8 * value.X) && !PlayerInput.IgnoreMouseInterface)
            {
                player.mouseInterface = true;
                player.releaseUseItem = false;

                vector8 *= 1.1f;

                if (!npcChatFocus2)
                    PlaySound(12);

                npcChatFocus2 = true;
            }
            else
            {
                if (npcChatFocus2)
                    PlaySound(12);

                npcChatFocus2 = false;
            }

            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font3, text3, vector9 + stringSize3 * value * 0.5f, baseColor3, 0f, stringSize3 * 0.5f, vector8 * value);

            if (text3.Length > 0)
            {
                UILinkPointNavigator.SetPosition(2500, vector9 + stringSize3 * 0.5f);
                UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsLeft = true;
            }

            Vector2 vector7 = new Vector2((float)num + stringSize3.X * value.X + 30f, y);

            text3 = Lang.inter[52].Value;
            font3 = fontMouseText;
            vector9 = vector7;
            vector8 = new Vector2(0.9f);
            stringSize3 = ChatManager.GetStringSize(font3, text3, vector8);
            baseColor3 = new Color(superColor, (int)(superColor / 1.1), superColor / 2, superColor);
            value = new Vector2(1f);

            if (vec.Between(vector9, vector9 + stringSize3 * vector8 * value.X) && !PlayerInput.IgnoreMouseInterface)
            {
                player.mouseInterface = true;
                player.releaseUseItem = false;

                vector8 *= 1.1f;

                if (!npcChatFocus1)
                    PlaySound(12);

                npcChatFocus1 = true;
            }
            else
            {
                if (npcChatFocus1)
                    PlaySound(12);

                npcChatFocus1 = false;
            }

            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font3, text3, vector9 + stringSize3 * value * 0.5f, baseColor3, 0f, stringSize3 * 0.5f, vector8 * value);

            if (text3.Length > 0)
            {
                UILinkPointNavigator.SetPosition(2501, vector9 + stringSize3 * 0.5f);
                UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsMiddle = true;
            }

            if (string.IsNullOrWhiteSpace(focusText3))
                return;
            Vector2 vector11 = new Vector2(vector7.X + stringSize3.X * value.X + 30f, y);

            text3 = focusText3;
            font3 = fontMouseText;
            vector9 = vector11;
            vector8 = new Vector2(0.9f);
            stringSize3 = ChatManager.GetStringSize(font3, text3, vector8);
            baseColor3 = chatColor;
            value = new Vector2(1f);

            if (vec.Between(vector9, vector9 + stringSize3 * vector8 * value.X) && !PlayerInput.IgnoreMouseInterface)
            {
                player.mouseInterface = true;
                player.releaseUseItem = false;

                vector8 *= 1.1f;

                if (!npcChatFocus3)
                    PlaySound(12);

                npcChatFocus3 = true;
            }
            else
            {
                if (npcChatFocus3)
                    PlaySound(12);

                npcChatFocus3 = false;
            }

            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font3, text3, vector9 + stringSize3 * value * 0.5f, baseColor3, 0f, stringSize3 * 0.5f, vector8 * value);
            UILinkPointNavigator.SetPosition(2502, vector9 + stringSize3 * 0.5f);
            UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight = true;
        }

        [Obsolete]
        private static void HelpText()
        {
            bool flag35 = false;

            if (player[myPlayer].statLifeMax > 100)
                flag35 = true;

            bool flag34 = false;

            if (player[myPlayer].statManaMax > 0)
                flag34 = true;

            bool flag33 = true;
            bool flag32 = false;
            bool flag31 = false;
            bool flag30 = false;
            bool flag29 = false;
            bool flag28 = false;
            bool flag27 = false;

            for (int j = 0; j < 58; j++)
            {
                if (player[myPlayer].inventory[j].pick > 0 && player[myPlayer].inventory[j].Name != "Copper Pickaxe")
                    flag33 = false;

                if (player[myPlayer].inventory[j].axe > 0 && player[myPlayer].inventory[j].Name != "Copper Axe")
                    flag33 = false;

                if (player[myPlayer].inventory[j].hammer > 0)
                    flag33 = false;

                if (player[myPlayer].inventory[j].type == ItemID.IronOre || player[myPlayer].inventory[j].type == ItemID.CopperOre || player[myPlayer].inventory[j].type == ItemID.GoldOre || player[myPlayer].inventory[j].type == ItemID.SilverOre)
                    flag32 = true;

                if (player[myPlayer].inventory[j].type == ItemID.GoldBar || player[myPlayer].inventory[j].type == ItemID.CopperBar || player[myPlayer].inventory[j].type == ItemID.SilverBar || player[myPlayer].inventory[j].type == ItemID.IronBar)

                    flag31 = true;

                if (player[myPlayer].inventory[j].type == ItemID.FallenStar)

                    flag30 = true;

                if (player[myPlayer].inventory[j].type == ItemID.Lens)

                    flag29 = true;

                if (player[myPlayer].inventory[j].type == ItemID.RottenChunk || player[myPlayer].inventory[j].type == ItemID.WormFood || player[myPlayer].inventory[j].type == ItemID.Vertebrae)
                    flag28 = true;

                if (player[myPlayer].inventory[j].type == ItemID.GrapplingHook)
                    flag27 = true;
            }

            bool flag26 = false;
            bool flag25 = false;
            bool flag24 = false;
            bool flag23 = false;
            bool flag22 = false;
            bool flag21 = false;
            bool flag20 = false;
            bool flag19 = false;
            bool flag18 = false;

            for (int i = 0; i < 200; i++)
            {
                if (npc[i].active)
                {
                    if (npc[i].type == NPCID.Merchant)
                        flag26 = true;

                    if (npc[i].type == NPCID.Nurse)
                        flag25 = true;

                    if (npc[i].type == NPCID.ArmsDealer)
                        flag23 = true;

                    if (npc[i].type == NPCID.Dryad)
                        flag24 = true;

                    if (npc[i].type == NPCID.Clothier)
                        flag18 = true;

                    if (npc[i].type == NPCID.Mechanic)
                        flag21 = true;

                    if (npc[i].type == NPCID.Demolitionist)
                        flag22 = true;

                    if (npc[i].type == NPCID.Wizard)
                        flag20 = true;

                    if (npc[i].type == NPCID.GoblinTinkerer)
                        flag19 = true;
                }
            }

            object obj = Lang.CreateDialogSubstitutionObject();

            while (true)
            {
                helpText++;

                if (Language.Exists("GuideHelpText.Help_" + helpText.ToString()))
                {
                    LocalizedText text = Language.GetText("GuideHelpText.Help_" + helpText.ToString());

                    if (text.CanFormatWith(obj))
                    {
                        npcChatText = text.FormatWith(obj);
                        return;
                    }
                }

                if (flag33)
                {
                    if (helpText == 1)
                    {
                        npcChatText = Lang.dialog(177);
                        return;
                    }

                    if (helpText == 2)
                    {
                        npcChatText = Lang.dialog(178);
                        return;
                    }

                    if (helpText == 3)
                    {
                        npcChatText = Lang.dialog(179);
                        return;
                    }

                    if (helpText == 4)
                    {
                        npcChatText = Lang.dialog(180);
                        return;
                    }

                    if (helpText == 5)
                    {
                        npcChatText = Lang.dialog(181);
                        return;
                    }

                    if (helpText == 6)
                    {
                        npcChatText = Lang.dialog(182);
                        return;
                    }
                }

                if (flag33 && !flag32 && !flag31 && helpText == 11)
                {
                    npcChatText = Lang.dialog(183);
                    return;
                }

                if (flag33 && flag32 && !flag31)
                {
                    if (helpText == 21)
                    {
                        npcChatText = Lang.dialog(184);
                        return;
                    }

                    if (helpText == 22)
                    {
                        npcChatText = Lang.dialog(185);
                        return;
                    }
                }

                if (flag33 && flag31)
                {
                    if (helpText == 31)
                    {
                        npcChatText = Lang.dialog(186);
                        return;
                    }
                    if (helpText == 32)
                    {
                        npcChatText = Lang.dialog(187);
                        return;
                    }
                }

                if (!flag35 && helpText == 41)
                {
                    npcChatText = Lang.dialog(188);
                    return;
                }

                if (!flag34 && helpText == 42)
                {
                    npcChatText = Lang.dialog(189);
                    return;
                }

                if (!flag34 && !flag30 && helpText == 43)
                {
                    npcChatText = Lang.dialog(190);
                    return;
                }

                if (!flag26 && !flag25)
                {
                    if (helpText == 51)
                    {
                        npcChatText = Lang.dialog(191);
                        return;
                    }

                    if (helpText == 52)
                    {
                        npcChatText = Lang.dialog(192);
                        return;
                    }

                    if (helpText == 53)
                    {
                        npcChatText = Lang.dialog(193);
                        return;
                    }

                    if (helpText == 54)
                    {
                        npcChatText = Lang.dialog(194);
                        return;
                    }
                }

                if (!flag26 && helpText == 61)
                {
                    npcChatText = Lang.dialog(195);
                    return;
                }

                if (!flag25 && helpText == 62)
                {
                    npcChatText = Lang.dialog(196);
                    return;
                }

                if (!flag23 && helpText == 63)
                {
                    npcChatText = Lang.dialog(197);
                    return;
                }

                if (!flag24 && helpText == 64)
                {
                    npcChatText = Lang.dialog(198);
                    return;
                }

                if (!flag21 && helpText == 65 && NPC.downedBoss3)
                {
                    npcChatText = Lang.dialog(199);
                    return;
                }

                if (!flag18 && helpText == 66 && NPC.downedBoss3)
                {
                    npcChatText = Lang.dialog(200);
                    return;
                }

                if (!flag22 && helpText == 67)
                {
                    npcChatText = Lang.dialog(201);
                    return;
                }

                if (!flag19 && NPC.downedBoss2 && helpText == 68)
                {
                    npcChatText = Lang.dialog(202);
                    return;
                }

                if (!flag20 && hardMode && helpText == 69)
                {
                    npcChatText = Lang.dialog(203);
                    return;
                }

                if (flag29 && helpText == 71)
                {
                    npcChatText = Lang.dialog(204);
                    return;
                }

                if (flag28 && helpText == 72)
                {
                    npcChatText = Lang.dialog(WorldGen.crimson ? 403 : 205);
                    return;
                }

                if ((flag29 | flag28) && helpText == 80)
                {
                    npcChatText = Lang.dialog(WorldGen.crimson ? 402 : 206);
                    return;
                }

                if (!flag27 && helpText == 201 && !hardMode && !NPC.downedBoss3 && !NPC.downedBoss2)
                {
                    npcChatText = Lang.dialog(207);
                    return;
                }

                if (helpText == 1000 && !NPC.downedBoss1 && !NPC.downedBoss2)
                {
                    npcChatText = Lang.dialog(208);
                    return;
                }

                if (helpText == 1001 && !NPC.downedBoss1 && !NPC.downedBoss2)
                {
                    npcChatText = Lang.dialog(209);
                    return;
                }

                if (helpText == 1002 && !NPC.downedBoss2)
                {
                    if (WorldGen.crimson)
                        npcChatText = Lang.dialog(331);
                    else
                        npcChatText = Lang.dialog(210);

                    return;
                }

                if (helpText == 1050 && !NPC.downedBoss1 && player[myPlayer].statLifeMax < 200)
                {
                    npcChatText = Lang.dialog(211);
                    return;
                }

                if (helpText == 1051 && !NPC.downedBoss1 && player[myPlayer].statDefense <= 10)
                {
                    npcChatText = Lang.dialog(212);
                    return;
                }

                if (helpText == 1052 && !NPC.downedBoss1 && player[myPlayer].statLifeMax >= 200 && player[myPlayer].statDefense > 10)
                {
                    npcChatText = Lang.dialog(213);
                    return;
                }

                if (helpText == 1053 && NPC.downedBoss1 && !NPC.downedBoss2 && player[myPlayer].statLifeMax < 300)
                {
                    npcChatText = Lang.dialog(214);
                    return;
                }

                if (helpText == 1054 && NPC.downedBoss1 && !NPC.downedBoss2 && player[myPlayer].statLifeMax >= 300)
                {
                    npcChatText = Lang.dialog(215);
                    return;
                }

                if (helpText == 1055 && NPC.downedBoss1 && !NPC.downedBoss2 && player[myPlayer].statLifeMax >= 300)
                {
                    npcChatText = Lang.dialog(216);
                    return;
                }

                if (helpText == 1056 && NPC.downedBoss1 && NPC.downedBoss2 && !NPC.downedBoss3)
                {
                    npcChatText = Lang.dialog(217);
                    return;
                }

                if (helpText == 1057 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !hardMode && player[myPlayer].statLifeMax < 400)
                {
                    npcChatText = Lang.dialog(218);
                    return;
                }

                if (helpText == 1058 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !hardMode && player[myPlayer].statLifeMax >= 400)
                {
                    npcChatText = Lang.dialog(219);
                    return;
                }

                if (helpText == 1059 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !hardMode && player[myPlayer].statLifeMax >= 400)
                {
                    npcChatText = Lang.dialog(220);
                    return;
                }

                if (helpText == 1060 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !hardMode && player[myPlayer].statLifeMax >= 400)
                {
                    npcChatText = Lang.dialog(221);
                    return;
                }

                if (helpText == 1061 && hardMode)
                {
                    npcChatText = Lang.dialog(WorldGen.crimson ? 401 : 222);
                    return;
                }

                if (helpText == 1062 && hardMode)
                    break;

                if (helpText > 1100)
                    helpText = 0;
            }

            npcChatText = Lang.dialog(223);
        }
    }
}