﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Framework.Common.Redis
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：刘晓雷
    /// 日 期：2016.04.28 10:45
    /// 描 述：定义缓存接口
    /// </summary>
    public class Cache : ICache
    {
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            return RedisCache.Get<T>(cacheKey);
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            //RedisCache.Set(cacheKey, value);
            //配置成与webcache相同时间
            WriteCache(value, cacheKey, DateTime.Now.AddMinutes(10));
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            RedisCache.Set(cacheKey, value, expireTime);
        }


        //如果缓存里没有，则取数据然后缓存起来
        public F Get<F>(string key, Func<F> getRealData) where F : class
        {
            var getDataFromCache = new Func<F>(() =>
            {
                F data = default(F);
                var cacheData = new Cache().GetCache<F>(key);
                if (cacheData == null)
                {
                    data = getRealData();

                    if (data != null)
                        new Cache().WriteCache<F>(data, key);
                }
                else
                {
                    data = (F)cacheData;
                }

                return data;
            });

            return getRealData();

        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveCache(string cacheKey)
        {
            RedisCache.Remove(cacheKey);
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveCache()
        {
            RedisCache.RemoveAll();
        }
    }
}