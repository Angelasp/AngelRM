using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace AngelRM.DBHelper
{
   public class DBHelperSql
    {
        public SqlConnection cn;    // SQL连接对象 cn
        private DataSet ds;                   // 数据集
        private SqlDataAdapter da;  // 数据适配器
        private SqlCommand cmd;   //命令
        private SqlDataReader rdr;   //数据读取器

        #region 构造函数
       /// <summary>
       /// 无参构造函数
       /// </summary>
        public DBHelperSql()
        {
            this.cn = new SqlConnection();      //建立连接串
            //Web.confg文件中定义
            //<connectionStrings>
            //<add name="connstr" connectionString="Data Source=feng;Initial Catalog=person;Persist Security Info=True;User ID=sa;Password=sa"/>
            //</connectionStrings>
            //"server=192.168.0.154;User Id=mcrm;password=mcrm;database=mcrm_corp"
            this.cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["angelconn"].ConnectionString; ;
            //this.cn.ConnectionString="server=127.0.0.1;uid=root;pwd=root;database=mcrm";
            this.ds = new DataSet();              //DataSet
            this.da = new SqlDataAdapter();     //建立数据适配器
            this.cmd = new SqlCommand();        //建立Command

        }

        public string getDatabaseName()
        {
            String ss = cn.ConnectionString;
            if (ss == "") return "";
            string[] result = System.Text.RegularExpressions.Regex.Split(ss, ";", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            for (int i = 0; i < result.Length; i++)
            {
                string caption = result[i].ToUpper().Trim().Substring(0, 7);
                if (caption == "DATABAS")
                    return result[i].Trim().Substring(9);
            }
            return "";

        }
        /// <summary>
        /// 构造函数1
        /// </summary>
        /// <param name="sConnStr">server=127.0.0.1;uid=root;pwd=root;database=dbName;</param>
        public DBHelperSql(String sConnStr)
        {
            this.cn = new SqlConnection();      //建立连接串
            this.cn.ConnectionString = sConnStr;
            this.ds = new DataSet();            //DataSet
            this.da = new SqlDataAdapter();     //建立数据适配器
            this.cmd = new SqlCommand();        //建立Command

        }
        /// <summary>
        /// 构造函数2
        /// </summary>
        /// <param name="server">服务器名</param>
        /// <param name="Catalog">库名</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        public DBHelperSql(string server, string dataBase, string user, string pass)
        {
            this.cn = new SqlConnection();      //建立连接串
            //"server=127.0.0.1;uid=root;pwd=root;database=world;";
            this.cn.ConnectionString = "server=" + server +
                                      ";uid=" + user +
                                      ";pwd=" + pass +
                                      ";database=" + dataBase +
                                      ";"
;
            this.ds = new DataSet();            //DataSet
            this.da = new SqlDataAdapter();     //建立数据适配器
            this.cmd = new SqlCommand();        //建立Command

        }
        #endregion
        #region 打开关闭数据库联接
        /// <summary>
        /// 打开SQL连接对象， cn是本类库的属性变量
        /// </summary>
        /// <returns>返回是否成功</returns>
        private bool OpenConnection()
        {
            try { this.cn.Open(); }
            catch (Exception ex)
            {
                // System.Console.WriteLine("数据库连接错误");
                // throw ex;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 和OpenConnection对应，关闭SQL连接对象
        /// </summary>
        private void CloseConnection()
        {
            this.cn.Close();
        }
        #endregion
        #region 获取表中的记录数

        /// <summary>
        /// 表中全部记录数 
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>记录数</returns>
        public int selectRecordCount(string TableName)
        {
            return selectRecordCountByCondition(TableName, "1=1");
        }
        /// <summary>
        /// 符合条件记录数 
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditonString">条件(不包含where)</param>
        /// <returns>记录数</returns>
        public int selectRecordCountByCondition(string TableName, string ConditonString)
        {
            int count = 0;
            cmd.CommandText = "select count(*) from " + TableName;              //配置Command
            cmd.CommandText += " where " + ConditonString + ";";                //加入条件串
            if (!OpenConnection()) return -1;
            cmd.Connection = cn;
            //var vv = cmd.ExecuteScalar().ToString();
            count = int.Parse(cmd.ExecuteScalar().ToString());
            CloseConnection();
            return count;
        }
        /// <summary>
        /// 获取某列的最大值（必须为数值，否则返回0）
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ColunmName">列名</param>
        /// <returns>最大值</returns>
        public int selectMaxValueOfColumn(string TableName, string ColunmName)
        {
            //SELECT max(rosterID) FROM `ofroster`;
            cmd.CommandText = "select max("+ColunmName+") from `" + TableName+"`";              //配置Command
            if (!OpenConnection()) return -1;
            cmd.Connection = cn;
            
            var maxValue = cmd.ExecuteScalar().ToString();
            
            CloseConnection();

            Regex re_Number = new Regex("[0-9]");
            if(re_Number.IsMatch(maxValue) )
                return int.Parse(maxValue);
            else
               return 0;
        }

        /// <summary>
        /// 按列值得到记录数
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IdColName"></param>
        /// <param name="IdColValue"></param>
        /// <returns></returns>
        public int SelectRecordCountByColVal(string TableName, string IdColName, string IdColValue)
        {
            string strCondition = IdColName + " like '" + IdColValue + "';";
            return (selectRecordCountByCondition(TableName, strCondition));
        }
        /// <summary>
        /// 获取DB当前时间
        /// </summary>
        /// <returns>20130903101834</returns>
        public String selectDbTime()
        {
            cmd.CommandText = "select DATE_FORMAT(NOW(),'%Y%m%d%H%i%s');";
            if (!OpenConnection()) return "-1";
            cmd.Connection = cn;
            String str_dbTime = cmd.ExecuteScalar().ToString();
            CloseConnection();
            return str_dbTime;
        }
        #endregion
        #region 新增插入
        /// <summary>
        /// 生成Insert SQL 命令文本
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lst_ColName">列名串</param>
        /// <param name="lst_ColValue">列值串</param>
        /// <returns>命令字符串</returns>
        public string GetInsertCmd(string tablename, List<string> lst_ColName, List<string> lst_ColValue)
        {
            string QStr = "";       //查询串        "insert into sys_user"
            string ColumnStr = "";  //列名串        "user_id,name,password,role,upd_date,upd_cd"
            string NumberStr = "";  //带入数字串    "'{0}','{1}','{2}','{3}',getdate(),'{4}'",
            List<string> lstFormatValue = new List<string>();       //值串       "UserId, Name, Password, Access,getdate(),Updater);
            int ColCount = 0;     //列数
            int i = 0;

            ColCount = lst_ColName.Count; //传入的列名串
            if (ColCount <= 0) return "";
            //如果表中含有标识列（自增字段）
            //int colStart = 0;
            //if (this.tableHasIdentity(tablename))
            int colStart = 1;  //默认表中含有标识列

            //给列名加上逗号，生成列名串 -->如："user_id,name,password,role,upd_date,upd_cd"
            for (i = colStart; i < ColCount - 1; i++)
                ColumnStr += lst_ColName[i] + ",";
            ColumnStr += lst_ColName[ColCount - 1];   //最后一列不要逗号

            //生成带入数字串
            for (i = colStart; i < ColCount; i++)
            {
                string strColValue = (lst_ColValue[i] == null) ? "" : lst_ColValue[i];
                //判断是否date_format 开头
                if ((strColValue.Length > 10) && strColValue.Substring(0, 11).ToUpper() == "DATE_FORMAT")
                    NumberStr += "{" + Convert.ToString(i) + "},";      //date_format(now(),'%Y%m%d%H%i%s')注入不要引号
                else
                    NumberStr += "'{" + Convert.ToString(i) + "}',";
            }
            //最后一个值不要逗号
            if (NumberStr.Length > 1)
                NumberStr = NumberStr.Substring(0, NumberStr.Length - 1);

            //生成值串 将值代入{0},{1}...生成值串
            for (i = 0; i < ColCount; i++)
            {
                if (!string.IsNullOrEmpty(lst_ColValue[i]))
                    lstFormatValue.Add(lst_ColValue[i].Trim());
                else
                    lstFormatValue.Add(lst_ColValue[i]);
            }

            //生成最终查询串
            QStr = "insert into " + tablename + " (" + ColumnStr + ") values"
                    + "(" + NumberStr + ");";
            string FinalStr = "";
            FinalStr = string.Format(QStr, lstFormatValue.ToArray());

            return FinalStr;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="Colnames">列名列表</param>
        /// <param name="ColValues">列值列表</param>
        /// <returns>成功否</returns>
        public bool InsertData(string tablename, List<string> Colnames, List<string> ColValues)
        {
            //cmd.CommandText = string.Format("insert into sys_user"
            //                 + "(user_id,name,password,role,upd_date,upd_cd) values"
            //                 + "('{0}','{1}','{2}','{3}',getdate(),'{4}');",
            //                 this.UserId, this.Name, this.Password, this.Access, GlobalUserClass.GlobalUserID);
            //cmd.CommandText = cmdStr;
            bool success = true;
            cmd.CommandText = this.GetInsertCmd(tablename, Colnames, ColValues);
            cmd.Connection = cn;
            if (!OpenConnection()) return false;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return success;
        }
        /// <summary>
        /// 插入数据,返回自增id
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="Colnames">列名列表</param>
        /// <param name="ColValues">列值列表</param>
        /// <returns>出错返回-1，无id返回“0”</returns>
        public string InsertDataGet_id(string tablename, List<string> Colnames, List<string> ColValues)
        {
            //cmd.CommandText = string.Format("insert into sys_user"
            //                 + "(user_id,name,password,role,upd_date,upd_cd) values"
            //                 + "('{0}','{1}','{2}','{3}',getdate(),'{4}');",
            //                 this.UserId, this.Name, this.Password, this.Access, GlobalUserClass.GlobalUserID);
            //cmd.CommandText = cmdStr;
            bool success = true;
            string id = "";
            cmd.Connection = cn;
            if (!OpenConnection()) return "-1";
            try
            {
                cmd.CommandText = this.GetInsertCmd(tablename, Colnames, ColValues);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT LAST_INSERT_ID();";
                id = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return id;
        }

        #endregion
        #region 条件串
        /// <summary>
        ///  生成条件串(不包含where)  --> "user_id='xcg' and password ='xcg' 
        /// </summary>
        /// <param name="lst_ColId">列名列表</param>
        /// <param name="lst_ColValue">列值列表</param>
        /// <returns>条件串</returns>
        public string GetConditionStr(List<string> lst_ColId, List<string> lst_ColValue)
        {
            int ColCount = 0;     //列数
            string ConditonStr = "";
            ColCount = lst_ColId.Count; //传入的列数
            //
            //生成值串
            for (int i = 0; i < ColCount; i++)
            {
                if (i > 0) ConditonStr += " and ";

                ConditonStr += lst_ColId[i] + "='{" + Convert.ToString(i) + "}'";
            }
            ConditonStr = string.Format(ConditonStr, lst_ColValue.ToArray());

            return ConditonStr;
        }
        #endregion
        #region 更新记录

        /// <summary>
        /// 生成Update SQL 命令文本，
        /// 当以"getdate()"作为字段的值，执行命令时时会插入系统时间。
        /// 不更新Id字段
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lst_ColId">列名表</param>
        /// <param name="lst_ColValue">列值表</param>
        /// <returns>拼接的文本（不带条件）</returns>
        public string GetUpdateCmd(string tablename, List<string> lst_ColId, List<string> lst_ColValue)
        {
            List<string> lstFormatValue = new List<string>();
            string UpdCmdStr = "";
            int ColCount = 0;     //列数
            int i = 0;

            ColCount = lst_ColId.Count; //传入的 列数
            //生成值串
            for (i = 0; i < ColCount; i++)
            {
                // var value = String.IsNullOrEmpty(lst_ColValue[i]) ? "" : lst_ColValue[i].Trim();
                lstFormatValue.Add(lst_ColValue[i] ?? "");
            }
            //最终更新串
            UpdCmdStr = "update " + tablename + " set ";
            //如果表中含有标识列（自增字段）
            //int colStart = 0;
            //if (this.tableHasIdentity(tablename))
            int colStart = 1;
            //字段=值，... 字段=值，
            for (i = colStart; i < ColCount; i++)
            {
                var strColValue = lst_ColValue[i] ?? "";
                //判断是否date_format 开头
                if ((strColValue.Length > 10) && strColValue.Substring(0, 11).ToUpper() == "DATE_FORMAT")
                    UpdCmdStr += lst_ColId[i] + "={" + Convert.ToString(i) + "},";     //date_format(now(),'%Y%m%d%H%i%s')注入不要引号
                else
                    //字段名 = 值，
                    UpdCmdStr += lst_ColId[i] + "='{" + Convert.ToString(i) + "}',";
            }
            //去掉最后一个逗号
            UpdCmdStr = UpdCmdStr.Substring(0, UpdCmdStr.Length - 1);

            //带入格式
            UpdCmdStr = string.Format(UpdCmdStr, lstFormatValue.ToArray());

            return UpdCmdStr;
        }
        /// <summary>
        /// 生成Update SQL 命令文本，
        /// 当以"getdate()"作为字段的值，执行命令时时会插入系统时间。
        /// 不更新Id字段
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lst_ColId">列名表</param>
        /// <param name="lst_ColValue">列值表</param>
        /// <returns>拼接的文本（不带条件）</returns>
        public bool UpdateMyOrderlineCmd(string tablename, string MyWhere, string Line_ID)
        {
            string sql = "update " + tablename + " SET Line_ID ='" + Line_ID + "' " + MyWhere + "";
            cmd.CommandText = sql;
            Console.Write(sql);
            if (!OpenConnection()) return false;
            cmd.Connection = cn;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return true;

        }
        /// <summary>
        /// 按条件修改一条记录
        /// 当以"getdate()"作为字段的值，执行命令时时会插入系统时间。
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lst_ColId">列名列表</param>
        /// <param name="lst_ColValue">列值列表</param>
        /// <param name="IdColName">id列名</param>
        /// <param name="IdColValue">id列值</param>
        /// <returns>是否成功</returns>

        public bool UpdateDataByConditon(string tablename, List<string> lst_ColId, List<string> lst_ColValue, string strCondition)
        {
            //生成命令串
            cmd.CommandText = GetUpdateCmd(tablename, lst_ColId, lst_ColValue);
            //加入条件串
            cmd.CommandText += " where " + strCondition + ";";

            if (!OpenConnection()) return false;
            cmd.Connection = cn;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return true;

        }
        /// <summary>
        /// 按ID值修改一条记录
        /// 当以"getdate()"作为字段的值，执行命令时时会插入系统时间。
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lst_ColId">列名列表</param>
        /// <param name="lst_ColValue">列值列表</param>
        /// <param name="IdColName">id列名</param>
        /// <param name="IdColValue">id列值</param>
        /// <returns>是否成功</returns>
        public bool UpdateDataById(string tablename,
                                   List<string> lst_ColId,
                                   List<string> lst_ColValue,
                                   string IdColName,
                                   string IdColValue)
        {
            //生成命令串
            cmd.CommandText = GetUpdateCmd(tablename, lst_ColId, lst_ColValue);
            //加入条件串
            cmd.CommandText += " where " + IdColName + " like '" + IdColValue + "';";

            if (!OpenConnection()) return false;
            cmd.Connection = cn;
            try { this.cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return true;

        }
        /// <summary>
        /// 按ID值修改一个字段
        /// 当以"getdate()"作为字段的值，执行命令时时会插入系统时间。
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="IdColName">id列名</param>
        /// <param name="IdColValue">id列值</param>
        /// <param name="ColId">修改列</param>
        /// <param name="ColValue">修改值</param>

        /// <returns>是否成功</returns>
        public bool UpdateColById(string tablename,
                                  string IdColName,
                                  string IdColValue,
                                  string ColId,
                                  string ColValue)
        {
            //生成命令串
            cmd.CommandText = "update " + tablename + " set " + ColId + "='" + ColValue + "'";
            //加入条件串
            cmd.CommandText += " where " + IdColName + " like '" + IdColValue + "';";

            if (!OpenConnection()) return false;
            cmd.Connection = cn;
            try { this.cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return true;

        }
        /// <summary>
        /// 按ID值修改一个字段
        /// 当以"getdate()"作为字段的值，执行命令时时会插入系统时间。
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lst_ColId">列名列表</param>
        /// <param name="lst_ColValue">列值列表</param>
        /// <param name="str_condition">条件串</param>
        /// <returns>是否成功</returns>
        public bool UpdateColByCondition(string tablename,
                                  string ColId,
                                  string ColValue,
                                  string strCondition)
        {
            //生成命令串
            cmd.CommandText = "update " + tablename + " set " + ColId + "='" + ColValue + "'";
            //加入条件串
            cmd.CommandText += " where " + strCondition + ";";

            if (!OpenConnection()) return false;
            cmd.Connection = cn;
            try { this.cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return true;

        }

        #endregion
        #region 删除记录
        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ConditionStr">条件串</param>
        /// <returns>是否成功</returns>
        public bool DeleteDataByConditon(string tableName, string ConditionStr)
        {
            bool success = true;
            cmd.CommandText = "delete from " + tableName
                            + " where " + ConditionStr
                            + ";";
            if (!OpenConnection()) return false;
            cmd.Connection = this.cn;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return success;

        }

        public bool DeleteDataById(string tableName, string idColName, string idColVal)
        {
            bool success = true;
            cmd.CommandText = "delete from " + tableName
                            + " where " + idColName + " like '" + idColVal
                            + "';";
            if (!OpenConnection()) return false;
            cmd.Connection = this.cn;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return success;

        }
        #endregion
        #region 判断记录是否存在
        /// <summary>
        /// 符合条件的记录是否存在 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="conditonStr">条件字符串</param>
        /// <returns>是否存在</returns>
        public bool DataExistByCondition(string tableName, string conditonStr)
        {
            return (this.selectRecordCountByCondition(tableName, conditonStr) > 0);
        }
        /// <summary>
        /// 列值为 colVald 的记录是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colName">列名</param>
        /// <param name="colVal">列值</param>
        /// <returns>是否存在</returns>
        public bool DataExistByColVal(string tableName, string colName, string colVal)
        {
            return (this.SelectRecordCountByColVal(tableName, colName, colVal) > 0);
        }

        #endregion


        #region =================查询读取记录
        /// <summary>
        /// 读取表数据到DataSet 
        /// </summary>
        /// <param name="tableName"> 表名</param>
        /// <param name="ConditionStr">条件</param>
        /// <returns></returns>
        /// 

        public DataTable GetDataTable(string tableName, string ConditionStr)
        {
            var ddss = new DataSet();
            var sql = "select * from " + tableName + " where " + ConditionStr + ";";
            cmd = new SqlCommand(sql, cn);
            var ddaa = new SqlDataAdapter(cmd);
            ddaa.Fill(ddss);
            return ddss.Tables[0];
        }

        /// <summary>
        /// 读取数据获得datatable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ConditionStr">条件</param>
        /// <returns>Datatable</returns>
        public DataTable GetDisrinctDataTable(string tableName,string string_ColId,string ConditionStr)
        {
            var ddss = new DataSet();
            var sql = "select distinct " + string_ColId + " from " + tableName + " where " + ConditionStr + ";";
            cmd = new SqlCommand(sql, cn);
            var ddaa = new SqlDataAdapter(cmd);
            ddaa.Fill(ddss);
            return ddss.Tables[0];
        }

        public DataSet GetAllData(string tableName)
        {
            DataSet dds = new DataSet();
            if (tableName == "") return dds;
            this.cmd.CommandText = "select * from " + tableName + ";";    //查询串
            if (!OpenConnection()) return dds;
            this.cmd.Connection = this.cn;          //连接
            //把表数据加入DataSet
            this.da.SelectCommand = this.cmd;
            this.da.Fill(dds, tableName); //数据集，表名
            CloseConnection();
            return dds;
        }
        /// <summary>
        /// 读取表记录 
        /// 结果存入 List<List<string>>
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="lst_ColId"></param>
        /// <param name="ConditonStr"></param>
        /// <returns></returns>
        public DataSet GetDataSetByCondition(string tableName, List<string> lst_ColId, string ConditonStr)
        {
            DataSet dds = new DataSet();
            if (tableName == "") return dds;
            //配置Command
            this.cmd.CommandText = GetPickStr(tableName, lst_ColId, ConditonStr);

            if (!OpenConnection()) return dds;
            this.cmd.Connection = this.cn;          //连接
            //把表数据加入DataSet
            this.da.SelectCommand = this.cmd;
            this.da.Fill(dds, tableName); //数据集，表名
            CloseConnection();
            return dds;

        }
        public DataSet GetDataSetByCondition(string tableName, List<string> lst_ColId, string ConditonStr,bool distinct)
        {
            DataSet dds = new DataSet();
            if (tableName == "") return dds;
            //配置Command
            this.cmd.CommandText = GetPickStr(tableName, lst_ColId, ConditonStr, distinct);

            if (!OpenConnection()) return dds;
            this.cmd.Connection = this.cn;          //连接
            //把表数据加入DataSet
            this.da.SelectCommand = this.cmd;
            this.da.Fill(dds, tableName); //数据集，表名
            CloseConnection();
            return dds;

        }

        // 升序排列
        public DataSet GetDataSetByConditionInOrder(string tableName, List<string> lst_ColId, string ConditonStr,
            string str_OrderBy, bool asc)
        {
            var dds = new DataSet();
            if (tableName == "") return dds;
            //配置Command
            this.cmd.CommandText = GetPickStrInOrder(tableName, lst_ColId, ConditonStr, str_OrderBy, asc,false);
            if (!OpenConnection()) return dds;
            this.cmd.Connection = this.cn;          //连接
            //把表数据加入DataSet
            this.da.SelectCommand = this.cmd;
            this.da.Fill(dds, tableName); //数据集，表名
            CloseConnection();
            return dds;
        }
        /// <summary>
        /// 生成查询命令字符串(带方向排序)
        /// "select C1,C2,C3... from TABLE where CONDITION"
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="lst_ColId">列名列表</param>
        /// <param name="ConditionStr">条件字符串</param>
        /// <param name="str_orderBy">排序字段,为空表示不排序</param>
        /// <param name="bl_asc">排序方向，true为升序，false为降序</param>
        /// <param name="bl_distinct">值不重复，筛选,false为不筛选 </param>
        /// <returns>查询字符串</returns>
        public string GetPickStrInOrder(string tableName, 
                                               List<string> lst_ColId, 
                                                         string str_Condition, 
                                                         string str_orderBy,
                                                           bool bl_asc,
                                                           bool bl_distinct)
        {
            int ColCount = lst_ColId.Count;
            //string PickStr = "select ";
            string PickStr = bl_distinct ? "select distinct " : "select ";
            //加入字段列表
            for (int i = 0; i < ColCount; i++)
            {
                if (!string.IsNullOrEmpty(lst_ColId[i].Trim()))  // 去掉空字段
                {
                    PickStr += lst_ColId[i];
                    if (i < ColCount - 1) PickStr += ",";
                }
            }
            // PickStr += ( "`"+String.Join("`,`", lst_ColId.ToArray()) +"`");
            // 排序方向
            string OrderByStr = "";
            // 如果指定了排序字段
            if (!String.IsNullOrEmpty(str_orderBy))   
               OrderByStr = (bl_asc) ? " order by " + str_orderBy + " asc; " : " order by " + str_orderBy + " desc ; ";

            PickStr += " from " + tableName + " where " + str_Condition + OrderByStr;
            return PickStr;

        }
        /// <summary>
        /// 读取表记录 
        /// 结果存入 List<List<string>>
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="lst_ColId"></param>
        /// <param name="ConditonStr"></param>
        /// <returns></returns>
        public List<List<string>> GetDataByCondition(string TableName, List<string> lst_ColId, string ConditonStr)
        {
            List<string> l_row;
            List<List<string>> ll_table;

            ll_table = new List<List<string>>();

            //配置Command
            this.cmd.CommandText = GetPickStr(TableName, lst_ColId, ConditonStr);
            //cmd.CommandText = "select * from " + TableName
            //                   + " where " + ConditonStr
            //                   + ";";

            //验证那些的就不写了
            if (!OpenConnection()) return ll_table;
            cmd.Connection = cn;
            this.rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (this.rdr.Read())     //多条记录循环
            {
                l_row = new List<string>();
                //多个字段循环
                for (int i = 0; i < this.rdr.FieldCount; i++)
                {
                    l_row.Add(rdr.GetValue(i).ToString().Trim());
                }
                ll_table.Add(l_row);
            }

            CloseConnection();
            return ll_table;
        }
        public List<List<string>> GetDataByCondition(string TableName, string lst_ColId, string ConditonStr)
        {
            List<string> l_row;
            List<List<string>> ll_table;

            ll_table = new List<List<string>>();

            if (string.IsNullOrEmpty(lst_ColId)) return null;
            string sql = "select " + lst_ColId + " from " + TableName;
            if (!string.IsNullOrEmpty(ConditonStr))
                sql += " where " + ConditonStr + " ";
            this.cmd.CommandText = sql;

            //验证那些的就不写了
            if (!OpenConnection()) return ll_table;
            cmd.Connection = cn;
            this.rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (this.rdr.Read())     //多条记录循环
            {
                l_row = new List<string>();
                //多个字段循环
                for (int i = 0; i < this.rdr.FieldCount; i++)
                {
                    l_row.Add(rdr.GetValue(i).ToString().Trim());
                }
                ll_table.Add(l_row);
            }

            CloseConnection();
            return ll_table;
        }

        /// <summary>
        /// 读取一条记录（按条件)
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lst_ColId">列名列表</param>
        /// <param name="conditionStr">条件</param>
        /// <returns>值列表</returns>
        public List<string> GetRecordByCondition(string tablename, List<string> lst_ColId, string conditionStr)
        {
            List<string> PickedData = new List<string>();
            int i = 0;
            //cmd.CommandText = "select user_id,name,password,role from sys_user where user_id='"
            //                + userid.Trim()
            //                + "';";
            this.cmd.CommandText = GetPickStr(tablename, lst_ColId, conditionStr);
            if (!OpenConnection()) return PickedData;
            this.cmd.Connection = this.cn;
            this.rdr = this.cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //while (this.rdr.Read())     //多条记录循环
            if (this.rdr.Read())     //读第一条记录
            {
                //多个字段循环
                for (i = 0; i < this.rdr.FieldCount; i++)
                {
                    PickedData.Add(rdr.GetValue(i).ToString().Trim());
                }
            }
            this.cn.Close();
            return PickedData;

        }
        /// <summary>
        /// 从DB 中读取一列（供给画面下拉列表)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ColId">列名</param>
        /// <param name="ConditionStr">查询条件</param>
        /// <returns>值列表</returns>
        public List<string> GetWholeColValue(string tableName, string ColId, string ConditionStr)
        {
            List<string> PickedData = new List<string>();
            //配置Command
            string sql = "select " + ColId + " from " + tableName;
            if (!string.IsNullOrEmpty(ConditionStr))
                sql += " where " + ConditionStr + " ";
            this.cmd.CommandText = sql;
            this.cmd.Connection = this.cn;
            if (!OpenConnection()) return PickedData;
            this.rdr = this.cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //多条记录循环
            while (this.rdr.Read())
                PickedData.Add(rdr.GetValue(0).ToString().Trim());
            CloseConnection();
            return PickedData;
        }
        /// <summary>
        /// 从DB 中读取一列（值不重复，供给画面下拉列表)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ColId">列名</param>
        /// <param name="ConditionStr">查询条件</param>
        /// <returns>值列表</returns>
        public List<string> GetDisrinctColValue(string tableName, string ColId, string ConditionStr)
        {
            List<string> PickedData = new List<string>();
            //配置Command
            this.cmd.CommandText = "select distinct " + ColId
                               + " from " + tableName
                               + " where " + ConditionStr + ";";
            this.cmd.Connection = this.cn;
            if (!OpenConnection()) return PickedData;
            this.rdr = this.cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //多条记录循环
            while (this.rdr.Read())
                PickedData.Add(rdr.GetValue(0).ToString().Trim());
            CloseConnection();
            return PickedData;
        }
        /// <summary>
        ///  根据条件串, 获得表中的一个值
        /// </summary>
        /// <param name="table_Name">表名</param>
        /// <param name="ColumnId">列名</param>
        /// <param name="str_condition">条件</param>
        /// <returns></returns>
        public string GetValueByConditionStr(string table_Name, string ColumnId, string str_condition)
        {
            string result = "";
            List<string> l_values = this.GetWholeColValue(table_Name, ColumnId, str_condition);
            if (l_values.Count > 0)
                result = l_values[0];
            return result;
        }
        /// <summary>
        ///根据一个已知列的值（ID） 读取另一列的值 （Caption）
        /// </summary>
        /// <param name="table_Name">表名</param>
        /// <param name="id_Name">已知列名</param>
        /// <param name="id_Value">已知列值</param>
        /// <param name="Col_Name">读取列名</param>
        /// <returns>读取列值</returns>
        public string GetValueByID(string table_Name, string id_Name, string id_Value, string Col_Name)
        {
            List<string> PickedData = new List<string>();
            string ConditionStr = "";
            string Caption = "";

            //配置Command

            ConditionStr += id_Name + " like '" + id_Value + "'";
            cmd.CommandText = "select " + Col_Name
                               + " from " + table_Name
                               + " where " + ConditionStr + ";";
            try
            {
                if (!OpenConnection()) return "";
                cmd.Connection = this.cn;
                this.rdr = this.cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (this.rdr.Read())     //多条记录循环
                    PickedData.Add(rdr.GetValue(0).ToString().Trim());  //只取第一条记录
                CloseConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
            if (PickedData.Count > 0) Caption = PickedData[0];
            return Caption;

        }
        #region 生成查询命令串 带select命令
        /// <summary>
        /// 生成查询命令字符串（无中文标题）
        /// "select C1,C2,C3... from TABLE where CONDITION"
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="lst_ColId">列名列表</param>
        /// <param name="ConditionStr">条件字符串</param>
        /// <returns>查询字符串</returns>
        public string GetPickStr(string tableName, List<string> lst_ColId, string ConditionStr)
        {
            int ColCount = lst_ColId.Count;
            string PickStr = "select ";

            //加入字段列表
            for (int i = 0; i < ColCount; i++)
            {
                PickStr += lst_ColId[i];
                if (i < ColCount - 1) PickStr += ",";
            }
            if (!ConditionStr.Contains("order by"))
                ConditionStr += " order by id desc ";
            //PickStr += " from " + tableName + " where " + ConditionStr + " order by id desc ; ";
            PickStr += " from " + tableName + " where " + ConditionStr + ";";
            return PickStr;

        }
        // distinct
        public string GetPickStr(string tableName, List<string> lst_ColId, string ConditionStr,bool distinct)
        {
            int ColCount = lst_ColId.Count;
            string PickStr;
            PickStr = distinct ? "select distinct " : "select ";
            //加入字段列表
            for (int i = 0; i < ColCount; i++)
            {
                PickStr += lst_ColId[i];
                if (i < ColCount - 1) PickStr += ",";
            }
            if (!ConditionStr.Contains("order by"))
                ConditionStr += " order by id desc ";
            //PickStr += " from " + tableName + " where " + ConditionStr + " order by id desc ; ";
            PickStr += " from " + tableName + " where " + ConditionStr + ";";
            return PickStr;

        }

        #region 生成查询命令串 带select命令
        /// <summary>
        ///  生成带标题的查询全部记录的命令字符串  
        ///  select C1 as '列1'，C2 as '列2'... from TABLE where 1=1
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="lst_ID">列名</param>
        /// <param name="lst_Caption">列标题</param>
        /// <param name="Condition">条件</param>
        /// <returns>查询命令字符串</returns>
        public string GetPickAllStrWithCaption(string tableName, List<string> lst_ID, List<string> lst_Caption)
        {
            return (GetPickStr_WithCaption(tableName, lst_ID, lst_Caption, "1=1"));
        }
        /// <summary>
        ///  生成带标题的查询命令字符串  
        ///  select C1 as '列1'，C2 as '列2'... from TABLE where CONDITION
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="lst_ID">列名</param>
        /// <param name="lst_Caption">列标题</param>
        /// <param name="Condition">条件</param>
        /// <returns>查询命令字符串</returns>
        public string GetPickStr_WithCaption(string tableName, List<string> lst_ID, List<string> lst_Caption, string Condition)
        {
            string ColumnStr = "";  //列名串
            string QStr = "";       //查询串
            int i = 0;

            while (i < lst_ID.Count)
            {
                ColumnStr += lst_ID[i] + " as '" + lst_Caption[i] + "'";
                if (i < lst_ID.Count - 1)
                    ColumnStr += ",";
                i++;
            }

            QStr = "select " + ColumnStr + " from " + tableName + " where " + Condition;
            return QStr;
        }
        #endregion
        #endregion
        #region 数据库操作
        //    
        //得到库中的所有表
        //
        public List<string> GetTableList()
        {
            return GetWholeColValue("sysobjects", "name", "type ='U'");
        }
        //
        //得到表中的所有列的名称
        //
        public List<string> GetColList(string str_tableName)
        {
            //select column_name from information_schema.columns where table_name='city';
            return GetWholeColValue("information_schema.columns", "column_name", "table_name = '" + str_tableName + "'");
        }

        /// <summary>
        ///  调用存储过程
        ///  用 法：
        ///      string[] names = { "output_Name", "input_Name1", "input_Name2" };
        ///      string[] values = { "", "input_value1", "input_value2" };
        ///      string result = db.callStoredProcedure("spName", names, values);
        /// </summary>
        /// <param name="str_spName">存储过程名</param>
        /// <param name="spParaNames">参数名列表，第一个为返回参数，无返回则将之设为"" </param>
        /// <param name="spParaValues">参数值列表</param>
        /// <returns></returns>
        public string callStoredProcedure(string str_spName, string[] spParaNames, string[] spParaValues)
        {
            SqlParameter p;
            string str_result = "";  // 返回值
            // 调用SP
            cmd.CommandText = str_spName;              //配置存储过程名
            cmd.CommandType = CommandType.StoredProcedure;
            // 检查参数名和参数值数量相同
            int paraCount = spParaNames.Length;
            if (spParaValues.Length != paraCount)
                return "the parameter's name and value number not match";
            // 设置参数，添加到数据库 
            if (spParaNames[0] != "")                   // 输出参数
            {
                p = new SqlParameter("?" + spParaNames[0], SqlDbType.VarChar, 200);
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
            }
            for (int i = 1; i < paraCount; i++)        // 输入参数
            {
                p = new SqlParameter("?" + spParaNames[i], SqlDbType.VarChar, 200);
                p.Value = spParaValues[i].ToString();
                cmd.Parameters.Add(p);
            }
            // 调用存储过程
            if (!OpenConnection()) return "connection error";
            cmd.Connection = cn;
            try
            {
                cmd.ExecuteNonQuery();
                if (spParaNames[0] != "")    // 有返回值
                    str_result = (string)cmd.Parameters[0].Value;
                else  // 没有返回值
                    str_result = "success";
            }
            catch (Exception ex) { str_result = ex.Message; }
            CloseConnection();
            // 返回值
            return str_result;
        }
        #endregion
        #region 直接执行Sql
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="str_SqlStatements">Sql语句</param>
        /// <returns>成功否</returns>
        public bool ExecuteSql(string str_SqlStatements)
        {
            bool success = true;
            cmd.CommandText = str_SqlStatements;
            cmd.Connection = cn;
            if (!OpenConnection()) return false;
            try { cmd.ExecuteNonQuery(); }
            catch (Exception ex) { throw ex; }
            CloseConnection();
            return success;
        }
        #endregion

        #endregion===================查询结束
    }
}
