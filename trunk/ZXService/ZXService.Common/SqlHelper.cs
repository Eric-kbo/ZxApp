using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

namespace ZXService.Common
{
    /// <summary>
    /// 数据库访问抽象基类  (基于MS-SQLServer2005或以上版本)
    /// 此类为抽象类 abstract ，不允许实例化，在应用时直接调用即可
    /// </summary>
    public abstract class SqlHelper
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.        
        public static readonly string LocalSqlServer = ConfigurationManager.ConnectionStrings["npgsql"].ConnectionString;

        #region ===========通用分页存储过程===========
        /// <summary>
        /// 通用分页存储过程
        /// </summary>
        /// <param name="connectionString">连接</param>
        /// <param name="tblName">要显示的表或多个表的连接</param>
        /// <param name="strGetFields"> 需要返回的列</param>
        /// <param name="fldName">排序的字段名</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="doCount">返回记录总数, 非 0 值则返回</param>
        /// <param name="OrderType">1降序 ,0 升序，其他 多字段order 条件</param>
        /// <param name="strWhere">查询条件 (注意: 不要加 WHERE)</param>
        /// <returns>查询当前页的数据集</returns>
        public static DataSet PageList(string connectionString, string tblName, string strGetFields, string fldName
             , int PageSize, int PageIndex, int doCount, int OrderType, string strWhere)
        {
            SqlParameter[] parameters ={ new SqlParameter("@tblName",SqlDbType.NVarChar,255),
                new SqlParameter("@strGetFields",SqlDbType.NVarChar,1000),
                new SqlParameter("@fldName",SqlDbType.NVarChar,255),
                new SqlParameter("@PageSize",SqlDbType.Int),
                new SqlParameter("@PageIndex",SqlDbType.Int),
                new SqlParameter("@doCount",SqlDbType.Bit),
                new SqlParameter("@OrderType",SqlDbType.Int),
                //new SqlParameter("@strWhere",SqlDbType.NVarChar,1500)};
                new SqlParameter("@strWhere",SqlDbType.VarChar,8000)};

            parameters[0].Value = tblName;
            parameters[1].Value = (strGetFields == null) ? "*" : strGetFields;
            parameters[2].Value = (fldName == null) ? "" : fldName;
            parameters[3].Value = PageSize;
            parameters[4].Value = PageIndex;
            parameters[5].Value = doCount;
            parameters[6].Value = OrderType;
            parameters[7].Value = (strWhere == null) ? "" : strWhere;

            DataSet ds = RunProcedureDS(connectionString, "PageList", parameters, "PageListTable");
            return ds;
        }
        #endregion

        #region ===========执行简单SQL语句============

