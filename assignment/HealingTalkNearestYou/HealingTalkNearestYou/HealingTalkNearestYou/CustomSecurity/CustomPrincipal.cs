using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace HealingTalkNearestYou.CustomSecurity
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string userEmail)
        {
            Identity = new GenericIdentity(userEmail);
        }

        public IIdentity Identity
        {
            get;
            private set;
        } 

        public bool IsInRole(string role)
        {
            if (role == UserType)
                return true;
            return false;
        }

        public string UserType { get; set; }
    }
}