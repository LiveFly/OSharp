﻿// -----------------------------------------------------------------------
//  <copyright file="SqlServerDbContextOptionsBuilderCreator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-08-21 1:07</last-date>
// -----------------------------------------------------------------------

using System;
using System.Data.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace OSharp.Entity.SqlServer
{
    /// <summary>
    /// SqlServer的<see cref="DbContextOptionsBuilder"/>数据库驱动差异处理器
    /// </summary>
    public class DbContextOptionsBuilderDriveHandler : IDbContextOptionsBuilderDriveHandler
    {
        private readonly ILogger _logger;

        /// <summary>
        /// 初始化一个<see cref="DbContextOptionsBuilderDriveHandler"/>类型的新实例
        /// </summary>
        public DbContextOptionsBuilderDriveHandler(IServiceProvider provider)
        {
            _logger = provider.GetLogger(typeof(DbContextOptionsBuilderDriveHandler));
        }

        /// <summary>
        /// 获取 数据库类型名称，如 SQLSERVER，MYSQL，SQLITE等
        /// </summary>
        public DatabaseType Type { get; } = DatabaseType.SqlServer;

        /// <summary>
        /// 处理<see cref="DbContextOptionsBuilder"/>驱动差异
        /// </summary>
        /// <param name="builder">创建器</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="existingConnection">已存在的连接对象</param>
        /// <returns></returns>
        public DbContextOptionsBuilder Handle(DbContextOptionsBuilder builder, string connectionString, DbConnection existingConnection)
        {
            if (existingConnection == null)
            {
                _logger.LogDebug($"使用新连接“{connectionString}”应用SqlServer数据库");
                return builder.UseSqlServer(connectionString);
            }
            _logger.LogDebug($"使用已存在的连接“{existingConnection.ConnectionString}”应用SqlServer数据库");
            return builder.UseSqlServer(existingConnection);
        }
    }
}