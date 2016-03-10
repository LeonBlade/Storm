﻿/*
    Copyright 2016 Cody R. (Demmonic), Inari Whitebear

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

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Storm.StardewValley.Accessor;
using Storm.Collections;

namespace Storm.StardewValley.Wrapper
{
    public class GameLocation : Wrapper
    {
        private readonly GameLocationAccessor accessor;

        public GameLocation(StaticContext parent, GameLocationAccessor accessor)
        {
            Parent = parent;
            this.accessor = accessor;
        }

        public StaticContext Parent { get; }

        public void AddHoeDirtAt(Vector2 tileLocation)
        {
            accessor._MakeHoeDirt(tileLocation);
        }

        public string GetTileProperty(int tileX, int tileY, string propName, string layerName)
        {
            return accessor._GetTileProperty(tileX, tileY, propName, layerName);
        }

        public Dictionary<Vector2, ObjectItem> Objects
        {
            get
            {
                var orig = accessor._GetObjects();
                var conv = new Dictionary<Vector2, ObjectItem>();
                foreach (var vec in orig.Keys)
                {
                    conv.Add((Vector2) vec, new ObjectItem(Parent, (ObjectAccessor) orig[vec]));
                }
                return conv;
            }
        }

        public Dictionary<Vector2, TerrainFeature> TerrainFeatures
        {
            get
            {
                var orig = accessor._GetTerrainFeatures();
                var conv = new Dictionary<Vector2, TerrainFeature>();
                foreach (var vec in orig.Keys)
                {
                    conv.Add((Vector2) vec, new TerrainFeature(Parent, (TerrainFeatureAccessor) orig[vec]));
                }
                return conv;
            }
        }

        public bool IsOutdoors
        {
            get { return accessor._GetIsOutdoors(); }
            set { accessor._SetIsOutdoors(value); }
        }

        public string Name
        {
            get { return accessor._GetName(); }
            set { accessor._SetName(value); }
        }

        public WrappedProxyList<NPCAccessor, NPC> Characters
        {
            get
            {
                return new WrappedProxyList<NPCAccessor, NPC>(accessor._GetCharacters(), c => c == null ? null : new NPC(Parent, c));
            }
        }

        public Event CurrentEvent
        {
            get
            {
                var tmp = accessor._GetCurrentEvent();
                if (tmp == null) return null;
                return new Event(Parent, tmp);
            }
            set { accessor._SetCurrentEvent(value?.Cast<EventAccessor>()); }
        }

        public void GrowWeedGrass(int iterations)
        {
            accessor._GrowWeedGrass(iterations);
        }

        public ObjectItem GetObjectAt(int tileX, int tileY)
        {
            var key = new Vector2(tileX / Parent.TileSize, tileY / Parent.TileSize);
            var objects = Objects;
            if (objects.ContainsKey(key))
            {
                return objects[key];
            }
            return null;
        }

        public object Expose() => accessor;
    }
}