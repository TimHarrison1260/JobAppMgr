﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace JobAppMgr
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mappings.AutoMapperConfiguration.Configure();
        }
    }
}