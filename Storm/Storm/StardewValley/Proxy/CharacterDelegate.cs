﻿/*
    Copyright 2016 Inari-Whitebear

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

using Microsoft.Xna.Framework;
using Storm.StardewValley.Wrapper;

namespace Storm.StardewValley.Proxy
{
    public class CharacterDelegate : TypeDelegate<Character>
    {
        private object[] constructorParams;

        public CharacterDelegate()
        {
            constructorParams = new object[] { };
        }

        public CharacterDelegate(AnimatedSprite sprite, Vector2 position, int speed, string name)
        {
            constructorParams = new object[] { sprite.Expose(), position, speed, name };
        }

        public object[] GetConstructorParams()
        {
            return constructorParams;
        }
    }
}
