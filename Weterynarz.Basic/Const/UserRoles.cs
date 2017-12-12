using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Basic.Const
{
    public static class UserRoles
    {
        public const string Admin = "admin";
        public const string Doctor = "doctor";
        public const string Worker = "worker";
        public const string Client = "client";

        public static string[] GetUserRolesList()
        {
            return new string[] { Admin, Doctor, Worker, Client };
        }

        public static string TranslateRole(string role)
        {
            switch(role)
            {
                case Admin:
                    return ResAdmin.userRoles_admin;
                case Doctor:
                    return ResAdmin.userRoles_doctor;
                case Worker:
                    return ResAdmin.userRoles_worker;
                case Client:
                    return ResAdmin.userRoles_client;
                default:
                    return string.Empty;
            }
        }
    }
}
