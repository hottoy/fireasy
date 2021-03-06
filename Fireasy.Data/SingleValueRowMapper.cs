﻿// -----------------------------------------------------------------------
// <copyright company="Fireasy"
//      email="faib920@126.com"
//      qq="55570729">
//   (c) Copyright Fireasy. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Data;
using Fireasy.Common.Extensions;
using Fireasy.Data.Converter;
using Fireasy.Data.Extensions;
using Fireasy.Data.RecordWrapper;

namespace Fireasy.Data
{
    /// <summary>
    /// 一个将数据行转换为单一数值或字符串类型的映射器。
    /// </summary>
    /// <typeparam name="T">要转换的类型。该类型一定是实现了 <see cref="IConvertible"/> 或 <see cref="IValueConverter"/> 接口。</typeparam>
    public class SingleValueRowMapper<T> : IDataRowMapper<T>
    {
        /// <summary>
        /// 将一个 <see cref="IDataReader"/> 转换为一个 <typeparamref name="T"/> 的对象。
        /// </summary>
        /// <param name="reader">一个 <see cref="IDataReader"/> 对象。</param>
        /// <returns>由当前 <see cref="IDataReader"/> 对象中的数据转换成的 <typeparamref name="T"/> 对象实例。</returns>
        public virtual T Map(IDataReader reader)
        {
            var index = reader.FieldCount != 1 ? 1 : 0;
            var value = RecordWrapper == null ? reader[index] : 
                RecordWrapper.GetValue(reader, index);

            var converter = ConvertManager.GetConverter(typeof(T));
            if (converter != null)
            {
                return (T)converter.ConvertFrom(value, reader.GetFieldType(index).GetDbType());
            }

            return value.To<object, T>();
        }

        /// <summary>
        /// 将一个 <see cref="DataRow"/> 转换为一个 <typeparamref name="T"/> 的对象。
        /// </summary>
        /// <param name="row">一个 <see cref="DataRow"/> 对象。</param>
        /// <returns>由 <see cref="DataRow"/> 中数据转换成的 <typeparamref name="T"/> 对象实例。</returns>
        public virtual T Map(DataRow row)
        {
            var index = row.Table.Columns.Count != 1 ? 1 : 0;
            var converter = ConvertManager.GetConverter(typeof(T));
            if (converter != null)
            {
                return (T)converter.ConvertFrom(row[index], row.Table.Columns[index].DataType.GetDbType());
            }

            return row[index].To<object, T>();
        }

        /// <summary>
        /// 获取或设置 <see cref="IRecordWrapper"/>。
        /// </summary>
        public IRecordWrapper RecordWrapper { get; set; }

        /// <summary>
        /// 获取或设置对象的初始化器。
        /// </summary>
        public Action<object> Initializer { get; set; }

        object IDataRowMapper.Map(IDataReader reader)
        {
            return Map(reader);
        }

        object IDataRowMapper.Map(DataRow row)
        {
            return Map(row);
        }
    }
}
