using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace StatBot.UserIO
{
    public static class UserMessagePrinterEmitter
    {
        public static Type EmitImplType()
        {
            var namePrefix = nameof(IUserMessagePrinter).Substring(1);

            var assemblyName = new AssemblyName(namePrefix + "DynamicAssembly");

            var assembly =
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            var module = assembly.DefineDynamicModule(namePrefix + "DynamicModule",
                                                      namePrefix + "DynamicModule.dll");

            var type = module.DefineType(namePrefix, TypeAttributes.Public);
            type.AddInterfaceImplementation(typeof(IUserMessagePrinter));

            var printerField = type.DefineField(nameof(IMessagePrinter).Substring(1),
                                                typeof(IMessagePrinter),
                                                FieldAttributes.Private);

            EmitCtor(type, printerField);

            EmitUnderlyingPrinterProperty(type, printerField);

            var methodsInfos = typeof(IUserMessagePrinter).GetMethods().Where(m => !m.Name.StartsWith("get_"));
            foreach (var methodInfo in methodsInfos)
            {
                var parameterInfos = methodInfo.GetParameters();

                var method = type.DefineMethod(methodInfo.Name,
                                               MethodAttributes.Public | MethodAttributes.Virtual,
                                               methodInfo.ReturnType,
                                               parameterInfos.Select(p => p.ParameterType).ToArray()
                );

                type.DefineMethodOverride(method, typeof(IUserMessagePrinter).GetMethod(methodInfo.Name));

                var methodIl = method.GetILGenerator();

                var message = BuildUserMessageFromMethodName(methodInfo);

                methodIl.Emit(OpCodes.Ldarg_0);
                methodIl.Emit(OpCodes.Ldfld, printerField);
                methodIl.Emit(OpCodes.Ldstr, message);

                if (parameterInfos.Any())
                {
                    EmitStringConcat(methodIl, ". ");
                }

                for (var i = 0; i < parameterInfos.Length; i++)
                {
                    EmitStringConcat(methodIl, parameterInfos[i].Name + ": ");

                    methodIl.Emit(OpCodes.Ldarg, i + 1);
                    methodIl.Emit(OpCodes.Callvirt, typeof(object).GetMethod("ToString", new Type[0]));
                    EmitStringConcat(methodIl);

                    if (i < parameterInfos.Length - 1)
                    {
                        EmitStringConcat(methodIl, ", ");
                    }
                }

                methodIl.Emit(OpCodes.Callvirt,
                              typeof(IMessagePrinter).GetMethod("Print", new Type[1] {typeof(string)}));
                methodIl.Emit(OpCodes.Ret);
            }

            var implType = type.CreateType();

            assembly.Save(namePrefix + "DynamicAssembly" + ".dll");

            return implType;
        }

        private static void EmitCtor(TypeBuilder type, FieldBuilder printerField)
        {
            var ctor = type.DefineConstructor(MethodAttributes.Public,
                                              CallingConventions.HasThis,
                                              new[] {typeof(IMessagePrinter)});

            var ctorIl = ctor.GetILGenerator();

            ctorIl.Emit(OpCodes.Ldarg_0);
            ctorIl.Emit(OpCodes.Ldarg_1);
            ctorIl.Emit(OpCodes.Stfld, printerField); // this.printer = printer;
            ctorIl.Emit(OpCodes.Ret);
        }

        private static void EmitUnderlyingPrinterProperty(TypeBuilder type, FieldBuilder printerField)
        {
            const string printerPropName = "UnderlyingPrinter";
            var underlyingPrinter =
                typeof(IUserMessagePrinter).GetProperty(printerPropName, typeof(IMessagePrinter), Type.EmptyTypes);
            var getMethod = underlyingPrinter.GetGetMethod();

            var impl = type.DefineMethod(getMethod.Name,
                                         MethodAttributes.Public | MethodAttributes.Virtual,
                                         getMethod.ReturnType,
                                         Type.EmptyTypes);

            var getGenerator = impl.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Ldfld, printerField);
            getGenerator.Emit(OpCodes.Ret);

            type.DefineMethodOverride(impl, getMethod);
        }

        private static void EmitStringConcat(ILGenerator il, string piece)
        {
            il.Emit(OpCodes.Ldstr, piece);
            EmitStringConcat(il);
        }

        private static void EmitStringConcat(ILGenerator il)
        {
            il.Emit(OpCodes.Call, typeof(string).GetMethod("Concat", new[] {typeof(string), typeof(string)}));
        }

        private static string BuildUserMessageFromMethodName(MethodInfo methodInfo)
        {
            var methodWords = Regex.Matches(methodInfo.Name, @"([A-Z][a-z]+)")
                                   .Cast<Match>()
                                   .Select(m => m.Value);
            return string.Join(" ", methodWords);
        }
    }
}