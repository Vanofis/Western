using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TriInspector;
using Zenject;

namespace Rowboat.Western.ProjectTools.Dependency
{
    public class SimpleComponentMonoInstaller : MonoInstaller
    {
        private enum InstallType
        {
            Transient,
            Single,
            Cached,
        }
        
        [SerializeField] 
        private GameObject startGameObject;
        
#if UNITY_EDITOR
        [Dropdown(nameof(GetComponentsDropdown))]
#endif
        [SerializeField]
        private Component componentToInstall;
        
        [PropertySpace]
        [EnumToggleButtons]
        [SerializeField]
        private InstallType installType = InstallType.Transient;
        [SerializeField] 
        private bool nonLazy = true;
        
#if UNITY_EDITOR
        private IEnumerable<TriDropdownItem<Component>> GetComponentsDropdown()
        {
            if (!startGameObject)
            {
                return new TriDropdownList<Component>();
            }
            
            var componentsDropdown = new TriDropdownList<Component>();
            var components = startGameObject.GetComponentsInChildren<Component>();
            var builder = new StringBuilder();
            
            foreach (var component in components)
            {
                if (!component || component.GetType() == typeof(SimpleComponentMonoInstaller))
                {
                    continue;
                }
                
                builder.Append(component.gameObject.name);
                builder.Append("/");
                builder.Append(component.GetType().Name);
                
                componentsDropdown.Add(builder.ToString(), component);

                builder.Clear();
            }
            
            return componentsDropdown;
        }

        private void OnValidate()
        {
            startGameObject ??= gameObject;
        }
#endif
        
        public override void InstallBindings()
        {
            if (!componentToInstall)
            {
                throw new NullReferenceException($"{nameof(SimpleComponentMonoInstaller)} " +
                                                 $"requires a valid component on {name}");
            }

            var bind = Container
                .Bind(componentToInstall.GetType())
                .FromInstance(componentToInstall);

            switch (installType)
            {
                case InstallType.Transient:
                    bind.AsTransient();
                    break;
                case InstallType.Single:
                    bind.AsSingle();
                    break;
                case InstallType.Cached:
                    bind.AsCached();
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (nonLazy)
            {
                bind.NonLazy();
            }
        }
    }
}