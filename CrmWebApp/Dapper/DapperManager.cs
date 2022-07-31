using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using RmosCrmWebApp.Models.EntityClasses;

namespace RmosCrmWebApp.Dapper
{
    public class DapperManager
    {
        /// <summary>
       
        /// DEMORMOS yerine local databaseye bağlanması için ayarlayınız.
        /// </summary>
        /// <returns></returns>
        public IDapperTools DapperBaseConnect()
        {
            string conString = @"Server=exp1;Database=exp2;User id=exp3;password=exp4";
            IDapperContext ctx = new DapperContext(conString);
            IDapperTools machine = new DapperTools(ctx);
            return machine;
        }        
        
        /// <summary>
        /// İstenen veri tabanına bağlanır.
        /// </summary>
        /// <param name="connection_string"></param>
        /// <returns></returns>
        public IDapperTools DapperConnect(string connection_string)
        {
            IDapperContext ctx = new DapperContext(connection_string);
            IDapperTools machine = new DapperTools(ctx);
            return machine;
        }      
        
        public IDapperTools HotelCodeConnect(string code)
        {
            IDapperContext ctx = new DapperContext(code);
            IDapperTools machine = new DapperTools(ctx);
            return machine;
        }

    }
}