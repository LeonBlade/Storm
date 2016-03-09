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

namespace Storm.StardewValley.Event.HoeDirt
{
    public class BeforeHoeDirtCanPlantEvent : StaticContextEvent
    {
        public BeforeHoeDirtCanPlantEvent(int objectIndex, int tileX, int tileY, bool isFertilizer = false)
        {
            ObjectIndex = objectIndex;
            TileX = tileX;
            TileY = tileY;
            IsFertilizer = isFertilizer;
        }

        public int ObjectIndex { get; }
        public int TileX { get; }
        public int TileY { get; }
        public bool IsFertilizer { get; }
    }
}