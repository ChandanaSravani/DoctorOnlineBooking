using DoctorOnlineBooking.Controllers;
using DoctorOnlineBooking.Interfaces;
using DoctorOnlineBooking.Repositories;

using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace DoctorOnlineBooking
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IHomeInterface, HomeRepository>();
            container.RegisterType<IAdminInterface, AdminRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}