using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace H2Service.Web.Helpers
{
    public class ConvertHelper
    {
        public static DataTable ListToTable<T>(List<T> list, bool isStoreDB = true)
        {
            Type tp = typeof(T);
            PropertyInfo[] proInfos = tp.GetProperties();
            DataTable dt = new DataTable();
            foreach (var item in proInfos)
            {
                if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Type[] typeArray = item.PropertyType.GetGenericArguments();
                    Type baseType = typeArray[0];
                    dt.Columns.Add(item.Name, baseType);
                }
                else
                dt.Columns.Add(item.Name, item.PropertyType); //添加列明及对应类型
            }
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                foreach (var proInfo in proInfos)
                {
                    object obj = proInfo.GetValue(item);
                    if (obj == null)
                    {
                        continue;
                    }
                    //if (obj != null)
                    // {
                    if (isStoreDB && proInfo.PropertyType == typeof(DateTime) && Convert.ToDateTime(obj) < Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    // dr[proInfo.Name] = proInfo.GetValue(item);
                    dr[proInfo.Name] = obj;
                    // }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


    }
}