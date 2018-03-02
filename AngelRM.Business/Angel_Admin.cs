using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AngelRM;
using AngelRM.Core;

namespace AngelRM.Business
{
    
    public class Angel_Admin
    {
        AngelData Adal = new AngelData("Angel_Admin", "Angel_Admin");

        public Angel_Admin()
        {
        }
        #region 基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string id)
        {
            return Adal.ObjectExistById(id);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.Angel_Admin model)
        {
            return Adal.InsertObject<Model.Angel_Admin>(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string id)
        {
            return Adal.DeleteObjectById(id);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Angel_Admin model)
        {
            return Adal.UpdateObject<Model.Angel_Admin>(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Angel_Admin GetModel(string id)
        {
            return Adal.GetObject_ById<Model.Angel_Admin>(id);
        }

        /// <summary>
        /// 用户名和密码返回实体
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Model.Angel_Admin GetModel(string UserName, string Password)
        {
            string Md5string = AngelDESEncrypt.Encrypt(Password);
            Adal.SetConditionString(string.Format(" LoginName='{0}' and Password='{1}'", UserName, Md5string));
            if (Adal.Condition_DataExist())
            {
                DataTable table = Adal.GetDataTable();
                Model.Angel_Admin model = new Model.Angel_Admin();
                foreach (DataRow row in table.Rows)
                {
                    if (row["ID"] != null && row["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(row["ID"].ToString());
                    }
                    if (row["RoleId"] != null && row["RoleId"].ToString() != "")
                    {
                        model.RoleId = int.Parse(row["RoleId"].ToString());
                    }
                    if (row["LoginName"] != null)
                    {
                        model.LoginName = row["LoginName"].ToString();
                    }
                    if (row["Password"] != null)
                    {
                        model.Password = row["Password"].ToString();
                    }
                    if (row["UserName"] != null)
                    {
                        model.UserName = row["UserName"].ToString();
                    }
                    if (row["UserEmail"] != null)
                    {
                        model.UserEmail = row["UserEmail"].ToString();
                    }
                    if (row["IsWorking"] != null && row["IsWorking"].ToString() != "")
                    {
                        model.IsWorking = int.Parse(row["IsWorking"].ToString());
                    }
                    if (row["AddTime"] != null && row["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(row["AddTime"].ToString());
                    }

                }
                return model;


            }
            else
                return null;
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int topNumber, string sqlWhere, string OrderField)
        {
            if (sqlWhere.Trim() != "")
            {
            Adal.AddConditionString(sqlWhere + string.Format(" order by {0}", OrderField));
            }
            int total = Adal.GetDataSetPage(1, topNumber);
            DataSet ds = Adal.ds;
            return ds;
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageIndex, int pageSize, string sqlWhere, string OrderField, out int total)
        {
            if (sqlWhere.Trim() != "")
            {
                Adal.AddConditionString(sqlWhere + string.Format(" order by {0}", OrderField));
            }
             total = Adal.GetDataSetPage(pageIndex, pageSize);
             DataSet ds = Adal.ds;
            return ds;
        }

        #endregion  Method


    }
}
