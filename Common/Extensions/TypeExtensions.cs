using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class TypeExtensions
    {
        public static List<ColumnInfo> GetMetadata(this Type type)
        {
            return type.GetProperties()
                .Select(x => new ColumnInfo
                {
                    Name = x.Name,
                })
                .ToList();
        }
    }
}
