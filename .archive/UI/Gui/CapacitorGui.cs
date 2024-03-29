﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.GameContent;
using TerraEnergy.Content.Capacitors;
using TerraEnergy.Content.Gui.Elements;

namespace TerraEnergy.Content.Gui;

public class CapacitorGui : UIState {
    private CapacitorTE capacitorEntity;
    private ChargingSlot[] chargingSlot;
    private GuiEnergyBar energybar;
    private UIPanel panel;

    public CapacitorGui(Item[] slot, CapacitorTE capacitorEntity) {
        chargingSlot = new ChargingSlot[4];
        for (int i = 0; i < chargingSlot.Length; i++) {
            chargingSlot[i] = new ChargingSlot(new Ref<Item>(slot[i]), TerraEnergy.GetTexture("Texture/ChargingSlotUI").Value, capacitorEntity, capacitorEntity.maxTransferRate);
        }

        this.capacitorEntity = capacitorEntity;
    }

    public override void OnInitialize() {
        panel = new UIPanel();
        panel.Width.Set(400, 0);
        panel.Height.Set(200, 0);
        panel.Top.Set(Main.screenHeight / 2 - 100, 0);
        panel.Left.Set(Main.screenWidth / 2 - 200, 0);

        int space = 40;
        for (int i = 0; i < chargingSlot.Length; i++) {
            ChargingSlot slot = chargingSlot[i];
            slot.Top.Set(25, 0);
            slot.Left.Set(space, 0);
            slot.Width.Set(64, 0);
            slot.Height.Set(64, 0);
            space += 75;
            panel.Append(slot);
        }

        var buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/UI/ButtonDelete");
        UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
        closeButton.Left.Set(400 - 45, 0f);
        closeButton.Width.Set(22, 0f);
        closeButton.Height.Set(22, 0f);
        closeButton.OnClick += new MouseEvent(CloseButtonClicked);
        panel.Append(closeButton);

        energybar = new UIEnergyBar(capacitorEntity.energy);
        energybar.Top.Set(170f, 0);
        energybar.Height.Set(14f, 0);
        energybar.Width.Set(386f, 0);
        panel.Append(energybar);
        Append(panel);
    }

    protected override void DrawChildren(SpriteBatch spriteBatch) {
        base.DrawChildren(spriteBatch);
        Vector2 nameDrawingPosition = new Vector2(Main.screenWidth / 2 - 60, Main.screenHeight / 2 - 95);
        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, capacitorEntity.Name,
            nameDrawingPosition, Color.White, 0f, Vector2.Zero,
            Vector2.One);
    }

    private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
        UIManager.CloseMachineUI();
        Main.playerInventory = false;
    }
}
