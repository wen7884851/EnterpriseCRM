using Domain.Site;
using System;
using StructureMap;
using AutoMapper;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Web.Site.WebUI
{
    public static class MappingConfig
    {
        public static void MappingRegister(params Registry[] registries)
        {
            var container = new Container(cfg =>
            {
                cfg.AddRegistry(new AutoMappingRegistry());
            });
            Register(container);
        }
        public static void Register(IContainer container)
        {
            var mappings = container.GetAllInstances<IHaveMapping>();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                foreach (var mapping in mappings)
                {
                    mapping.CreateMappings(cfg);
                }
            });
        }
    }

    public class AutoMappingRegistry : Registry
    {
        public AutoMappingRegistry()
        {
            this.Scan(x =>
            {
                x.AssembliesFromApplicationBaseDirectory();
                x.AddAllTypesOf<IHaveMapping>();
            });
        }
    }
}