﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace TerraEnergy.UI.Elements {
    public class GuiItemSlot : UIElement {
        private readonly int _context;
        private readonly float _scale;
        public Func<Item, bool> validItemFunc;

        private Ref<Item> _item;

        public ref Item item => ref _item.Value;

        public GuiItemSlot(Ref<Item> item, int context = ItemSlot.Context.ChestItem, float scale = 0.85f) {
            _item = item;

            _context = context;
            _scale = scale;

            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);

            validItemFunc = i => true;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();

            FixMouse();

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface) {
                Main.LocalPlayer.mouseInterface = true;
            }

            FixMouse(false);

            // Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
            ItemSlot.Draw(spriteBatch, ref item, _context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;
        }

        public Item GetItem() {
            Item tempItem;

            if (item.IsAir) {
                tempItem = item;
                item.TurnToAir();
                return tempItem;
            }

            tempItem = item.Clone();
            tempItem.stack = 1;
            item.stack -= 1;
            return tempItem;
        }

        public void ManipulateCurrentStack(int number) {
            int preCalculate = item.stack + number;
            if (preCalculate >= item.maxStack) {
                int overflow = preCalculate - item.maxStack;
                number = overflow;
                item.stack = item.maxStack;
                return;
            }
            item.stack = preCalculate;
        }

        void FixMouse(bool fix = true) {
            if (fix) {
                MouseX = Main.mouseX;
                MouseY = Main.mouseY;
                LastMouseX = Main.lastMouseX;
                LastMouseY = Main.lastMouseY;
                LastScreenWidth = Main.screenWidth;
                LastScreenHeight = Main.screenHeight;

                PlayerInput.SetZoom_Unscaled();
                PlayerInput.SetZoom_MouseInWorld();
            }
            else {
                Main.lastMouseX = LastMouseX;
                Main.lastMouseY = LastMouseY;
                Main.mouseX = MouseX;
                Main.mouseY = MouseY;
                Main.screenWidth = LastScreenWidth;
                Main.screenHeight = LastScreenHeight;
            }
        }


        public int ItemStack => item.stack;
        public int ItemType => item.type;

        public bool IsEmpty => item.IsAir;

        public Asset<Texture2D> ItemTexture => TextureAssets.Item[item.type];

        public int MouseX { get; private set; }
        public int MouseY { get; private set; }

        public int LastMouseX { get; private set; }
        public int LastMouseY { get; private set; }

        public int LastScreenWidth { get; private set; }
        public int LastScreenHeight { get; private set; }
    }
}
