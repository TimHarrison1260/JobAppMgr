using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;
using JobAppMgr.ViewModels;

namespace JobAppMgr.Mappings
{
    public class MapJobApplicationToJobDetailsViewModel : BaseMapper<JobApplication, JobDetailsViewModel>
    { }
}