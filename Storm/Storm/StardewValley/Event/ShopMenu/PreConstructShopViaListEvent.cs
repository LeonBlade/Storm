﻿/*
    Copyright 2016

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
using Storm.StardewValley.Wrapper;

namespace Storm.StardewValley.Event
{
    public class PreConstructShopViaListEvent : StaticContextEvent
    {
        public PreConstructShopViaListEvent(ProxyList<ItemAccessor, Item> itemsForSale, int currency = 0, string who = null)
        {
            ItemsForSale = itemsForSale;
            Currency = currency;
            Who = who;
        }

        public ProxyList<ItemAccessor, Item> ItemsForSale { get; }
        public int Currency { get; }
        public string Who { get; }
    }
}