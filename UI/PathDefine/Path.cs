using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace UI.PathDefine
{
    public static class Path
    {
        public readonly static string HOME_INDEX_PATH = "/IndexHome";
        
        public readonly static string JEWELRY_INDEX_PATH = "/Jewelries/Index";
       
        public readonly static string MATERIAL_METAL_INDEX_PATH = "/Materials/Metals/Index";
        public readonly static string MATERIAL_GEM_INDEX_PATH = "/Materials/Gems/Index";
       
        public readonly static string WARRANTY_INDEX_PATH = "/Warranties/Index";
		public readonly static string WARRANTY_ACTIVATE_PATH = "/Warranties/Create";
		public readonly static string WARRANTY_FIX_REQUEST_INDEX_PATH = "/FixRequests/Index";
        
        public readonly static string ACCOUNT_LOGIN_PATH = "/Login";
        public readonly static string ACCOUNT_LOGOUT = "/Logout";
		public readonly static string ACCOUNT_INDEX_PATH = "/Accounts/Index";
		
        public readonly static string PROMOTION_INDEX_PATH = "/Promotions/Index";
        
        public readonly static string TRANSACTION_INDEX_SELL_PATH = "/Orders/Sell/Index";
        public readonly static string TRANSACTION_INDEX_BUY_PATH = "/Orders/Buy/Index";
        public readonly static string TRANSACTION_BUY_PATH = "/Orders/BuyOld/Create";
		public readonly static string TRANSACTION_SELL_PATH = "/Orders/Sell/Create";

		public readonly static string DASHBOARD_INDEX_PATH = "/Dashboard";
	}
}
