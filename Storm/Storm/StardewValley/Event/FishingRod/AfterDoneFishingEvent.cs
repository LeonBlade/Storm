﻿/*
    Copyright 2016 Zoey (Zoryn)

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

namespace Storm.StardewValley.Event
{
    public class AfterDoneFishingEvent : StaticContextEvent
    {
        public AfterDoneFishingEvent(Wrapper.Farmer who, Wrapper.FishingRod rod, bool consumeBaitAndTackle)
        {
            Who = who;
            Rod = rod;
            ConsumeBaitAndTackle = consumeBaitAndTackle;
        }

        public Wrapper.Farmer Who { get; }
        public bool ConsumeBaitAndTackle { get; }
        public Wrapper.FishingRod Rod { get; }
    }
}