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
using Storm.Manipulation.Cecil;
using System;

namespace Storm.Manipulation
{
    public sealed class InjectorFactories
    {
        private InjectorFactories() { }

        public static InjectorFactory Create(InjectorFactoryType type, string path)
        {
            switch (type)
            {
                case InjectorFactoryType.Cecil:
                    return new CecilInjectorFactory(path);
            }
            throw new NotSupportedException(string.Format("Unsupported factory type {0}", type));
        }
    }
}
