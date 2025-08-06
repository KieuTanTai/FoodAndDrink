using Dapper;
using ProjectShop.Server.Infrastructure.Configuration;
using System.Reflection;

namespace ProjectShop.Server.Infrastructure.Persistence;
public static class SnakeCaseMapperInitializer
{
    public static void RegisterAllEntities()
    {
        IEnumerable<Type> entityTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsClass && type.Namespace == "ProjectShop.Server.Core.Entities");

        foreach (Type type in entityTypes)
        {
            Type mapperType = typeof(SnakeCaseTypeMapperConfiguration<>).MakeGenericType(type);
            object? instance = Activator.CreateInstance(mapperType);
            if (instance is SqlMapper.ITypeMap mapperInstance)
                SqlMapper.SetTypeMap(type, mapperInstance);
            else
                throw new InvalidOperationException($"Could not create an instance of {mapperType.FullName} as SqlMapper.ITypeMap.");
        }
    }
}
