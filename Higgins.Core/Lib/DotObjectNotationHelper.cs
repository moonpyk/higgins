using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Higgins.Core.Lib
{
    public class DotObjectNotationHelper
    {
        public static bool Apply(DotObjectNotationResult res, object dest)
        {
            if (string.IsNullOrWhiteSpace(res.Value))
            {
                return false;
            }

            var current = dest;

            while (res.PathElements.Count > 0)
            {
                var destPath = res.PathElements.Dequeue();

                var type = current.GetType();

                {
                    var directProp = type.GetProperty(destPath);

                    if (directProp != null)
                    {
                        if (res.PathElements.Count > 0)
                        {
                            current = directProp.GetValue(current);
                            continue;
                        }
                        return TrySetPropertyValue(directProp, current, res.Value);
                    }
                }

                {
                    var directField = type
                        .GetField(destPath);

                    if (directField != null)
                    {
                        if (res.PathElements.Count > 0)
                        {
                            current = directField.GetValue(current);
                            continue;
                        }

                        return TrySetFieldValue(directField, current, res.Value);
                    }
                }

                {
                    var dataMember = type.GetProperties().Select(_ => new
                    {
                        Property = _,
                        DataMember = _.GetCustomAttribute<DataMemberAttribute>()
                    }).FirstOrDefault(
                        _ => _.DataMember != null && 
                             !string.IsNullOrEmpty(_.DataMember.Name) &&  
                             _.DataMember.Name.ToLower() == destPath.ToLower()
                        );

                    if (dataMember != null)
                    {
                        if (res.PathElements.Count > 0)
                        {
                            current = dataMember.Property.GetValue(current);
                            continue;
                        }

                        return TrySetPropertyValue(dataMember.Property, current, res.Value);
                    }
                }

                {
                    var dataFieldMember = type.GetFields().Select(_ => new
                    {
                        Field = _,
                        DataMember = _.GetCustomAttribute<DataMemberAttribute>()
                    }).FirstOrDefault(_ => _.DataMember != null && !string.IsNullOrEmpty(_.DataMember.Name) &&
                                           _.DataMember.Name.ToLower() == destPath.ToLower());

                    if (dataFieldMember != null)
                    {
                        if (res.PathElements.Count > 0)
                        {
                            current = dataFieldMember.Field.GetValue(current);
                            continue;
                        }

                        return TrySetFieldValue(dataFieldMember.Field, current, res.Value);
                    }
                }
            }

            return false;
        }

        public static DotObjectNotationResult Parse(string toParse)
        {
            var arr = toParse.Split('=');

            if (arr.Length > 2)
            {
                throw new Exception("Invalid expression passed");
            }

            var ret = new DotObjectNotationResult();

            var pathExpression = arr[0].Split('.');
            ret.PathElements = new Queue<string>(pathExpression);

            string valueExpression = null;
            if (arr.Length == 2)
            {
                valueExpression = arr[1];
            }

            ret.Value = valueExpression;

            return ret;
        }

        private static bool TrySetPropertyValue(PropertyInfo info, object dest, string value)
        {
            try
            {
                if (info.PropertyType == typeof(string))
                {
                    info.SetValue(dest, value);
                    return true;
                }

                if (info.PropertyType == typeof(bool))
                {
                    info.SetValue(dest, ParseBool(value));
                    return true;
                }

                if (info.PropertyType == typeof(int))
                {
                    int intVal;
                    if (int.TryParse(value, out intVal))
                    {
                        info.SetValue(dest, intVal);
                        return true;
                    }
                }

                if (info.PropertyType == typeof(long))
                {
                    long longVal;
                    if (long.TryParse(value, out longVal))
                    {
                        info.SetValue(dest, longVal);
                        return true;
                    }
                }
            }
            catch (Exception) { }

            return false;
        }

        private static bool TrySetFieldValue(FieldInfo info, object dest, string value)
        {
            try
            {
                if (info.FieldType == typeof(string))
                {
                    info.SetValue(dest, value);
                    return true;
                }

                if (info.FieldType == typeof(bool))
                {
                    info.SetValue(dest, ParseBool(value));
                    return true;
                }

                if (info.FieldType == typeof(int))
                {
                    int intVal;
                    if (int.TryParse(value, out intVal))
                    {
                        info.SetValue(dest, intVal);
                        return true;
                    }
                }

                if (info.FieldType == typeof(long))
                {
                    long longVal;
                    if (long.TryParse(value, out longVal))
                    {
                        info.SetValue(dest, longVal);
                        return true;
                    }
                }
            }
            catch (Exception) { }

            return false;
        }

        private static bool ParseBool(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return false;
            }

            switch (val.ToLower())
            {
                case "1":
                case "yes":
                case "true":
                case "yep":
                    return true;
            }

            return false;
        }



        public class DotObjectNotationResult
        {
            public Queue<string> PathElements
            {
                get;
                set;
            }

            public string Value
            {
                get;
                set;
            }
        }
    }
}
