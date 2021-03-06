﻿using Shop_Nhi.Common;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class UserDAO
    {
        private DBConnect db = null;

        public UserDAO()
        {
            db = new DBConnect();
        }

        //GET LIST
        public List<User> ListUsers()
        {
            return db.Users.ToList();
        }

        //Đăng nhập
        public int Login(string username, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.userName == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }

        //find ID
        public User GetByID(long id)
        {
            return db.Users.Find(id);
        }

        //Get by Username
        public User GetByUsername(string username)
        {
            return db.Users.SingleOrDefault(x => x.userName == username);
        }

        //Get By Email
        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.email == email);
        }

        public void Save(User user)
        {
            var result = db.Users.Find(user.ID);
            if(result== null || user.ID == 0)
            {
                user.ID = -1;
                user.password = Encryptor.MD5Hash(user.password.Trim().Replace(" ", ""));
                user.status = true;
                db.Users.Add(user);
            }else
            {
                result.fullname = user.fullname;
                result.roleID = user.roleID;
            }
            db.SaveChanges();
        }
        //Delete
        public bool Delete(long id)
        {
           try
            {
                var result = db.Users.Find(id);
                db.Users.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        //Check username
        public bool CheckUsername(string username)
        {
            return db.Users.Count(x => x.userName == username) > 0;
        }

        //Check Email
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.email == email) > 0;
        }


        //Check Password
        public bool ChekcPassword(string password)
        {
            return db.Users.Count(x => x.password == password) > 0;
        }

        //Change Password
        public bool ChangePassword(User user)
        {
            try
            {
                var result = db.Users.Find(user.ID);
                result.password = user.password;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Change Status
        public bool ChangeStatus(long id)
        {
            var result = db.Users.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }

        //GET ROLE
        public List<Role> ListRole()
        {
            return db.Roles.ToList();
        }
    }
}
