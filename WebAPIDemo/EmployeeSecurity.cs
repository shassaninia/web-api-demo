﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace WebAPIDemo
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (var entities = new EmployeesDBEntities())
            {
                return entities.Users.Any(user => user.Username.Equals(username,
                    StringComparison.OrdinalIgnoreCase) && user.Password == password);
                                                   
            }
        }
    }
}