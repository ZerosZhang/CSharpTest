using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Xml.Serialization;


namespace _011.Json和Xml序列化
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Clear();
            string _json_path = "TestJson.json";
            JsonClass _json = LoadConfig_Json<JsonClass>(_json_path);
            richTextBox1.AppendText($"读取{_json_path}中的对象\r\n");

            if (_json == null)
            {
                _json = new JsonClass();
                richTextBox1.AppendText("首次创建新的对象\r\n");
            }
            SaveConfig_Json(_json, _json_path);
            richTextBox1.AppendText($"保存对象到{_json_path}中\r\n");
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Clear();
            string _xml_path = "TestXml.xml";
            XmlClass _xml = LoadConfig_Xml<XmlClass>(_xml_path);
            richTextBox1.AppendText($"读取{_xml_path}中的对象\r\n");

            if (_xml == null)
            {
                _xml = new XmlClass();
                richTextBox1.AppendText("首次创建新的对象\r\n");
            }
            SaveConfig_Xml(_xml, _xml_path);
            richTextBox1.AppendText($"保存对象到{_xml_path}中\r\n");
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Clear();
            string _list_path = "TestList.json";
            ListClass _json = LoadConfig_Json<ListClass>(_list_path);
            richTextBox1.AppendText($"读取{_list_path}中的对象\r\n");

            if (_json == null)
            {
                _json = new ListClass();
                richTextBox1.AppendText("首次创建新的对象\r\n");
            }
            SaveConfig_Json(_json, _list_path);
            richTextBox1.AppendText($"保存对象到{_list_path}中\r\n");
        }

        /// <summary>
        /// 读取Json文件，转化为任意配置结构JsonClass类型
        /// </summary>
        /// <typeparam name="JsonClass"></typeparam>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static JsonClass LoadConfig_Json<JsonClass>(string _path)
        {
            if (!File.Exists(_path))
            {
                return default(JsonClass);
            }
            try
            {
                string _json_string = File.ReadAllText(_path);
                return JsonConvert.DeserializeObject<JsonClass>(_json_string);
            }
            catch
            {
                return default(JsonClass);
            }
        }

        /// <summary>
        /// 将任意配置结构转化为Json并保存
        /// </summary>
        /// <typeparam name="JsonClass"></typeparam>
        /// <param name="_config"></param>
        /// <param name="_path"></param>
        public static void SaveConfig_Json<JsonClass>(JsonClass _config, string _path)
        {
            if (_config == null) { return; }

            try
            {
                File.WriteAllText(_path, JsonConvert.SerializeObject(_config, Formatting.Indented));
            }
            catch
            {
                // 序列化失败不做任何处理
            }
        }

        /// <summary>
        /// 读取XML文件，转化为任意配置结构ConfigType类型
        /// </summary>
        /// <typeparam name="XmlClass"></typeparam>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static XmlClass LoadConfig_Xml<XmlClass>(string _path)
        {
            if (!File.Exists(_path)){return default(XmlClass);}

            try
            {            
                // using的写法可以自动关闭FileStream
                using (FileStream _stream = File.OpenRead(_path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(XmlClass));
                    return (XmlClass)serializer.Deserialize(_stream);
                }
            }
            catch (System.Exception)
            {
                return default(XmlClass);
            }
        }

        /// <summary>
        /// 将任意配置结构转化为XML并保存
        /// </summary>
        /// <typeparam name="XmlClass"></typeparam>
        /// <param name="_config"></param>
        /// <param name="_path"></param>
        public static void SaveConfig_Xml<XmlClass>(XmlClass _config, string _path)
        {
            if (_config == null) { return; }

            try
            {
                // using的写法可以自动关闭FileStream
                using (FileStream _stream = File.OpenWrite(_path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(XmlClass));
                    serializer.Serialize(_stream, _config);
                }
            }
            catch
            {
                // 序列化失败不做任何处理
            }
        }


    }


    public class JsonClass
    {
        [JsonProperty(PropertyName = "整型数据")]
        public int i = 0;
        public string str;
        [JsonProperty]
        private double[] array = new double[5];
        public List<float> list = new List<float>();
        [JsonProperty(PropertyName = "日志等级")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevel _level = LogLevel.debug;

        public enum LogLevel
        {
            debug = 0,
            info = 1,
            warning = 2,
            error = 3
        }
    }


    [XmlRoot("配置文件")]
    public class XmlClass
    {
        [XmlElement("整型")]
        public int i = 0;
        public string str = "测试XML序列化";
        [XmlElement("数组")]
        private double[] array = new double[5];
        public List<float> list = new List<float>();
        [XmlElement("日志等级")]
        public LogLevel _level = LogLevel.debug;

        public enum LogLevel
        {
            debug = 0,
            info = 1,
            warning = 2,
            error = 3
        }
    }

    public class ListClass
    {
        public int i = 0;
        public string str = "测试XML序列化";
        private double[] array = new double[5];
        public List<float> list = new List<float>();
        public LogLevel _level = LogLevel.debug;

        public enum LogLevel
        {
            debug = 0,
            info = 1,
            warning = 2,
            error = 3
        }

        public ListClass()
        {
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
        }
    }
}
