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
using System;
using System.IO;
using Storm.StardewValley;
using System.Diagnostics;

namespace Storm
{
    class Program
    {
        static void Main(string[] args)
        {
            /* allow window resizing on osx & *nix */
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                Environment.SetEnvironmentVariable("FNA_WORKAROUND_WINDOW_RESIZABLE", "1");

            Logging.Log = (msg) => Console.WriteLine(msg);
            Logging.DebugLog = (msg) => Debug.WriteLine(msg);

            var launcher = new ManagedStardewValleyLauncher(new FileStream(StormAPI.GetResource("injectors.json"), FileMode.Open), "Stardew Valley.exe");
            launcher.Launch();
            launcher.Dispose();

            Console.ReadKey();
        }
    }
}
