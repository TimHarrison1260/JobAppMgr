using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;
using JobAppMgr.ViewModels;

namespace JobAppMgr.Mappings
{
    public class MapJobViewModelToJobApplication : BaseMapper<JobEditViewModel, JobApplication>
    {
        //  Override collection version as it's not needed.
        public override IEnumerable<JobApplication> Map(IEnumerable<JobEditViewModel> obj)
        {
            throw new  NotImplementedException();
        }
    }
}