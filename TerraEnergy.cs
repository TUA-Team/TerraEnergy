using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerraEnergy.MachineRecipe.Forge;
using TerraEnergy.MachineRecipe.Furnace;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TUA.Utilities;

namespace TerraEnergy {
    public class TerraEnergy : Mod
	{
        public static TerraEnergy Instance => ModContent.GetInstance<TerraEnergy>();

        public static Asset<Texture2D> GetTexture(string path) {
            return ModContent.Request<Texture2D>("TerraEnergy/" + path);
        }

        public override void AddRecipes() {
            #region PreHardmode
            AddFurnaceRecipe(ItemID.CopperOre, ItemID.CopperBar);
            AddFurnaceRecipe(ItemID.TinOre, ItemID.TinBar, 10);
            AddFurnaceRecipe(ItemID.IronOre, ItemID.IronBar, 30);
            AddFurnaceRecipe(ItemID.LeadOre, ItemID.LeadBar);
            AddFurnaceRecipe(ItemID.SilverOre, ItemID.SilverBar);
            AddFurnaceRecipe(ItemID.TungstenOre, ItemID.TungstenBar);
            AddFurnaceRecipe(ItemID.GoldOre, ItemID.GoldBar, 30);
            AddFurnaceRecipe(ItemID.PlatinumOre, ItemID.PlatinumBar, 40);
            AddFurnaceRecipe(ItemID.CrimtaneOre, ItemID.CrimtaneBar);
            AddFurnaceRecipe(ItemID.DemoniteOre, ItemID.DemoniteBar);
            AddFurnaceRecipe(ItemID.Meteorite, ItemID.MeteoriteBar, 30);
            #endregion
            #region Hardmode
            AddFurnaceRecipe(ItemID.CobaltOre, ItemID.CobaltBar, 25);
            AddFurnaceRecipe(ItemID.PalladiumOre, ItemID.PalladiumBar, 25);
            AddFurnaceRecipe(ItemID.MythrilOre, ItemID.MythrilBar, 40);
            AddFurnaceRecipe(ItemID.OrichalcumOre, ItemID.OrichalcumBar, 50);
            AddFurnaceRecipe(ItemID.AdamantiteOre, ItemID.AdamantiteBar, 70);
            AddFurnaceRecipe(ItemID.TitaniumOre, ItemID.TitaniumBar, 80);
            AddFurnaceRecipe(ItemID.ChlorophyteOre, ItemID.ChlorophyteBar, 120);
            AddFurnaceRecipe(ItemID.LunarOre, ItemID.LunarBar, 240);
            #endregion
            AddFurnaceRecipe(ItemID.SandBlock, ItemID.Glass, 20);

            //Smetler recipe recipe
            AddInductionSmelterRecipe(ItemID.Hellstone, ItemID.Obsidian, ItemID.HellstoneBar, 1, 2, 1, 90);
            AddInductionSmelterRecipe(ItemID.ChlorophyteBar, ItemID.Ectoplasm, ItemID.SpectreBar, 4, 1, 3);
            AddInductionSmelterRecipe(ItemID.ChlorophyteBar, ItemID.GlowingMushroom, ItemID.ShroomiteBar, 1, 5, 1, 180);
        }

        public void AddFurnaceRecipe(int itemID, int itemResult, int timer = 20) {
            FurnaceRecipe r1 = FurnaceRecipeManager.CreateRecipe(this);
            r1.AddIngredient(itemID, 1);
            r1.SetResult(itemResult, 1);
            r1.SetCostAndCookTime(timer);
            r1.AddRecipe();
        }

        public void AddInductionSmelterRecipe(int catalyst, int reactant,
            int product, int catalystQuantity = 1, int reactantQuantity = 1,
            int productQuantity = 1, int timer = 120) {
            ForgeRecipe fr1 = ForgeRecipeManager.CreateRecipe(this);
            fr1.AddCatalyst(catalyst, catalystQuantity);
            fr1.AddIngredient(reactant, reactantQuantity);
            fr1.SetResult(product, productQuantity);
            fr1.AddRecipe();
        }
    }
}