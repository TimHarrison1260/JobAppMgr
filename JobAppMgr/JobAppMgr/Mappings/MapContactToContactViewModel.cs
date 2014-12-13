using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;
using JobAppMgr.ViewModels;
using AutoMapper;

namespace JobAppMgr.Mappings
{
    public class MapContactToContactViewModel : BaseMapper<Contact, ContactViewModel>
    { }
}