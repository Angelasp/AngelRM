using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AngelRM;
using AngelRM.Core;

namespace AngelRM.Business
{
    public class Angel_Admin_Navigation
    {
        AngelData Adal = new AngelData("Angel_Admin_Navigation", "Angel_Admin_Navigation");
        public Angel_Admin_Navigation()
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
        public bool Add(Model.Angel_Admin_Navigation model)
        {
            return Adal.InsertObject<Model.Angel_Admin_Navigation>(model);
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
        public bool Update(Model.Angel_Admin_Navigation model)
        {
            return Adal.UpdateObject<Model.Angel_Admin_Navigation>(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Angel_Admin_Navigation GetModel(string id)
        {
            return Adal.GetObject_ById<Model.Angel_Admin_Navigation>(id);
        }

        /// <summary>
        /// 获取所有导航信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableTableAll()
        {
            Adal.SetConditionAll();
            Adal.AddConditionString(" ViewFlag='1' order by Sequence asc ,id asc ");
            DataTable table = Adal.GetDataTable();
            return table;
        }

        /// <summary>
        /// 获取栏目操作权限
        /// </summary>
        /// <param name="NavName"></param>
        /// <returns></returns>
        public string GetOperationValueString(string NavName)
        {
            Adal.SetConditionString(" NavName='" + NavName + "' and ViewFlag='1' ");
            string liststring = Adal.GetDisrinctColValue("Operation_Value")[0];
            return liststring;
        }

        /// <summary>
        /// 根据父类ID查询导航返回DataTable
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetList(int ParentID)
        {
            Adal.AddConditionString(" ParentID=" + ParentID + " order by Sequence asc ,id asc ");
            DataTable table = Adal.GetDataTable();
            return table;
        }
        /// <summary>
        /// 查询导航代码是否存在
        /// </summary>
        /// <param name="NavlistName"></param>
        /// <returns></returns>
        public bool IsNavNameDataExist(string NavlistName)
        {
            Adal.SetConditionAll();
            Adal.AddConditionString(" NavName='" + NavlistName + "'");
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
        #endregion ===方法结束
    }
}
