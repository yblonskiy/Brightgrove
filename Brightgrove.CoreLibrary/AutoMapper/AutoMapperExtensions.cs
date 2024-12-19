using AutoMapper.Configuration;
using System.Reflection;

namespace AutoMapper
{
    /// <summary>
    /// Taken from here: https://stackoverflow.com/questions/71311303/replacement-for-automappers-forallothermembers
    /// WARNING : This is a hack, we should really not use this or create some unit tests to make sure it works in the next version
    /// </summary>
    public static class AutoMapperExtensions
    {
        private static readonly PropertyInfo TypeMapActionsProperty = typeof(TypeMapConfiguration).GetProperty("TypeMapActions", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void ForAllOtherMembers<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression, Action<IMemberConfigurationExpression<TSource, TDestination, object>> memberOptions)
        {
            var typeMapConfiguration = (TypeMapConfiguration)expression;

            var typeMapActions = (List<Action<TypeMap>>)TypeMapActionsProperty.GetValue(typeMapConfiguration);

            typeMapActions.Add(typeMap =>
            {
                var destinationTypeDetails = typeMap.DestinationTypeDetails;

                foreach (var accessor in destinationTypeDetails.WriteAccessors.Where(m => typeMapConfiguration.GetDestinationMemberConfiguration(m) == null))
                {
                    expression.ForMember(accessor.Name, memberOptions);
                }
            });
        }
    }
}