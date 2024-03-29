﻿using Terraria;
using Terraria.ModLoader;

namespace TerraEnergy.MachineRecipe.Forge
{
    class ForgeRecipe
    {
        private Mod mod;
        private int cookTime = 10;
        private Item ingredient;
        private Item ingredient2;
        private string ingredientName;
        private string catalyserName;
        private Item result;
        private string resultName;

        public ForgeRecipe(Mod mod)
        {
            this.mod = mod;
        }

        public void AddIngredient(int itemID, int quantity = 1)
        {
            ingredient = new Item();
            ingredient.type = itemID;
            ingredient.stack = quantity;
            ingredientName = ingredient.Name;
        }

        public void AddCatalyst(int itemID, int quantity = 1)
        {
            ingredient2 = new Item();
            ingredient2.type = itemID;
            ingredient2.stack = quantity;
            catalyserName = ingredient2.Name;
        }

        public void SetResult(int itemID, int quantity = 1)
        {

            result = new Item();
            result.SetDefaults(itemID, false);
            result.stack = quantity;
            resultName = result.Name;
        }

        public bool checkItem(Item[] ingredients)
        {
            string ingredientList = "";
            for (int i1 = 0; i1 < ingredients.Length; i1++)
            {
                Item i = ingredients[i1];
                if (i != null)
                {
                    ingredientList += i.Name + " ";
                }
            }
            return ingredientList.Contains(ingredient.Name) && ingredientList.Contains(ingredient2.Name);
        }

        public bool checkQuantity(Item[] ingredients)
        {
            bool ingredientFlag = false;
            bool catalyserFlag = false;
            for (int i1 = 0; i1 < ingredients.Length; i1++)
            {
                Item i = ingredients[i1];
                if (i.Name == ingredient.Name && i.stack >= ingredient.stack)
                {
                    ingredientFlag = true;
                }
                if (i.Name == ingredient2.Name && i.stack >= ingredient2.stack)
                {
                    catalyserFlag = true;
                }
            }
            return ingredientFlag && catalyserFlag;
        }

        public void AddRecipe()
        {
            ForgeRecipeManager.getInstance().AddRecipe(this);
        }
    }
}
