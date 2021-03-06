﻿using System;
using System.Collections.Generic;
using System.Text;
using TinyIoC;

namespace InMemoryMongoDb.Comparers
{
    internal class IocInstallers : ITinyIoCInstaller
    {
        public void Install(TinyIoCContainer container)
        {
            container.Register<IFilterComparers, FilterComparers>();
        }
    }
}
