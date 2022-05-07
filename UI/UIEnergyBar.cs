﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerraEnergy.EnergyAPI;
using TerraEnergy.UI.Elements;
using Terraria;
using Terraria.UI;

namespace TerraEnergy.UI {
    public class UIEnergyBar : UIElement
    {

        private Asset<Texture2D> energyBar;
        private Asset<Texture2D> fullEnergyBar;

        public EnergyCore BoundEnergyCore;

        public UIEnergyBar(EnergyCore boundEnergyCore)
        {
            this.BoundEnergyCore = boundEnergyCore;
        }

        

        public override void OnInitialize()
        {
            energyBar = TerraEnergy.GetTexture("Texture/TerraEnergyBar");
            fullEnergyBar = TerraEnergy.GetTexture("Texture/TerraEnergyBarFilled");

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDim = base.GetInnerDimensions();
            Vector2 picturePos = new Vector2(innerDim.X, innerDim.Y);
            Rectangle r = energyBar.Value.Bounds;


            spriteBatch.Draw(energyBar.Value, new Vector2(innerDim.X + 386, innerDim.Y + 14), r, Color.White, 0f, r.Size(), new Vector2(1f, 0.5f), SpriteEffects.None, 0f);
            if (IsMouseHovering)
            {

                if (BoundEnergyCore is FuelCore)
                {

                    Main.hoverItemName = BoundEnergyCore.getCurrentEnergyLevel() + " / " +
                                         BoundEnergyCore.getMaxEnergyLevel() + " Fuel";
                }
                else
                {
                    Main.hoverItemName = BoundEnergyCore.getCurrentEnergyLevel() + " / " +
                                         BoundEnergyCore.getMaxEnergyLevel() + " TE";
                }
                
                //spriteBatch.Draw(TextureAssets.Item[ItemID.Actuator], new Vector2(Main.mouseX, Main.mouseY + 20), r, Color.White, 0f, r.Size(), new Vector2(1f, 0.5f), SpriteEffects.None, 0f);
                //spriteBatch.DrawString(FontAssets.MouseText.Value, currentEntity.energy.getCurrentEnergyLevel() + " / " + currentEntity.energy.getMaxEnergyLevel() + " TE", new Vector2(Main.mouseX, Main.mouseY + 20), Color.White);
            }

            float percent = (BoundEnergyCore.getCurrentEnergyLevel() * 100 / BoundEnergyCore.getMaxEnergyLevel());
            Rectangle sourceRectangle = new Rectangle(0, 0, (int) (386 * (percent / 100)), 28);
            spriteBatch.Draw(fullEnergyBar.Value, new Vector2(innerDim.X + 386, innerDim.Y + 14), sourceRectangle, Color.White, 0f, r.Size(), new Vector2(1f, 0.5f), SpriteEffects.None, 0f);
        }
    }
}
