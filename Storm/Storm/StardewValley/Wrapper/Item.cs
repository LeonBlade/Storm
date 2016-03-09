﻿/*
    Copyright 2016 Cody R. (Demmonic)

    Storm is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Storm is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Storm.  If not, see <http://www.gnu.org/licenses/>.
 */

using Storm.StardewValley.Accessor;

namespace Storm.StardewValley.Wrapper
{
    public class Item : Wrapper<ItemAccessor>
    {
        private readonly ItemAccessor accessor;

        public Item(StaticContext parent, ItemAccessor accessor)
        {
            Parent = parent;
            this.accessor = accessor;
        }

        public StaticContext Parent { get; }

        public int Category
        {
            get { return accessor._GetCategory(); }
            set { accessor._SetCategory(value); }
        }

        public bool HasBeenInInventory
        {
            get { return accessor._GetHasBeenInInventory(); }
            set { accessor._SetHasBeenInInventory(value); }
        }

        public bool IsSpecialItem
        {
            get { return accessor._GetSpecialItem(); }
            set { accessor._SetSpecialItem(value); }
        }

        public int SpecialVariable
        {
            get { return accessor._GetSpecialVariable(); }
            set { accessor._SetSpecialVariable(value); }
        }

        public bool IsTool() => accessor is ToolAccessor;

        public Tool ToTool() => new Tool(Parent, (ToolAccessor)accessor);

        public ItemAccessor Expose() => accessor;
    }
}