using System;
using System.Collections;
using System.Web;

namespace Core.Common
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.9 10:45
    /// 描 述：缓存操作
    /// </summary>
    public class Cache : ICache
    {
        private static System.Web.Caching.Cache cache = HttpRuntime.Cache;

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            if (cache[cacheKey] != null)
            {
                return (T)cache[cacheKey];
            }
            return default(T);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            cache.Insert(cacheKey, value, null, DateTime.Now.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            cache.Insert(cacheKey, value, null, expireTime, System.Web.Caching.Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveCache(string cacheKey)
        {
            cache.Remove(cacheKey);
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveCache()
        {
            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                cache.Remove(CacheEnum.Key.ToString());
            }
        }

        //如果缓存里没有，则取数据然后缓存起来
        public  F Get<F>(string key, Func<F> getRealData) where F : class
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

            return GetItem<F>(key, getDataFromCache);
        }


        #region 以下几个方法从HttpContext.Items缓存页面数据，适合页面生命周期，页面载入后就被移除，而非HttpContext.Cache在整个应用程序都有效
        //如果上下文HttpContext.Current.Items里没有，则取数据然后加入Items，在页面生命周期内有效
        public static F GetItem<F>(string name, Func<F> getRealData)
        {
            if (HttpContext.Current == null)
                return getRealData();

            var httpContextItems = HttpContext.Current.Items;
            if (httpContextItems.Contains(name))
            {
                return (F)httpContextItems[name];
            }
            else
            {
                var data = getRealData();
                if (data != null)
                    httpContextItems[name] = data;
                return data;
            }
        }

        public static F GetItem<F>() where F : new()
        {
            return GetItem<F>(typeof(F).ToString(), () => new F());
        }

        public static F GetItem<F>(Func<F> getRealData)
        {
            return GetItem<F>(typeof(F).ToString(), getRealData);
        }
        #endregion


    }
}
