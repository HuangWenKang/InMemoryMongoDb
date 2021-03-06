﻿using System;
using System.Collections.Generic;
using System.Text;
using TinyIoC;
using System.Linq;
namespace InMemoryMongoDb
{
    public interface ITinyIoCInstaller
    {
        void Install(TinyIoCContainer container);
    }
    internal static class TinyIoCExtensions
    {
        public static void RunInstallers(this TinyIoCContainer container)
        {
            try
            {
                var installers = from t in typeof(ITinyIoCInstaller).Assembly.GetTypes()
                                 where typeof(ITinyIoCInstaller) != t && typeof(ITinyIoCInstaller).IsAssignableFrom(t)
                                 select t;

                foreach (var type in installers)
                {
                    var installer = (ITinyIoCInstaller)Activator.CreateInstance(type);
                    installer.Install(container);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static bool IsBaseType<T>(Type baseType)
        {
            if (baseType == typeof(object))
                return false;

            if (baseType == typeof(T))
                return true;

            return IsBaseType<T>(baseType.BaseType);
        }
    }
}
