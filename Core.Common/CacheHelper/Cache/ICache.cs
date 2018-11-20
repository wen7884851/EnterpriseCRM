﻿using System;

namespace Framework.Common
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.9 10:45
    /// 描 述：定义缓存接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        T GetCache<T>(string cacheKey) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        void WriteCache<T>(T value, string cacheKey) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class;

        /// <summary>
        /// 如果缓存里没有，则取数据然后缓存起来
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <param name="key">键</param>
        /// <param name="getRealData">没有缓存的时候获取数据的方式</param>
        /// <returns></returns>
        F Get<F>(string key, Func<F> getRealData) where F : class;
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        void RemoveCache(string cacheKey);
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        void RemoveCache();
    }
}
