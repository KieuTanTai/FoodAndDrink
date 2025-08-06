using Dapper;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Services;
using System.Reflection;

namespace ProjectShop.Server.Infrastructure.Configuration;
public class SnakeCaseTypeMapperConfiguration<T> : FallbackTypeMapper
{
    public SnakeCaseTypeMapperConfiguration() : base(
    [
        // Use a lambda to match the expected delegate signature (PropertyInfo, not PropertyInfo?)
        new CustomPropertyTypeMap(typeof(T), (type, columnName) => SelectProperty(type, columnName) ?? 
            throw new InvalidOperationException($"No matching property for column '{columnName}' in type '{type.FullName}'.")),
        new DefaultTypeMap(typeof(T))
    ])
    { }

    private static PropertyInfo? SelectProperty(Type type, string columnName)
    {
        // Convert snake_case column to PascalCase property
        IStringConverter? converter = GetProviderService.SystemServices.GetService<IStringConverter>();
        if (converter is null)
            return null;
        string pascal = converter.SnakeCaseToPascalCase(columnName);
        return type.GetProperties().FirstOrDefault(p =>
            string.Equals(p.Name, pascal, StringComparison.OrdinalIgnoreCase));
    }
}

public class FallbackTypeMapper(SqlMapper.ITypeMap[] sqlMappers) : SqlMapper.ITypeMap
{
    public ConstructorInfo? FindConstructor(string[] names, Type[] types) =>
        sqlMappers.Select(mapper => mapper.FindConstructor(names, types)).FirstOrDefault(mapper => mapper != null);

    public ConstructorInfo? FindExplicitConstructor() =>
        sqlMappers.Select(mapper => mapper.FindExplicitConstructor()).FirstOrDefault(mapper => mapper != null);

    public SqlMapper.IMemberMap? GetConstructorParameter(ConstructorInfo constructor, string columnName) =>
        sqlMappers.Select(mapper => mapper.GetConstructorParameter(constructor, columnName)).FirstOrDefault(mapper => mapper != null);

    public SqlMapper.IMemberMap? GetMember(string columnName) =>
        sqlMappers.Select(mapper => mapper.GetMember(columnName)).FirstOrDefault(mapper => mapper != null);
}
