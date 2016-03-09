﻿using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Manipulation.Cecil
{
    public class CecilConstructorReplacer : Injector
    {
        private readonly AssemblyDefinition def;
        private readonly AssemblyDefinition self;
        private readonly ConstructorReplacerParams @params;

        public CecilConstructorReplacer(AssemblyDefinition self, AssemblyDefinition def, ConstructorReplacerParams @params)
        {
            this.self = self;
            this.def = def;
            this.@params = @params;
        }

        public void Init()
        {
            
        }

        public void Inject()
        {
            var from = def.GetTypeRef(@params.FromClass);
            if (from == null)
            {
                from = self.GetTypeRef(@params.FromClass, true);
            }

            if (from == null)
            {
                Logging.DebugLogs("[{0}] Unable to find from type", GetType().Name);
                Logging.DebugLogs("\t{0} {1}", @params.FromClass, @params.ToClass);
                return;
            }

            var to = def.GetTypeRef(@params.ToClass);
            if (to == null)
            {
                to = self.GetTypeRef(@params.ToClass, true);
            }

            if (to == null)
            {
                Logging.DebugLogs("[{0}] Unable to find to type", GetType().Name);
                Logging.DebugLogs("\t{0} {1}", @params.FromClass, @params.ToClass);
                return;
            }

            var methods = def.Modules.SelectMany(m => m.Types).SelectMany(t => t.Methods).Where(m => m.HasBody).ToList();
            
            foreach (var method in methods)
            {
                var body = method.Body;
                var instructions = body.Instructions;
                var processor = body.GetILProcessor();

                for (int i = 0; i < instructions.Count; i++)
                {
                    var ins = instructions[i];
                    if (ins.OpCode == OpCodes.Call || ins.OpCode == OpCodes.Callvirt)
                    {
                        var @ref = (MethodReference)ins.Operand;
                        if (@ref.DeclaringType.FullName.Equals(@params.FromClass))
                        {
                            var redirect = to.Resolve().Methods.Where(m => m.Name.Equals(@ref.Name) && (@ref.Parameters.Count + 1) == m.Parameters.Count).FirstOrDefault();
                            if (redirect != null)
                            {
                                var call = def.Import(redirect);
                                if (@ref is GenericInstanceMethod)
                                {
                                    GenericInstanceMethod genericCall = new GenericInstanceMethod(def.Import(redirect));
                                    var gim = (GenericInstanceMethod)@ref;
                                    foreach (var arg in gim.GenericArguments)
                                    {
                                        genericCall.GenericArguments.Add(arg);
                                    }
                                    call = genericCall;
                                }

                                processor.Replace(ins, processor.Create(OpCodes.Call, call));
                            }
                        }
                    }
                }
            }
        }

        public object GetParams()
        {
            return @params;
        }
    }
}
