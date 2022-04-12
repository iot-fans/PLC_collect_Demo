using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fins.Utility
{
    /// <summary>
    /// 全局静态对象类
    /// 只有在使用实例/参数不切实际的情况下才应使用全局变量！
    /// 确保一次只有一个线程可以访问全局（静态）变量
    /// </summary>
    public static class GlobalShareObjs
    {
        private static Dictionary<string, object> shareObjs = new Dictionary<string, object>();

        private static object instance = new object();
        /// <summary>
        /// 全局静态实例
        /// </summary>
        public static object Instance { get { return instance; } }

        //return successful or not
        public static bool AddObj(string objName, object obj)
        {
            if (obj == null)
            {
                return false;
            }
            lock (instance)
            {
                if (!shareObjs.ContainsKey(objName))
                {
                    shareObjs[objName] = obj;
                    return true;
                }
            }
            return false;
        }

        public static bool HasObj(string objName)
        {
            lock (instance)
            {
                if (shareObjs.ContainsKey(objName))
                {
                    return true;
                }
                return false;
            }
        }

        public static void DelObj(string objName)
        {
            lock (instance)
            {
                if (shareObjs.ContainsKey(objName))
                {
                    shareObjs.Remove(objName);
                }
            }
        }

        public static object GetObj(string objName)
        {
            lock (instance)
            {
                if (!shareObjs.ContainsKey(objName))
                {
                    return null;
                }
                return shareObjs[objName];
            }
        }
    }
}
