using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts
{
    [DataContract]
    public class PageDataEntity<T, Q>
    {
        private int _PageSize = 20;
        private int _PageIndex = 1;
        private int _PageCount = 0;
        private int _TotalCount = 0;
        private bool _isQueryTotalCounts = true;//是否查询总的记录条数
        private string _Columns = "*";

        /// <summary>
        /// 要查表或者视图名字
        /// </summary>
        [DataMember]
        public string TableNameOrViewName
        {
            get;
            set;
        }

        /// <summary>
        /// 要查询的列
        /// 如果没有默认为*。如：col1,col2
        /// </summary>
        [DataMember]
        public string Columns
        {
            get
            {
                return _Columns;
            }
            set
            {
                _Columns = value;
            }
        }
        /// <summary>
        /// 排序列
        /// 必须要有。如：col1 desc,col2 asc
        /// </summary>
        [DataMember]
        public string OrderByColumns
        {
            get;
            set;
        }
        /// <summary>
        /// 是否查询总的记录条数
        /// </summary>
        [DataMember]
        public bool IsQueryTotalCounts
        {
            get { return _isQueryTotalCounts; }
            set { _isQueryTotalCounts = value; }
        }
        /// <summary>
        /// 显示页数
        /// </summary>
        /// 
        [DataMember]
        public int PageSize
        {
            get
            {
                return _PageSize;

            }
            set
            {
                _PageSize = value;
            }
        }
        /// <summary>
        /// 当前页
        /// </summary>
        /// 
        [DataMember]
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        /// 
        [DataMember]
        public int PageCount
        {
            get
            {
                return _PageCount;

            }
            set
            {
                _PageCount = value;

                if (PageCount != 0 && PageIndex > PageCount)
                {
                    PageIndex = PageCount;
                }

            }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        /// 
        [DataMember]
        public int TotalCount
        {
            get
            {
                return _TotalCount;
            }
            set
            {
                _TotalCount = value;

                PageCount = TotalCount % PageSize == 0 ? TotalCount / PageSize : TotalCount / PageSize + 1;
            }
        }
        [DataMember]
        public List<KeyValuePair<KeyValuePair<string, object>, DbType>> QueryConditions
        {
            get;
            set;
        }



        [DataMember]
        public Q ObjQueryConditions
        {
            get;
            set;
        }

        /// <summary>
        /// 查询结果
        /// </summary>
        [DataMember]
        public List<T> QueryResult
        {
            get;
            set;
        }

    }
}
