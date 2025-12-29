using System;
using System.Reflection;
using GameFramework;
using GameFramework.Dependencies;
using UnityEngine;

namespace NetworkSpecific
{
    /// <summary>
    /// Attribute to mark fields or properties for automatic injection from the Actor's components.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class BindActorComponentAttribute : Attribute
    {
        /// <summary>
        /// If true, logs an error if the component is not found on the Actor.
        /// </summary>
        public bool IsRequired { get; set; } = true;

        public BindActorComponentAttribute(bool isRequired = true)
        {
            IsRequired = isRequired;
        }
    }
    
    /// <summary>
    /// A static class providing extension methods for actor components in the network-specific context.
    /// </summary>
    public static class ActorComponentExtensions
    {
        /// <summary>
        /// Automatically populates fields and properties marked with [BindActorComponent] 
        /// by resolving them against the component's Actor.
        /// Like a lightweight Dependency Injection system for Actor Components.
        /// You should call this method in the Start method of your IActorComponent implementation (as OnEnable/Awake might be too early / race condition).
        /// </summary>
        /// <param name="component">The component requesting injection.</param>
        public static void InjectActorDependencies(this IActorComponent component)
        {
            // Safety check: Ensure we are working with a MonoBehaviour context if needed, 
            // though IActorComponent is sufficient for the logic.
            
            // 1. Ensure the Actor is assigned.
            // If called in Awake/OnEnable, the Actor might not be assigned by the parent AbstractActor yet.
            // We attempt to find it if it is missing.
            if (component.Actor == null && component is MonoBehaviour mb)
            {
                Debug.LogWarning($"[InjectActorDependencies] 'Actor' is null for '{component.GetType().Name}'. Attempting to find Actor in parent hierarchy.");
                component.Actor = mb.GetComponentInParent<IActor>();
            }

            if (component.Actor == null)
            {
                Debug.LogWarning($"[InjectActorDependencies] Could not inject dependencies for '{component.GetType().Name}' because 'Actor' is null.");
                return;
            }

            var type = component.GetType();
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            // 2. Handle Fields
            var fields = type.GetFields(flags);
            foreach (var field in fields)
            {
                var attr = field.GetCustomAttribute<BindActorComponentAttribute>();
                if (attr == null) continue;

                if (TryResolveDependency(component.Actor, field.FieldType, out var result))
                {
                    field.SetValue(component, result);
                }
                else if (attr.IsRequired)
                {
                    Debug.LogError($"[InjectActorDependencies] Required dependency '{field.FieldType.Name}' missing for '{type.Name}' on Actor '{component.Actor.UUID}'.");
                }
            }

            // 3. Handle Properties
            var properties = type.GetProperties(flags);
            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<BindActorComponentAttribute>();
                if (attr == null) continue;

                if (!prop.CanWrite)
                {
                    Debug.LogError($"[InjectActorDependencies] Property '{prop.Name}' in '{type.Name}' is marked for binding but is read-only.");
                    continue;
                }

                if (TryResolveDependency(component.Actor, prop.PropertyType, out var result))
                {
                    prop.SetValue(component, result);
                }
                else if (attr.IsRequired)
                {
                    Debug.LogError($"[InjectActorDependencies] Required dependency '{prop.PropertyType.Name}' missing for '{type.Name}' on Actor '{component.Actor.UUID}'.");
                }
            }
        }
        
        private static bool TryResolveDependency(IActor actor, Type targetType, out object? result)
        {
            result = actor.ComponentsContainer.GetComponent(targetType);
            return result != null;
        }
    }
}