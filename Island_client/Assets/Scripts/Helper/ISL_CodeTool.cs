using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL_Helper
{

    //用于简单创建instance
    public class ISL_Instance<T>  where T:class,new()
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if(s_instance == null)
                {
                    s_instance = new T();
                }
                return s_instance;
            }

        }
    }




}
