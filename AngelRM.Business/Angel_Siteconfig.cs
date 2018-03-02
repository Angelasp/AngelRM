using System;
using System.Collections.Generic;
using System.Text;
using AngelRM.Model;
using AngelRM.Core;

namespace AngelRM.Business
{
   public partial class Angel_Siteconfig
    {
        private static object lockcommand = new object();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.Angel_Siteconfig loadConfig()
        {
            Model.Angel_Siteconfig model = AngelCacheHelper.Get<Model.Angel_Siteconfig>(AngelConst.ANGEL_CACHE_SITECONFIG);
            if (model == null)
            {
                AngelCacheHelper.Insert(AngelConst.ANGEL_CACHE_SITECONFIG, loadConfig(AngelUtils.GetXmlMapPath(AngelConst.ANGELXML_FILE_SITECONFING)),
                    AngelUtils.GetXmlMapPath(AngelConst.ANGELXML_FILE_SITECONFING));
                model = AngelCacheHelper.Get<Model.Angel_Siteconfig>(AngelConst.ANGEL_CACHE_SITECONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.Angel_Siteconfig saveConifg(AngelRM.Model.Angel_Siteconfig model)
        {
            return saveConifg(model, AngelUtils.GetXmlMapPath(AngelConst.ANGELXML_FILE_SITECONFING));
        }

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.Angel_Siteconfig loadConfig(string configFilePath)
        {
           return (Model.Angel_Siteconfig)AngelSerialization.Load(typeof(Model.Angel_Siteconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.Angel_Siteconfig saveConifg(Model.Angel_Siteconfig model, string configFilePath)
        {
            lock (lockcommand)
            {
                AngelSerialization.Save(model, configFilePath);
            }
            return model;
        }
    }
}
