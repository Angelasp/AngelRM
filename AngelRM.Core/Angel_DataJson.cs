using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;


namespace AngelRM.Core
{
    public class Angel_DataJson
    {
        /// <summary>   
        /// 添加时间转换器   
        /// </summary>   
        /// <param name="serializer"></param>   
        private static void AddIsoDateTimeConverter(JsonSerializer serializer)
        {
            IsoDateTimeConverter idtc = new IsoDateTimeConverter();
            //定义时间转化格式   
            idtc.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //idtc.DateTimeFormat = "yyyy-MM-dd";   
            serializer.Converters.Add(idtc);
        }

        /// <summary>   
        /// Json转换配置   
        /// </summary>   
        /// <param name="serializer"></param>   
        private static void SerializerSetting(JsonSerializer serializer)
        {
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //serializer.NullValueHandling = NullValueHandling.Ignore;   
            //serializer.MissingMemberHandling = MissingMemberHandling.Ignore;   
            //serializer.DefaultValueHandling = DefaultValueHandling.Ignore;   
        }

        //------------------------------------------------------------------------------------//  

        /// <summary>   
        /// 对象列表 ==> Json字符串
        /// </summary>   
        /// <param name="objList">对象列表</param>   
        /// <returns>Json字符串</returns>   
        public static string objList2Json<T>(List<T> objList)
        {
            JsonSerializer serializer = new JsonSerializer();
            SerializerSetting(serializer);
            AddIsoDateTimeConverter(serializer);

            using (TextWriter sw = new StringWriter())
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                //writer.Formatting = Formatting.Indented;   //缩进格式
                serializer.Serialize(writer, objList);
                return sw.ToString();
            }
        }

