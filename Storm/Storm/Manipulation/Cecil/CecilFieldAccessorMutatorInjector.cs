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
using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Manipulation.Cecil
{
    public class CecilFieldAccessorMutatorInjector : Injector
    {
        private AssemblyDefinition self;
        private AssemblyDefinition def;
        private FieldAccessorMutatorParams @params;

        public CecilFieldAccessorMutatorInjector(AssemblyDefinition self, AssemblyDefinition def, FieldAccessorMutatorParams @params)
        {
            this.self = self;
            this.def = def;
            this.@params = @params;
        }

        public void Inject()
        {
            var methodType = def.GetTypeRef(@params.Type, true);
            var field = def.GetField(@params.OwnerType, @params.OwnerFieldName, @params.OwnerFieldType);

            if (methodType == null)
            {
                Logging.DebugLog(String.Format("[CecilFieldAccessorMutatorInjector] Could not find methodType {0} {1} {2} {3} {4} {4} {5}",
                     @params.OwnerType, @params.OwnerFieldName, @params.OwnerFieldType,
                     @params.MethodName, @params.Type, @params.IsStatic));
                return;
            }

            if (field == null)
            {
                Logging.DebugLog(String.Format("[CecilFieldAccessorMutatorInjector] Could not find field {0} {1} {2} {3} {4} {4} {5}",
                     @params.OwnerType, @params.OwnerFieldName, @params.OwnerFieldType,
                     @params.MethodName, @params.Type, @params.IsStatic));
                return;
            }
            
            var mutator = new MethodDefinition("_Set" + @params.MethodName, MethodAttributes.Public | MethodAttributes.NewSlot | MethodAttributes.Virtual, def.Import(typeof(void)));
            {
                mutator.Parameters.Add(new ParameterDefinition(def.Import(methodType)));

                var instructions = mutator.Body.Instructions;
                var processor = mutator.Body.GetILProcessor();
                if (!@params.IsStatic)
                {
                    instructions.Add(processor.Create(OpCodes.Ldarg_0));
                }
                instructions.Add(processor.Create(@params.IsStatic ? OpCodes.Ldarg_0 : OpCodes.Ldarg_1));
                instructions.Add(processor.Create(OpCodes.Stfld, field));
                instructions.Add(processor.Create(OpCodes.Ret));
            }

            var accessor = new MethodDefinition("_Get" + @params.MethodName, MethodAttributes.Public | MethodAttributes.NewSlot | MethodAttributes.Virtual, def.Import(methodType));
            {
                var instructions = accessor.Body.Instructions;
                var processor = accessor.Body.GetILProcessor();
                if (!@params.IsStatic)
                {
                    instructions.Add(processor.Create(OpCodes.Ldarg_0));
                }
                instructions.Add(processor.Create(OpCodes.Ldfld, field));
                instructions.Add(processor.Create(OpCodes.Ret));
            }

            field.DeclaringType.Methods.Add(mutator);
            field.DeclaringType.Methods.Add(accessor);
        }

        public object GetParams()
        {
            return @params;
        }
    }
}
