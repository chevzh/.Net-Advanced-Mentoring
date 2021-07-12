using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTrees.Task2.ExpressionMapping
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));

            Type destinationType = typeof(TDestination);
            MemberInitExpression constructor = Expression.MemberInit(Expression.New(destinationType), BindMembers(destinationType, sourceParam));

            var mapFunction = 
                Expression.Lambda<Func<TSource, TDestination>>(
                        constructor,
                        sourceParam
                    );

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }

        private List<MemberBinding> BindMembers(Type destinationType, ParameterExpression sourceParam)
        {
            List<MemberBinding> members = new List<MemberBinding>();

            List<PropertyInfo> sourceProperties = sourceParam.Type.GetProperties().Where(x => x.CanRead).ToList();
            List<PropertyInfo> destinationProperties = destinationType.GetProperties().Where(x => x.CanWrite).ToList();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                if (destinationProperty == null)
                {
                    continue;
                }

                MemberExpression accessMember = Expression.MakeMemberAccess(sourceParam, sourceProperty);
                MemberAssignment  assignedMember = Expression.Bind(destinationProperty, accessMember);
                members.Add(assignedMember);
            }

            return members;
        }
    }
}