        /// <summary>   
        ///  一个对象 ==> Json字符串
        /// </summary>   
        /// <typeparam name="T"></typeparam>   
        /// <param name="obj"></param>   
        /// <returns></returns>   
        public static string Object2Json<T>(T obj)
        {
            JsonSerializer serializer = new JsonSerializer();
            SerializerSetting(serializer);
            AddIsoDateTimeConverter(serializer);

            using (TextWriter sw = new StringWriter())
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                //writer.Formatting = Formatting.Indented;   //缩进格式
                serializer.Serialize(writer, obj);
                return sw.ToString();
            }
        }
        /// <summary>   
        /// json字符串 ==> 对象（一个）
        /// </summary>   
        /// <typeparam name="T"></typeparam>   
        /// <param name="data"></param>   
        /// <returns></returns>   
        public static T json2Object<T>(string data)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                AddIsoDateTimeConverter(serializer);
                StringReader sr = new StringReader(data);
                return (T)serializer.Deserialize(sr, typeof(T));
            }
            catch (Exception)
            {
                //throw ee;
                return default(T);
            }
        }

        /// <summary>   
        /// json字符串 ==> 对象列表
        /// [{ , }, { , }] ==> Class Obj
        /// </summary>   
        /// <typeparam name="T"></typeparam>   
        /// <param name="data"></param>   
        /// <returns></returns>   
        public static List<T> json2ObjList<T>(string data)
        {
            JsonSerializer serializer = new JsonSerializer();
            //  serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            //   AddIsoDateTimeConverter(serializer);
            StringReader sr = new StringReader(data);
            return (List<T>)serializer.Deserialize(sr, typeof(List<T>));
        }
        /// <summary>
        ///  数据集（dataSet) ==> Json字符串
        /// </summary>
        public static string dataSet2Json(DataSet dataSet)
        {
            // IsoDateTimeConverter mtimeconvert = new IsoDateTimeConverter();
            //   mtimeconvert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //   return (JsonConvert.SerializeObject(dataSet, Formatting.Indented,mtimeconvert));
            return (JsonConvert.SerializeObject(dataSet));
        }
        /// <summary>
        ///  数据表（dataTable) ==> Json字符串通过DataSet转
        /// </summary>
        public static string dataTable2Json(DataTable table)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(table.Copy());
            ds.AcceptChanges();
            return (JsonConvert.SerializeObject(ds, Formatting.Indented));
        }
        /// <summary>
        /// 数据表（dataTable) ==> Json字符串直接转
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string dataTable2JsonZ(DataTable table)
        {
            return (JsonConvert.SerializeObject(table, Formatting.Indented));
        }
        //
        //从Json串中直接读取属性
        static void ReadJsonString()
        {
            JObject o = JObject.Parse(@"{ ""Stores"": [    ""Lambton Quay"",    ""Willis Street"" ], ""Manufacturers"": [    {      ""Name"": ""Acme Co"",      ""Products"": [        {          ""Name"": ""Anvil"",          ""Price"": 50        }      ]    },    {      ""Name"": ""Contoso"",      ""Products"": [        {          ""Name"": ""Elbow Grease"",          ""Price"": 99.95        },        {          ""Name"": ""Headlight Fluid"",          ""Price"": 4        }      ]    } ]}");
            string name = (string)o.SelectToken("Manufacturers[0].Name");// Acme Co 
            decimal productPrice = (decimal)o.SelectToken("Manufacturers[0].Products[0].Price");  // 50 
            string productName = (string)o.SelectToken("Manufacturers[1].Products[0].Name");// Elbow Grease 
            Console.WriteLine(name);
            Console.WriteLine(productPrice);
            Console.WriteLine(productName);
        }
        //
        //按属性列表读取Json串，给值列表赋值
        //
        public static void getValuelistByJsonString(string strJson, List<string> l_propName, out List<string> l_porpValue)
        {
            JObject o = JObject.Parse(strJson);
            List<string> lv = new List<string>();
            for (int i = 0; i < l_propName.Count; i++)
                lv.Add((string)o.SelectToken(l_propName[i]));
            l_porpValue = lv;
        }
        //
        // 读取Json串中的某个属性,返回属性值
        //
        public static string getValueByNameInJsonString(string strJson, string propName)
        {
            JObject o = JObject.Parse(strJson);
            //List<string> lv = new List<string>();
            string str_value;
            str_value = (string)o.SelectToken(propName);
            return str_value;
        }
        //
        // 读取Json串中的某个属性,返回属性对象
        //
        public static Object getObjectByNameInJsonString(string strJson, string propName)
        {
            JObject o = JObject.Parse(strJson);
            //List<string> lv = new List<string>();
            Object obj_value;
            obj_value = (Object)o.SelectToken(propName);
            return obj_value;
        }
        /// <summary>
        /// 用属性列表和值列表构造Json串
        /// </summary>
        /// <param name="l_property">属性名称列表</param>
        /// <param name="l_value">属性值列表</param>
        /// <returns>Json字符串</returns>
        public static string List2Json(List<string> l_property, List<string> l_value)
        {
            using (TextWriter sw = new StringWriter())
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                //writer.Formatting = Formatting.Indented;   //缩进格式
                //writer.WriteStartArray();                  // [
                //writer.WriteValue("JSON!");                //"JSON!",
                //writer.WriteValue(1);                      //1,
                //writer.WriteValue(true);                   //true,

                //开始
                writer.WriteStartObject();              //{      
                for (int i = 0; i < l_property.Count; i++)
                {
                    writer.WritePropertyName(l_property[i]);   // "property":"value"
                    writer.WriteValue(l_value[i]);
                }
                //结束
                writer.WriteEndObject();                //}
                //writer.WriteEndArray();                 //]
                writer.Flush();
                string jsonText = sw.ToString();
                return jsonText;
            }
        }
        //BuildJson------------
        /// <summary>
        /// 将数据集转换为EasyUi DataGrid格式的Json
        /// </summary>
        /// <param name="dataSet">传入数据集</param>
        /// <param name="total">总记录数（分页用）</param>
        /// <returns>结果字符串</returns>
        public static string dataSet2EasyUiDataGridJson(DataSet dataSet, int total)
        {
            //正则表达式工具
            Angel_RegularExpression re = new Angel_RegularExpression();
            //int count = dataSet.Tables[0].Rows.Count;
            //数据集==>Json串
            IsoDateTimeConverter istime = new IsoDateTimeConverter();
            istime.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dataSet, istime);
            if (result == "") return result;
            //添加头部字符串，更新总行数
            string strMatch = @"^{.*?:";
            string strRep = "{\"total\":";
            strRep += total.ToString();
            strRep += ",\"rows\":";
            if (!re.IsMatchStr(result, strMatch)) return "";
            result = re.ReplaceStr(result, strMatch, strRep);

            return result;
        }//dataSet2EasyUiDataGridJson
        /// <summary>
        /// 将对象列表==>EasyUi DataGrid格式的Json
        /// </summary>
        /// <param name="objList">对象列表</param>
        /// <returns>结果字符串</returns>
        /// 
        public static string objList2EasyUiDataGridJson<T>(List<T> objList)
        {
            string result = "";
            //对象列表==>Json串
            int count = objList.Count;
            if (count > 0) result = objList2Json(objList);
            //格式化
            if (result == "") return result;
            //在结果前加入{"total":28,"rows": 
            string head = "{\"total\":";
            head += count.ToString();
            head += ",\"rows\":";
            //在结果后加入} 
            result = head + result + "}";
            return result;
        }//ObjList2EasyUiDataGridJson

    }
}
