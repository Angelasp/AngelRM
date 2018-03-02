using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using AngelRM.DBHelper;

namespace AngelRM.Core
{ 
  
   public class AngelData
    {
    /// <summary>
    /// 实现按对象进行数据库增、删、改查
    /// 对象类和表映射。
    /// 类的各个属性和表的列标题需要设为一样。
    /// 1、初始化指定 表名              AngelData data = new AngelData(tableName)
    /// 2、初始化指定 类名              data.GetObjIdList<objClassName>();
    /// 3、设定数据操作条件             data.SetCondition(str_ColName, ColValue);
    /// 4、执行查询操作返回对象         obj = data.GetObject_ByCondition<objClassName>();
    /// 5、获得对象的值列表用于增删改   GetObjValueList<objClassName>(TheObj)
    /// </summary>
        #region 类构造
        //成员变量
        public string tablename;                //表名
        public string ConditionStr;

        public List<string> lst_colId;          //属性名称列表
        public List<string> lst_Value;          //值列表
        public List<List<string>> ll_data;      //数据表
        public DataSet ds;                      //数据集
        public String strDsJson;                //数据集的Json字符串
        public DBHelperSql Item_db;             //数据库访问对象


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName">表名</param>
        public AngelData(string tableName)
        {
            this.tablename = tableName;
            ConditionStr = "";
            //UI
            lst_colId = new List<string>();
            //数据
            lst_Value = new List<string>();
            ll_data = new List<List<string>>();
            ds = new DataSet();
            this.strDsJson = "";
            //DB对象
            this.Item_db = new DBHelperSql();
        }
        /// <summary>
        /// 构造函数2
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ModelClassName">对象类名</param>
        /// 
        public AngelData(string tableName, string ModelClassName)
        {
            if ((tableName == "") || (tableName == null)) return;
            if ((ModelClassName == "") || (ModelClassName == null)) return;
            this.tablename = tableName;
            ConditionStr = "";
            //UI
            lst_colId = new List<string>();
            //数据
            lst_Value = new List<string>();
            ll_data = new List<List<string>>();
            ds = new DataSet();
            this.strDsJson = "";
            //DB对象
            this.Item_db = new DBHelperSql();
            //获得属性名称列表
            this.GetObjIdList(ModelClassName);
        }
        /// <summary>
        /// 构造函数3
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ModelClassName">对象类名</param>
        /// 
        public AngelData(string tableName, string ModelClassName, DBHelperSql dd)
        {
            if ((tableName == "") || (tableName == null)) return;
            if ((ModelClassName == "") || (ModelClassName == null)) return;
            this.tablename = tableName;
            ConditionStr = "";
            //UI
            lst_colId = new List<string>();
            //数据
            lst_Value = new List<string>();
            ll_data = new List<List<string>>();
            ds = new DataSet();
            this.strDsJson = "";
            //DB对象
            this.Item_db = dd;
            //获得属性名称列表
            this.GetObjIdList(ModelClassName);
        }

        #endregion
        #region 对象操作
        // 通过Id(第一个属性）判断DB中对象是否存在
        // 按条件检索出一个对象
        // 按条件修改一个对象
        // 按条件删除一个对象
        // 添加一个新对象

        /// <summary>
        /// 用Json字符串更新对象
        /// </summary>
        /// <typeparam name="T">对戏类</typeparam>
        /// <param name="strJson_obj2Update">Json数据</param>
        /// <returns>是否成功</returns>
        public bool UpdateObjectByJson<T>(string strJson_obj2Update)
        {
            //用Json给值列表赋值
            T obj2Update = Angel_DataJson.json2Object<T>(strJson_obj2Update);
            GetObjValueList<T>(obj2Update);
            //设定条件
            SetCondition(lst_colId[0], lst_Value[0]);
            //更新DB
            return UpdateData();
        }
        /// <summary>
        ///  获取数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            this.Item_db = new DBHelperSql();
            return Item_db.GetDataTable(tablename, ConditionStr);
        }

