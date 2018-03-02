using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AngelRM;
using AngelRM.Core;

namespace AngelRM.Business
{
    public class Angel_Admin_Role
    {
        AngelData Adal = new AngelData("Angel_Admin_Role", "Angel_Admin_Role");

        public Angel_Admin_Role()
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
        public bool Add(Model.Angel_Admin_Role model)
        {
            return Adal.InsertObject<Model.Angel_Admin_Role>(model);
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
        public bool Update(Model.Angel_Admin_Role model)
        {
            return Adal.UpdateObject<Model.Angel_Admin_Role>(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Angel_Admin_Role GetModel(string id)
        {
            return Adal.GetObject_ById<Model.Angel_Admin_Role>(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Angel_Admin_Role GetAllModel(string id)
        {
            return Adal.GetObject_ById<Model.Angel_Admin_Role>(id);
        }


        /// <summary>
        /// 获得所有角色
        /// </summary>
        public DataSet GetAllList(string sqlWhere)
        {
            if (sqlWhere.Trim() != "")
            {
                Adal.SetConditionString(sqlWhere);
            }
            Adal.GetDataset();
            DataSet ds = Adal.ds;
            return ds;
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
        public DataSet GetList(int pageIndex, int pageSize, string sqlWhere, out int total)
        {
            if (sqlWhere.Trim() != "")
            {
                Adal.SetConditionString(sqlWhere);
            }
            total = Adal.GetDataSetPage(pageIndex, pageSize);
            DataSet ds = Adal.ds;
            return ds;
        }

        #endregion  Method
    }
}
