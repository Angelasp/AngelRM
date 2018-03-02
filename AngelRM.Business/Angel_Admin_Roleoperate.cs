using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AngelRM;
using AngelRM.Core;

namespace AngelRM.Business
{
    public class Angel_Admin_Roleoperate
    {
        AngelData Adal = new AngelData("Angel_Admin_Roleoperate", "Angel_Admin_Roleoperate");
        public Angel_Admin_Roleoperate()
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
        public bool Add(Model.Angel_Admin_Roleoperate model)
        {
            return Adal.InsertObject<Model.Angel_Admin_Roleoperate>(model);
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
        public bool Update(Model.Angel_Admin_Roleoperate model)
        {
            return Adal.UpdateObject<Model.Angel_Admin_Roleoperate>(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Angel_Admin_Roleoperate GetModel(string id)
        {
            return Adal.GetObject_ById<Model.Angel_Admin_Roleoperate>(id);
        }

        /// <summary>
        /// 查询权限表该角色是否有权限数据
        /// </summary>
        /// <param name="NavlistName">栏目名称</param>
        /// <param name="Roleid">角色ID</param>
        /// <returns></returns>
        public bool IsRoleoperateDataExist(string NavlistName, int Roleid)
        {
            Adal.SetConditionString(" NavIdName='" + NavlistName + "' and RoleId=" + Roleid + "");
            if (Adal.Condition_DataExist())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询权限表该角色是否有可用权限
        /// </summary>
        /// <param name="NavlistName">栏目名称</param>
        /// <param name="Roleid">角色ID</param>
        /// <returns></returns>
        public bool IsOkRoleoperateDataExist(string NavlistName, int Roleid)
        {
            Adal.SetConditionString(" NavIdName='" + NavlistName + "' and RoleId=" + Roleid + " and IsView=1");
            if (Adal.Condition_DataExist())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获得对象
        /// </summary>
        /// <param name="NavlistName"></param>
        /// <param name="Roleid"></param>
        /// <returns></returns>
        public Model.Angel_Admin_Roleoperate GetModelWhere(string NavlistName, int Roleid)
        {
            Adal.SetConditionString(" NavIdName='" + NavlistName + "' and RoleId=" + Roleid + "");
            return Adal.GetObject_ByCondition<Model.Angel_Admin_Roleoperate>();
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

        /// <summary>
        /// 根据条件查询所有操作栏目
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAllDataTable(string sqlWhere)
        {
            if (sqlWhere.Trim() != "")
            {
                Adal.SetConditionString(sqlWhere);
                DataTable dt = Adal.GetDataTable();
                return dt;
            }
            else
            {
                return null;
            }

        }

        #endregion  Method
    }
}
