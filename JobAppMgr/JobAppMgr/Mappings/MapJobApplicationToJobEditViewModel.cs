using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Core.Model;
using JobAppMgr.ViewModels;

namespace JobAppMgr.Mappings
{
    public class MapJobApplicationToJobEditViewModel : BaseMapper<JobApplication, JobEditViewModel>
    { }
}