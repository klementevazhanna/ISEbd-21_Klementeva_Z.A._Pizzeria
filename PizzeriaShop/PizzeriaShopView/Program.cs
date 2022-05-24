using PizzeriaDatabaseImplement.Implements;
using PizzeriaShopBusinessLogic.BusinessLogics;
using PizzeriaShopBusinessLogic.MailWorker;
using PizzeriaShopBusinessLogic.MailWorker.Implements;
using PizzeriaShopBusinessLogic.OfficePackage;
using PizzeriaShopBusinessLogic.OfficePackage.Implements;
using PizzeriaShopContracts.Attributes;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.StoragesContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace PizzeriaShopView
{
    static class Program
    {
        private static IUnityContainer container = null;

        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }

        [STAThread]
        static void Main()
        {
            var mailSender = Container.Resolve<AbstractMailWorker>();
            mailSender.MailConfig(new MailConfigBindingModel
            {
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                PopHost = ConfigurationManager.AppSettings["PopHost"],
                PopPort = Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"])
            });

            var timer = new System.Threading.Timer(new TimerCallback(MailCheck), null, 0, 100000);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IIngredientStorage, IngredientStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPizzaStorage, PizzasStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWareHouseStorage, WareHouseStorage>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<IClientStorage, ClientStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerStorage, ImplementerStorage>(new 
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMessageInfoStorage, MessageInfoStorage>(new 
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IBackUpInfo, BackUpInfo>(new 
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientLogic, IngredientLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPizzaLogic, PizzaLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportLogic, ReportLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerLogic, ImplementerLogic>(new 
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMessageInfoLogic, MessageInfoLogic>(new 
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkProcess, WorkModeling>(new 
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWareHouseLogic, WareHouseLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractMailWorker, MailKitWorker>(new 
            SingletonLifetimeManager());
            currentContainer.RegisterType<IBackUpLogic, BackUpLogic>(new 
            HierarchicalLifetimeManager());
            return currentContainer;
        }

        private static void MailCheck(object obj) => Container.Resolve<AbstractMailWorker>().MailCheck();

        public static void ConfigGrid<T>(List<T> data, DataGridView grid)
        {
            var type = typeof(T);
            var config = new List<string>();
            grid.Columns.Clear();
            foreach (var prop in type.GetProperties())
            {
                var attributes = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    foreach (var attr in attributes)
                    {
                        if (attr is ColumnAttribute columnAttr)
                        {
                            config.Add(prop.Name);
                            var column = new DataGridViewTextBoxColumn
                            {
                                Name = prop.Name,
                                ReadOnly = true,
                                HeaderText = columnAttr.Title,
                                Visible = columnAttr.Visible,
                                Width = columnAttr.Width
                            };
                            if (columnAttr.Alignment != DataGridViewContentAlignment.NotSet)
                            {
                                column.DefaultCellStyle.Alignment = columnAttr.Alignment;
                            }
                            if (columnAttr.GridViewAutoSize !=
                            GridViewAutoSize.None)
                            {
                                column.AutoSizeMode = (DataGridViewAutoSizeColumnMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnMode), columnAttr.GridViewAutoSize.ToString());
                            }
                            column.DefaultCellStyle.Format = columnAttr.DateType;
                            grid.Columns.Add(column);
                        }
                    }
                }
            }
            foreach (var elem in data)
            {
                var objs = new List<object>();
                foreach (var conf in config)
                {
                    var value = elem.GetType().GetProperty(conf).GetValue(elem);
                    objs.Add(value);
                }
                grid.Rows.Add(objs.ToArray());
            }
        }
    }
}
