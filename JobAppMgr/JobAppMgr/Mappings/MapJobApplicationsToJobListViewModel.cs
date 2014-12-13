using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

using Core.Model; //  Survey model

using JobAppMgr.ViewModels;

namespace JobAppMgr.Mappings
{
    /// <summary>
    /// This class maps a Survey to a SurveyAnalysisViewModel 
    /// </summary>
    public class MapJobApplicationsToJobListViewModel : BaseMapper<JobApplication, JobListViewModel>
    { }
}