        /// <summary>
        /// 用Json字符串更新对象
        /// </summary>
        /// <param name="strJson_obj2Update"></param>
        /// <returns></returns>
        /// 
        public bool UpdateObjectByJson(string strJson_obj2Update)
        {
            //用Json给值列表赋值
            //T obj2Update = C_xJson.json2Object<T>(strJson_obj2Update);
            //GetObjValueList<T>(obj2Update);
            Angel_DataJson.getValuelistByJsonString(strJson_obj2Update, lst_colId, out lst_Value);
            //设定条件
            SetCondition(lst_colId[0], lst_Value[0]);
            //更新DB
            return UpdateData();
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="T">对象类</typeparam>
        /// <param name="obj2Update">更新对象</param>
        /// <returns>是否成功</returns>
        public bool UpdateObject<T>(T obj2Update)
        {
            //用对象给值列表赋值
            GetObjValueList<T>(obj2Update);
            //设定条件
            SetCondition(lst_colId[0], lst_Value[0]);
            //更新DB
            return UpdateData();
        }
        /// <summary>
        /// 用Json字符串更新对象
        /// </summary>
        /// <typeparam name="T">对戏类</typeparam>
        /// <param name="strJson_obj2Update">Json数据</param>
        /// <returns>是否成功</returns>
        public bool InsertObjectByJson<T>(string strJson_obj2Insert)
        {
            //用Json给值列表赋值
            T obj2Insert = Angel_DataJson.json2Object<T>(strJson_obj2Insert);
            GetObjValueList<T>(obj2Insert);
            //设定条件
            SetCondition(lst_colId[0], lst_Value[0]);
            //更新DB
            return InsertData();
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="strJson_obj2Insert"></param>
        /// <returns></returns>
        public bool InsertObjectByJson(string strJson_obj2Insert)
        {
            //用Json给值列表赋值
            //T obj2Insert = C_xJson.json2Object<T>(strJson_obj2Insert);
            //GetObjValueList<T>(obj2Insert);
            Angel_DataJson.getValuelistByJsonString(strJson_obj2Insert, lst_colId, out lst_Value);
            //设定条件
            SetCondition(lst_colId[0], lst_Value[0]);
            //更新DB
            return InsertData();
        }

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="Obj2Insert">要插入的对象</param>
        /// <returns>是否成功</returns>
        public bool InsertObject<T>(T Obj2Insert)
        {
            //用对象给值列表赋值
            GetObjValueList<T>(Obj2Insert);
            //设定条件
            // SetCondition(lst_colId[0], lst_Value[0]);
            //插入数据
            return InsertData();
        }
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="Obj2Insert">要插入的对象</param>
        /// <returns>是否成功</returns>
        public string InsertObjectGet_id<T>(T Obj2Insert)
        {
            //用对象给值列表赋值
            GetObjValueList<T>(Obj2Insert);
            //设定条件
            // SetCondition(lst_colId[0], lst_Value[0]);
            //插入数据
            //return InsertData();
            return this.InsertDataGet_id();
        }

        /// <summary>
        /// 对象在DB中是否存在
        /// </summary>
        /// <param name="str_id">字段名称必须为"id",此处为id值</param>
        /// <returns></returns>
        public bool ObjectExistById(string str_id)
        {
            SetCondition("id", str_id);
            return Condition_DataExist();
        }
        /// <summary>
        /// 判断表数据的某个字段中，是否存在某值，如：客户表中，客户代码'C0001'是否存在
        /// （表名为对象表名）
        /// </summary>
        /// <param name="ID_Name">ID字段名称</param>
        /// <param name="ID_Value">ID字段值</param>
        /// <returns></returns>
        public bool ID_ValueExist(string ID_Name, string ID_Value)
        {
            SetCondition(ID_Name, ID_Value);
            return Condition_DataExist();
        }

        /// <summary>
        /// 自动生成明细编号,3位数字字符串（"001","002"...）
        /// 调用前，设定条件，如，自动获得订单明细ID时，以订单号为条件
        /// </summary>
        /// <param name="ID_Name">ID字段名称("Line_ID")</param>
        /// <returns>明细编号</returns>
        public string autoGenerateLineID(string ID_Name)
        {
            string str_ID;  //返回ID值
            //-----------------------------得到最大ID号------------------------------
            List<string> l_id = GetWholeColValue(ID_Name);   //ID列表
            int lid_Count = l_id.Count;
            if (lid_Count == 0)
                str_ID = "1";
            else
            {
                l_id.Sort();    //ID排序
                //顺序号
                string str_lastIdNumber = l_id[lid_Count - 1];
                if (!isnumber(str_lastIdNumber)) return "";
                int intID = int.Parse(str_lastIdNumber);  //整型顺序号
                str_ID = (intID + 1).ToString();

            }
            //-----------------------------------------------------------------------
            //生成完整ID
            int i_idLength = str_ID.Length;
            if (i_idLength > 3) return "";
            int i_fill0Length = 3 - i_idLength;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < i_fill0Length; i++) sb.Append("0");     //中间补0
            sb.Append(str_ID);                                          //后面是顺序号

            return sb.ToString();
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="str_id">字段名称必须为"id",此处为id值</param>
        /// <returns>是否成功</returns>
        public bool DeleteObjectById(string str_id)
        {
            SetCondition("id", str_id);
            return DeleteData();
        }
        /// <summary>
        /// 删除多个对象
        /// </summary>
        /// <param name="str_JsonIdList">["id1","id2"..."idn"]</param>
        /// <returns>是否成功</returns>
        public bool DeleteObjectsById(string str_JsonIdList)
        {
            bool success = true;
            List<string> l_str_ids;
            l_str_ids = Angel_DataJson.json2ObjList<string>(str_JsonIdList);
            for (int i = 0; i < l_str_ids.Count; i++)
            {
                this.SetCondition("id", l_str_ids[i]);
                if (!this.DeleteData())
                    return false;
            }
            return success;
        }
        /// <summary>
        /// 修改多个对象的某个属性
        /// </summary>
        /// <param name="str_JsonIdList">["id1","id2"..."idn"]</param>
        /// <returns></returns>
        public bool setObjectsAttributById(string str_JsonIdList, string s_attr, string s_value)
        {
            bool success = true;
            List<string> l_str_ids;
            l_str_ids = Angel_DataJson.json2ObjList<string>(str_JsonIdList);
            for (int i = 0; i < l_str_ids.Count; i++)
            {
                if (!setObjectAttributById(l_str_ids[i], s_attr, s_value))
                    return false;
            }
            return success;
        }
        //
        // 修改对象的某个属性
        //
        public bool setObjectAttributById(string s_id, string s_attr, string s_value)
        {
            return Item_db.UpdateColById(this.tablename, "id", s_id, s_attr, s_value);
        }
        /// <summary>
        ///  得到对象列表
        /// </summary>
        /// <typeparam name="T"> 对象类</typeparam>
        /// <param name="str_Condition">条件</param>
        /// <returns>对象列表</returns>
        public List<T> getObjectListByCondition<T>(string str_Condition)
        {
            this.strDsJson = "";
            // 得到数据集
            DataSet ds = Item_db.GetDataSetByCondition(this.tablename, this.lst_colId, str_Condition);
            // 数据集转Json
            if (!(ds == null || ds.Tables.Count == 0))
                strDsJson = Angel_DataJson.dataSet2Json(ds);
            // 去掉Json串中的表名
            // string strMatch = @"^{.*?:";
            Regex rx = new Regex(@"^{.*?:", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (rx.IsMatch(strDsJson))
                strDsJson=rx.Replace(strDsJson,"");

            return Angel_DataJson.json2ObjList<T>(strDsJson);
        }

        #endregion
        #region 对象映射
        /// <summary>
        /// 对象是否存在
        /// </summary>
        /// <param name="ModelClassName"></param>
        /// <returns></returns>
        public bool isObjExist(string ModelClassName)
        {
            //  AngelRM.Model., AngelRM.Model


            string typeFullName = "AngelRM.Model." + ModelClassName + ", AngelRM.Model";
            Type type = Type.GetType(typeFullName);
            if (type == null) return false;
            //创建对象实例
            object obj = Activator.CreateInstance(type);
            if (obj == null) return false;
            return true;
        }
        /// <summary>
        /// 获得对象的属性名称列表,赋值给内部列表变量lst_colId
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        // public void GetObjIdList<T>()
        public void GetObjIdList(string ModelClassName)
        {
            if (!isObjExist(ModelClassName)) return;
            //清空属性列表
            this.lst_colId.Clear();
            //获取对象类型
            //Type type = typeof(T);
            string typeFullName = "AngelRM.Model." + ModelClassName + ", AngelRM.Model";
            Type type = Type.GetType(typeFullName);
            //创建对象实例
            object obj = Activator.CreateInstance(type);
            //得到属性列表
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //保存到内部列表变量
            for (int i = 0; i < props.Length; i++)
                this.lst_colId.Add(props[i].Name);
        }

        /// <summary>
        /// 获得对象的值列表,赋值给内部列表变量lst_Value
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="TheObj">对象实例</param>
        public void GetObjValueList<T>(T TheObj)
        {
            //清空属性列表
            this.lst_Value.Clear();
            //对象类型
            Type type = typeof(T);
            //创建对象实例
            object obj = Activator.CreateInstance(type);
            //得到属性列表
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //保存到内部列表变量
            for (int i = 0; i < props.Length; i++)
            {
                object value = props[i].GetValue(TheObj, null);//通过属性获取当前对象的属性值
                if (value == null) value = "";
                this.lst_Value.Add(value.ToString());
            }
        }
        /// <summary>
        /// 数据列表转为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象</returns>
        private T List2Object<T>()
        {
            string sJson = Angel_DataJson.List2Json(this.lst_colId, this.lst_Value);
            T Object = Angel_DataJson.json2Object<T>(sJson);
            return (T)Object;
        }
        /// <summary>
        /// 通过Id获得对象
        /// </summary>
        /// <typeparam name="T">对象类</typeparam>
        /// <param name="str_id">id值，字段名必须为"id"</param>
        /// <returns>对象</returns>
        public T GetObject_ById<T>(string str_id)
        {
            this.SetCondition("id", str_id);
            return GetObject_ByCondition<T>();
        }
        /// <summary>
        /// 条件查询结果转为对象（一条记录）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象</returns>
        public T GetObject_ByCondition<T>()
        {
            this.GetData();
            if (this.lst_colId.Count == 0) return default(T);
            if (this.lst_Value.Count == 0) return default(T);
            //数据列表转为对象
            return (List2Object<T>());
        }
        #endregion
        #region 条件设定
        /// <summary>
        /// 设置第一条件
        /// </summary>
        /// <param name="str_ColName">库表的 列名</param>
        /// <param name="str_idValue">对应的 列值</param>
        public void SetCondition(string str_ColName, string str_ColValue)
        {
            this.ConditionStr = str_ColName + "='" + str_ColValue + "'";
        }
        /// <summary>
        /// 直接设置 第一条件 
        /// </summary>
        /// <param name="str_Conditon">条件字符串</param>
        public void SetConditionString(string str_Conditon)
        {
            this.ConditionStr = str_Conditon;
        }
        /// <summary>
        /// 直接设置 第一条件为检索全部记录 
        /// </summary>
        public void SetConditionAll()
        {
            this.ConditionStr = "1=1";
        }
        /// <summary>
        /// 添加附加条件
        /// </summary>
        /// <param name="str_ColName">列名</param>
        /// <param name="str_ColValue">列值</param>
        public void AddCondition(string str_ColName, string str_ColValue)
        {
            this.ConditionStr += " and ";
            this.ConditionStr += str_ColName + "='" + str_ColValue + "'";
        }
        /// <summary>
        ///  直接添加 附加条件串
        /// </summary>
        /// <param name="str_Condition">附加条件</param>
        public void AddConditionString(string str_Condition)
        {
            this.ConditionStr += " and ";
            this.ConditionStr += str_Condition;
        }
        /// <summary>
        ///  按条件检查 记录是否存在
        /// </summary>
        /// <returns></returns>
        public bool Condition_DataExist()
        {
            //输入的代码  在数据库存在
            if (this.tablename == "") return false;
            if (this.ConditionStr == "") return false;
            if (!this.Item_db.DataExistByCondition(this.tablename, this.ConditionStr))
                return false;
            return true;
        }
        #endregion
        #region 读取记录
        /// <summary>
        /// 读取单条记录（ 需要条件设置及判断 ）
        /// 此前调用：this.SetCondition(...); this.AddCondition(...);...
        /// </summary>
        public void GetData()
        {
            //清空数据
            this.lst_Value.Clear();
            //读取数据
            if (!this.Condition_DataExist()) return;
            this.lst_Value = this.Item_db.GetRecordByCondition(this.tablename, this.lst_colId, this.ConditionStr);
        }
        //
        // 读取多行记录 （ 需要条件设置及判断 ）
        //
        public void GetTable()
        {
            //清空数据
            this.ll_data.Clear();
            //读取数据
            if (!this.Condition_DataExist()) return;
            this.ll_data = this.Item_db.GetDataByCondition(this.tablename, this.lst_colId, this.ConditionStr);
        }
        // 得到符合条件的记录数
        public int GetCountByCondition()
        {
            if (!this.Condition_DataExist()) return 0;
            return this.Item_db.selectRecordCountByCondition(this.tablename, this.ConditionStr);

        }

        // 读取一列记录
        public List<string> GetWholeColValue(string colId)
        {
            List<string> ls = new List<string>();
            if (!this.Condition_DataExist()) return ls;
            return this.Item_db.GetWholeColValue(this.tablename, colId, this.ConditionStr);
        }
        // 读取一列记录(值不重复)
        public List<string> GetDisrinctColValue(string colId)
        {
            List<string> ls = new List<string>();
            if (!this.Condition_DataExist()) return ls;
            return this.Item_db.GetDisrinctColValue(this.tablename, colId, this.ConditionStr);

        }

        /// <summary>
        /// 按照事先设定的条件-->此前调用：this.SetCondition(...); this.AddCondition(...);...
        /// 读取数据到数据集
        /// </summary>
        public int GetDataset()
        {
            int total = 0;
            if (ds == null) return 0;
            //清空数据
            this.ds.Clear();
            //读取数据
            if (!this.Condition_DataExist()) return total;
            this.ds = this.Item_db.GetDataSetByCondition(this.tablename, this.lst_colId, this.ConditionStr);
            total = this.ds.Tables[0].Rows.Count;
            return total;
        }
        // 读取数据集，升序排列
        public int GetDatasetInOrder(string str_OrderBy, bool bl_asc)
        {
            int total = 0;
            if (ds == null) return 0;
            //清空数据
            this.ds.Clear();
            //读取数据
            if (!this.Condition_DataExist()) return total;
            this.ds = this.Item_db.GetDataSetByConditionInOrder(this.tablename, this.lst_colId, this.ConditionStr, str_OrderBy, bl_asc);
            total = this.ds.Tables[0].Rows.Count;
            return total;
        }


        /// <summary>
        /// 按照分页，获得数据集中的一页
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>总行数</returns>
        public int GetDataSetPage(int pageIndex, int pageSize)
        {
            //取得数据集
            int total = this.GetDataset();
            if (total > 0)
                //一页的数据集
                this.ds = GetPage(pageIndex, pageSize, this.ds);
            return total;
        }
        /// <summary>
        /// 按照分页，获得数据集中的一页
        /// </summary>
        /// <param name="pIndex">页号(1...n)</param>
        /// <param name="pSize">业内行数</param>
        /// <param name="ds">原始数据集</param>
        /// <returns>页数据集</returns>
        private DataSet GetPage(int pIndex, int pSize, DataSet ds)
        {
            DataSet newds = new DataSet();
            DataTable dt = ds.Tables[0];
            int totalNumber = dt.Rows.Count;    //总行数

            //如果pIndex是起始页，则start=(pIndex-1)*pSize;
            if (pSize < 1) pSize = 1;
            if (pIndex < 1) pIndex = 1;

            int start = (pIndex - 1) * pSize;    //起始记录 0开始
            int end = start + pSize;   //结束记录

            if (start >= totalNumber) start = totalNumber - 1;
            if (start < 0) start = 0;
            if (end > totalNumber) end = totalNumber;

            //新表
            DataTable newDt = dt.Clone(); //复制结构，新建表
            for (int i = start; i < end; i++)
            {
                //新表增加记录
                newDt.ImportRow(ds.Tables[0].Rows[i]);
            }
            newds.Tables.Add(newDt);
            return newds;
        }
        //
        // 获得数据集的Json字符串
        //
        public void GetDsJson(int total)
        {
            this.strDsJson = "";
            //this.GetDataset();
            if ((ds == null) || (ds.Tables.Count == 0))
                strDsJson = "{ total: 0, rows: [] }";
            else
                strDsJson = Angel_DataJson.dataSet2EasyUiDataGridJson(ds, total);
        }

        //
        // 获得对象的Json字符串
        //
        public string GetObjectJson<T>(T obj)
        {
            return Angel_DataJson.Object2Json<T>(obj);
        }

        #endregion
        #region 数据库操作
        //
        // 插入数据
        //
        public bool InsertData()
        {
            if (this.tablename == "") return false;
            if (this.lst_Value.Count == 0) return false;
            if (this.lst_colId.Count == 0) return false;

            if (this.Item_db.InsertData(this.tablename, this.lst_colId, this.lst_Value) == true)
                return true;
            return false;
        }
        //
        // 插入数据,返回Id
        //
        public string InsertDataGet_id()
        {
            if (this.tablename == "") return "-1";
            if (this.lst_Value.Count == 0) return "-1";
            if (this.lst_colId.Count == 0) return "-1";
            return this.Item_db.InsertDataGet_id(this.tablename, this.lst_colId, this.lst_Value);
        }

        //
        // 更新数据
        //
        public bool UpdateData()
        {
            if (this.tablename == "") return false;
            if (this.lst_Value.Count == 0) return false;
            if (this.lst_colId.Count == 0) return false;
            if (this.ConditionStr == "") this.ConditionStr = "id=" + lst_Value[0];
            if (this.Item_db.UpdateDataByConditon(this.tablename, this.lst_colId, this.lst_Value, this.ConditionStr))
                return true;
            return true;
        }

        //
        // 更新明细数据
        //
        public bool UpdateDataOrderline(string updatewhere,string lind_id)
        {
            if (this.Item_db.UpdateMyOrderlineCmd(this.tablename, updatewhere,lind_id))
                return true;
            return true;
        }

        //
        // 删除数据
        //
        public bool DeleteData()
        {
            if (this.tablename == "") return false;
            //if (this.lst_Value.Count == 0) return false;
            //if (this.lst_colId.Count == 0) return false;
            if (this.Item_db.DeleteDataByConditon(this.tablename, this.ConditionStr))
                return true;
            return false;
        }
        #endregion
        #region 其他操作
        //
        // 判断字符串是否数字
        //
        public bool isnumber(string str)
        {
            if (str.Length == 0)
                return false;

            char[] ch = new char[str.Length];
            ch = str.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                if (ch[i] != '.')
                {
                    if (ch[i] < 48 || ch[i] > 57)   //Hex 30 ~ 39
                        return false;
                }
            }
            return true;
        }
        //
        // 把yyyymmdd 转换为yyyy年MM月dd日
        //
        public string ExtendDateString(string strDate)
        {
            //string strDate = base.Text.Trim(); //取得编辑框日期文本 
            if ((strDate == "") || (strDate == " "))
                return "";

            //如果全是数字
            if (isnumber(strDate))
            {
                char[] Source = new char[strDate.Length];
                string strTarget = "";

                Source = strDate.ToCharArray();
                for (int i = 0; i < 4; i++) strTarget += Source[i];
                strTarget += "年";
                for (int i = 4; i < 6; i++) strTarget += Source[i];
                strTarget += "月";
                for (int i = 6; i < 8; i++) strTarget += Source[i];
                strTarget += "日";
                return strTarget;
            }
            //如果不是数字
            return strDate;
        }

        #endregion



    }
}
