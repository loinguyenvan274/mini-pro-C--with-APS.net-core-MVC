using System;
using Web_1.Controllers;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Web_1.Models; // Assuming Account is in the Models namespace

namespace Web_1.Service
{
    public class MainService
    {
        
        private Dictionary<MyCookie, Account> _Accounts = new Dictionary<MyCookie,Account>();
        private static MainService _instance = null;
        public static MainService getInstance(){
            if (_instance == null)
            {
                _instance = new MainService();
            }
            return _instance;
        }

        public void addAcount(){

        }

        
    }
}