        /// <summary>
        /// 获取表某个字段的最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMaxID(string connectionString, string FieldName, string TableName)
        {
            string strSql = "select max(" + FieldName + ") from " + TableName;
            DataSet ds = ExecuteDataSet(connectionString, strSql);
            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        ///  检测一个记录是否存在(SqlParameter语句方式) -执行的是存储过程
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool ExistsRecord(string connectionString, string strSql, params SqlParameter[] cmdParms)
        {
            DataSet ds = RunProcedureDS(connectionString, strSql, cmdParms);
            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0;
        }


        /// <summary>
        ///执行查询，并将查询返回的结果集中第一行的第一列作为 .NET Framework 数据类型返回。忽略额外的列或行。返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string connectionString, string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        #endregion 执行简单SQL语句

        #region StrSQL执行结果，返回执行后受影响的行数

        /*【公告】：ExecuteNonQuery()方法介绍
         *对于   UPDATE、INSERT   和   DELETE   语句，
         *返回值为该命令所影响的行数。对于所有其他类型的语句，
         *返回值为   -1。如果发生回滚，返回值也为   -1。
         */

        /// <summary>
        /// 执行SQL语句，返回影响的记录数，select类型的语句此方法不可行。
        /// 对于select方法应该通过Dataset.Tables[0].Rows.Count来判断
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string connectionString, string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 方法重载，限定查询时间，返回影响的记录数
        /// 客观的多并发查询时，可限制用户查询时间，以免对服务器增加负担
        /// </summary>
        /// <param name="Times">等待命令执行的时间，默认值为 30 秒。</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string connectionString, string SQLstring, int Times)
        {
            using (SqlConnection conntion = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLstring, conntion))
                {
                    try
                    {
                        conntion.Open();
                        cmd.CommandTimeout = Times;//默认值为 30 秒
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        conntion.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回记录的条数【注意是记录数】；
        /// 获取SQL字段第一行第一字段的数值，请不要用select
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteCountSql(string connectionString, string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        int count = int.Parse(dr[0].ToString());
                        return count;

                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string connectionString, string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                SqlParameter myParameter = new SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string connectionString, string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                SqlParameter myParameter = new SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 执行带参数的SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string connectionString, string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行批量插入
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="tableName">数据表名</param>
        /// <param name="dt">需要插入的值</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteBatchSql(string connectionString, string tableName, DataTable dt)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString);
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BatchSize = dt.Rows.Count;
                    connection.Open();
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        bulkCopy.WriteToServer(dt);
                    }
                    return dt.Rows.Count;

                }
                catch (Exception E)
                {
                    connection.Close();
                    throw new Exception(E.Message);
                }
            }
        }
        /// <summary>
        /// 执行存储过程，返回Return值【这个方法还没看明白，- -  】
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string connectionString, string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                connection.Close();
                return result;
            }
        }
        #endregion

        #region ============获取DataReader============

        /*【公告】下面的方法调用(先实例化一个dr)该方法后，
         * 一定要对SqlDataReader进行Close (对实例化的dr进行 dr.Close();
         * 如果不Close掉，则会保持read回话状态，加重数据库负荷) */

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string connectionString, string strSQL)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader 
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedureDR(string connectionString, string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader returnReader;
            try
            {
                connection.Open();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                connection.Close();
                throw new Exception(e.Message);
            }
            return returnReader;
        }
        #endregion

        #region =============获取DataSet==============

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string connectionString, string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();

                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds);
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        public static DataSet ExecuteDataSet(string connectionString, string strSQL, int Times)
        {
            using (SqlConnection conntion = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    conntion.Open();
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, conntion);
                    da.SelectCommand.CommandTimeout = Times;//限制查询时间
                    da.Fill(ds);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    conntion.Close();
                    throw new Exception(e.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        /// BU层得到对象实体用这个方法
        public static DataSet ExecuteDataSet(string connectionString, string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        //【调用方法】：
        //ArrayList arrayList = new ArrayList();
        //arrayList.Add("strsql1");
        //arrayList.Add("strsql2");
        //...
        //arrayList.Add("strsqln");
        //return DbHelperSQL.ExecuteManySqlDS(arrayList);         
        /// <summary>
        /// 执行多条SQL语句返回多个DataSet
        /// </summary>
        /// <param name="arrayList">arrayList对象集合</param>
        /// <returns>多个数据集</returns>
        public static DataSet ExecuteManySqlDS(string connectionString, ArrayList arrayList)
        {
            DataSet set = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                string table = "table";
                SqlDataAdapter adapter = new SqlDataAdapter();

                for (int i = 0; i < arrayList.Count; ++i)
                {
                    command.CommandText = arrayList[i].ToString();
                    adapter.SelectCommand = command;
                    adapter.Fill(set, table + i);// table + i 每个语句返回数据集的表名
                }
                return set;
            }
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedureDS(string connectionString, string storedProcName, IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    sqlDA.Fill(dataSet);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                return dataSet;
            }
        }
        public static DataSet RunProcedureDS(string connectionString, string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        #endregion

        #region ============数据库事务处理============
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        /// <returns>执行事务影响的行数</returns>
        public static int ExecuteSqlTran(string connectionString, List<String> SQLStringList)
        {
            using (SqlConnection conntion = new SqlConnection(connectionString))
            {
                conntion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conntion;

                SqlTransaction ts = conntion.BeginTransaction();
                cmd.Transaction = ts;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    ts.Commit();//提交数据库事务
                    return count;
                }
                catch
                {
                    ts.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        ///  执行多条SQL语句，实现数据库事务
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(string connectionString, Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDY in SQLStringList)
                        {
                            string cmdText = myDY.Key.ToString();
                            SqlParameter[] parameter = (SqlParameter[])myDY.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, parameter);
                            int result = cmd.ExecuteNonQuery();     //这里可以记录该事务的执行结果
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion

        #region 创建 SqlCommand 对象及创建执行命令参数

        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="cmdparams">返回带参数的命令</param>
        public static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdparams)
        {
            if (conn.State != ConnectionState.Open)
            {//判断数据库连接状态
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {//判断是否需要事物处理
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;
            if (cmdparams != null)
            {
                foreach (SqlParameter parameter in cmdparams)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)    
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        #endregion

        #region ============构造语句常用类============
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, object Value)
        {
            return MakeParam(ParamName, DbType, 0, ParameterDirection.Input, Value);
        }

        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 构建存储过程参数
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="DbType">参数类型(枚举)</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">DataSet 的参数类型(枚举)</param>
        /// <param name="Value">设置该参数的数值</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;
            if (Size > 0)
            {
                param = new SqlParameter(ParamName, DbType, Size);
            }
            else
            {
                param = new SqlParameter(ParamName, DbType);
            }
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
            {
                param.Value = Value;
            }
            return param;
        }
        #endregion 构造语句常用类

        #region =============由Object取值=============
        /// <summary>
        /// 取得Int值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInt(object obj)
        {
            if (obj.ToString() != "")
                return int.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 获得Long值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long GetLong(object obj)
        {
            if (obj.ToString() != "")
                return long.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得Decimal值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal GetDecimal(object obj)
        {
            if (obj.ToString() != "")
                return decimal.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得Guid值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid GetGuid(object obj)
        {
            if (obj.ToString() != "")
                return new Guid(obj.ToString());
            else
                return Guid.Empty;
        }

        /// <summary>
        /// 取得DateTime值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            if (obj.ToString() != "")
                return DateTime.Parse(obj.ToString());
            else
                return DateTime.MinValue;
        }

        /// <summary>
        /// 取得bool值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetBool(object obj)
        {
            if (obj.ToString() == "1" || obj.ToString().ToLower() == "true")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 取得byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Byte[] GetByte(object obj)
        {
            if (obj.ToString() != "")
            {
                return (Byte[])obj;
            }
            else
                return null;
        }

        /// <summary>
        /// 取得string值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetString(object obj)
        {
            return obj.ToString();
        }
        #endregion

        #region ===========序列化与反序列化===========
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>返回二进制</returns>
        public static byte[] SerializeModel(Object obj)
        {
            if (obj != null)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                byte[] b;
                binaryFormatter.Serialize(ms, obj);
                ms.Position = 0;
                b = new Byte[ms.Length];
                ms.Read(b, 0, b.Length);
                ms.Close();
                return b;
            }
            else
                return new byte[0];
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="b">要反序列化的二进制</param>
        /// <returns>返回对象</returns>
        public static object DeserializeModel(byte[] b, object SampleModel)
        {
            if (b == null || b.Length == 0)
                return SampleModel;
            else
            {
                object result = new object();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                try
                {
                    ms.Write(b, 0, b.Length);
                    ms.Position = 0;
                    result = binaryFormatter.Deserialize(ms);
                    ms.Close();
                }
                catch { }
                return result;
            }
        }
        #endregion

        #region ==========Model与XML互相转换==========
        /// <summary>
        /// Model转化为XML的方法
        /// </summary>
        /// <param name="model">要转化的Model</param>
        /// <returns></returns>
        public static string ModelToXML(object model)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlElement ModelNode = xmldoc.CreateElement("Model");
            xmldoc.AppendChild(ModelNode);

            if (model != null)
            {
                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    XmlElement attribute = xmldoc.CreateElement(property.Name);
                    if (property.GetValue(model, null) != null)
                        attribute.InnerText = property.GetValue(model, null).ToString();
                    else
                        attribute.InnerText = "[Null]";
                    ModelNode.AppendChild(attribute);
                }
            }

            return xmldoc.OuterXml;
        }

        /// <summary>
        /// XML转化为Model的方法
        /// </summary>
        /// <param name="xml">要转化的XML</param>
        /// <param name="SampleModel">Model的实体示例，New一个出来即可</param>
        /// <returns></returns>
        public static object XMLToModel(string xml, object SampleModel)
        {
            if (string.IsNullOrEmpty(xml))
                return SampleModel;
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);

                XmlNodeList attributes = xmldoc.SelectSingleNode("Model").ChildNodes;
                foreach (XmlNode node in attributes)
                {
                    foreach (PropertyInfo property in SampleModel.GetType().GetProperties())
                    {
                        if (node.Name == property.Name)
                        {
                            if (node.InnerText != "[Null]")
                            {
                                if (property.PropertyType == typeof(System.Guid))
                                    property.SetValue(SampleModel, new Guid(node.InnerText), null);
                                else
                                    property.SetValue(SampleModel, Convert.ChangeType(node.InnerText, property.PropertyType), null);
                            }
                            else
                                property.SetValue(SampleModel, null, null);
                        }
                    }
                }
                return SampleModel;
            }
        }
        #endregion

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="_strDBName">数据库名称</param>
        /// <returns></returns>
        public static bool CreateDatabase(string connStr, string _strDBName)
        {
            bool bSuccess = false;
            try
            {
                using (SqlConnection conMaster = new SqlConnection(connStr))
                {
                    conMaster.Open();
                    // Check if the Database has existed first
                    string strExist = @"select * from dbo.sysdatabases where name='" + _strDBName + @"'";
                    SqlCommand cmdExist = new SqlCommand(strExist, conMaster);
                    SqlDataReader readerExist = cmdExist.ExecuteReader();
                    bool bExist = readerExist.HasRows;
                    readerExist.Close();
                    if (bExist)
                    {
                        //数据库已经存在,注释了删除代码，直接return true;
                        return true;
                        //string strDel = @"drop database " + _strDBName;
                        //SqlCommand cmdDel = new SqlCommand(strDel, conMaster);
                        //cmdDel.ExecuteNonQuery();
                    }
                    // Create the database now;     
                    string strDatabase = "Create Database [" + _strDBName + "]";
                    SqlCommand cmdCreate = new SqlCommand(strDatabase, conMaster);
                    cmdCreate.ExecuteNonQuery();
                    conMaster.Close();
                }
                bSuccess = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return bSuccess;
        }
        /// <summary>
        /// 导入sql脚本
        /// </summary>
        /// <param name="sqlConnString">连接数据库字符串</param>
        /// <param name="varFileName">脚本路径</param>
        /// <returns></returns>
        public static bool ExecuteSqlFile(string sqlConnString, string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] inf = dir.GetFiles();
            foreach (FileInfo finf in inf)
            {
                if (finf.Extension.Equals(".sql"))
                {
                    if (!File.Exists(finf.FullName))
                    {
                        return false;
                    }
                    StreamReader rs = new StreamReader(finf.FullName, System.Text.Encoding.Default);
                    ArrayList alSql = new ArrayList();
                    string commandText = "";
                    string varLine = "";
                    while (rs.Peek() > -1)
                    {
                        varLine = rs.ReadLine();
                        if (varLine == "")
                        {
                            continue;
                        }
                        if (varLine != "GO")
                        {
                            commandText += varLine;
                            commandText += "\r\n";
                        }
                        else
                        {
                            commandText += "";
                        }
                    }
                    alSql.Add(commandText);
                    rs.Close();
                    try
                    {
                        ExecuteCommand(sqlConnString, alSql);
                        //return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return true;

        }
        private static void ExecuteCommand(string sqlConnString, ArrayList varSqlList)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                //Don't use Transaction, because some commands cannot execute in one Transaction.
                //SqlTransaction varTrans = conn.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                //command.Transaction = varTrans;
                try
                {
                    foreach (string varcommandText in varSqlList)
                    {
                        command.CommandText = varcommandText;
                        command.ExecuteNonQuery();
                    }
                    //varTrans.Commit();
                }
                catch (Exception ex)
                {
                    //varTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
