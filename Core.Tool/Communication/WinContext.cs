//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tool
{
    public class WinContext
    {
        /// <summary>
        /// 保存页面跳转集合
        /// </summary>
        public static Hashtable Hash_PageUrl = new Hashtable();

        /// <summary>
        /// 表达式集合
        /// </summary>
        public static ArrayList ArrList = new ArrayList();

        public static void Clear()
        {
            ArrList = new ArrayList();
            Hash_PageUrl = new Hashtable();
        }
    }
}
