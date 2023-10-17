using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using HackingGame.Common.Controllers;


namespace HackingGame.Common
{
    public partial class ControllerRegistry : Node
    {
        // Registers controllers via interfaces for state objects/models.
        // Think of this as silly dependency injector.
        // This class is parent
        public void RegisterControllers()
        {
            RegisterChildren();
        }

        public static ControllerRegistry instance;

        private Dictionary<RegistryKey, IModelController> registry;

        public override void _Ready()
        {
            instance = this;
            registry = new Dictionary<RegistryKey, IModelController>();

            RegisterControllers();
        }

        public static T Get<T>() where T : class, IModelController
        {
            if(instance.registry.TryGetValue(RegistryKey.Get<T>(), out var obj))
            {
                return obj as T;
            }
            else
            {
                obj = instance.registry.First(x => x.Key.modelType == typeof(T)).Value;
                return obj as T;
            }
        }

        public static T Get<T>(string arg) where T : class, IModelController
        {
            return instance.registry[RegistryKey.Get<T>(arg)] as T;
        }

        public static void Register(IModelController controller, string arg = null)
        {
            var key = new RegistryKey(controller.ControllerInterface, arg);
            instance.registry[key] = controller;
        }

        public static void Register<T>(T controller, string arg = null) where T : class, IModelController
        {
            instance.registry[RegistryKey.Get<T>(arg)] = controller;
        }

        private void RegisterChildren()
        {
            foreach(var child in GetChildren())
            {
                if(child is IModelController modelController)
                {
                    Register(modelController);
                }
            }
        }

        private struct RegistryKey
        {
            public readonly Type modelType;
            public readonly string argument;

            public RegistryKey(Type modelType, string argument)
            {
                this.modelType = modelType;
                this.argument = argument;
            }

            public static RegistryKey Get<T>()
            {
                return RegistryKey.Get<T>(null);
            }

            public static RegistryKey Get<T>(string arg)
            {
                return new RegistryKey(typeof(T), arg);
            }
        }
    }
}