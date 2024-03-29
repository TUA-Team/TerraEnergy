﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerraEnergy.EnergyAPI;
using TerraEnergy.UI.Elements;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using TUA.Utilities;

namespace TerraEnergy.UI {
    public class FurnaceUI : UIState
    {
        public CustomizableUIPanel furnaceUI;
        public CustomizableUIPanel upgradeUI;
        public static bool visible = false;

        private readonly InputOutputSlot _input;
        private readonly InputOutputSlot _output;
        private readonly FuelSlot _fuel;
        private readonly UIEnergyBar _energyBar;
        private string _furnaceName = "";

        private UIElement xButton;
        private Asset<Texture2D> xButtonTexture;

        public Item InputItem => _input.item;

        public FurnaceUI(Ref<Item> input, Ref<Item> output, EnergyCore energyCore, string furnaceName)
        {
            this._input = new InputOutputSlot(input, TextureAssets.InventoryBack10.Value);
            this._output = new InputOutputSlot(output, TextureAssets.InventoryBack10.Value);
            this._furnaceName = furnaceName;

            _energyBar = new UIEnergyBar(energyCore);
        }

        public FurnaceUI(Ref<Item> input, Ref<Item> output, Ref<Item> fuel, FuelCore energyCore, string furnaceName)
        {
            this._input = new InputOutputSlot(input, TextureAssets.InventoryBack10.Value);
            this._output = new InputOutputSlot(output, TextureAssets.InventoryBack10.Value);

            if (energyCore != null)
            {
                _energyBar = new UIEnergyBar(energyCore);
            }

            this._fuel = new FuelSlot(fuel, TextureAssets.InventoryBack10.Value, energyCore);
            this._furnaceName = furnaceName;
        }

        public override void OnInitialize()
        {
            xButtonTexture = TerraEnergy.GetTexture("Texture/X_ui");

            xButton = new UIElement();
            xButton.Width.Set(20f, 0f);
            xButton.Height.Set(22f, 0f);
            xButton.Left.Set(Main.screenWidth / 2f + 170f, 0f);
            xButton.Top.Set(Main.screenHeight / 2f - 90f, 0f);
            xButton.OnClick += CloseButtonClicked;

            furnaceUI = new CustomizableUIPanel(TerraEnergy.GetTexture("Texture/UI/panel").Value);
            furnaceUI.SetPadding(0);
            furnaceUI.Width.Set(400, 0f);
            furnaceUI.Height.Set(200, 0f);
            furnaceUI.Top.Set(Main.screenHeight / 2 - 100, 0f);
            furnaceUI.Left.Set(Main.screenWidth / 2 - 200, 0f);

            upgradeUI = new CustomizableUIPanel(TerraEnergy.GetTexture("Texture/UI/panel").Value);
            upgradeUI.SetPadding(0);
            upgradeUI.Width.Set(200, 0f);
            upgradeUI.Height.Set(150, 0f);
            upgradeUI.Top.Set(Main.screenHeight / 2 - 100, 0f);
            upgradeUI.Left.Set(Main.screenWidth / 2 + 215, 0f);

            //furnaceUI.BackgroundColor = new Color(73, 94, 171);
            _output.Top.Set(60, 0f);
            _output.Left.Set(300, 0f);
            
            if (_fuel != null)
            {
                _input.Top.Set(30, 0f);
                _input.Left.Set(50, 0f);

                _fuel.Top.Set(90, 0f);
                _fuel.Left.Set(50, 0f);
                furnaceUI.Append(_fuel);
            }
            else
            {
                _input.Top.Set(60, 0f);
                _input.Left.Set(50, 0f);
            }


            furnaceUI.Append(_input);
            furnaceUI.Append(_output);

            
            _energyBar.Top.Set(180f, 0);
            _energyBar.Left.Set(10f, 0);
            _energyBar.Height.Set(14f, 0);
            _energyBar.Width.Set(386f, 0);
            furnaceUI.Append(_energyBar);
            
            Append(furnaceUI);
            Append(upgradeUI);
            Append(xButton);
        }


        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            UIManager.CloseMachineUI();
            Main.playerInventory = false;
        }

        protected override void DrawChildren(SpriteBatch spriteBatch)
        {
            base.DrawChildren(spriteBatch);
            Vector2 nameDrawingPosition = new Vector2(Main.screenWidth / 2 - 60, Main.screenHeight / 2 - 95);
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, _furnaceName,
                nameDrawingPosition, Color.White * 0.7f, 0f, Vector2.Zero,
                Vector2.One);
            upgradeUI.isVisible = false;
            furnaceUI.isVisible = true;
            if (_furnaceName.Equals("Adamantite Forge") || _furnaceName.Equals("Titanium Forge"))
            {

                upgradeUI.isVisible = true;
                Vector2 UpgradeDrawingPosition = new Vector2(Main.screenWidth / 2 + 230, Main.screenHeight / 2 - 95);
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, "Upgrade",
                    UpgradeDrawingPosition, Color.White * 0.7f, 0f, Vector2.Zero,
                    Vector2.One);
            }
            spriteBatch.Draw(xButtonTexture.Value, xButton.GetInnerDimensions().Position(), Color.White * 0.7f);
        }
    }
}
