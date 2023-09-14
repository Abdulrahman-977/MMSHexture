using Core.Interfaces.IExternalServices;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace WebApplication5.App_Start
{
    public class UnityConfig
    {
        public static void Initialize()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {


            container.RegisterType<IBoardRepository, BoardRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();

            container.RegisterType<IIdentityRepository, IdentityRepository>();
            container.RegisterType<IMeetingAttendeeRepository, MeetingAttendeeRepository>();
            container.RegisterType<IMeetingRepository, MeetingRepository>();

            container.RegisterType<IRestOperation, RestOperation>();
            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<IMeetingAgendaRepository, MeetingAgendaRepository>();
            container.RegisterType<IMeetingAgendaSpeakerRepository, MeetingAgendaSpeakerRepository>();
            container.RegisterType<IMeetingAgendaPollOptionRepository, MeetingAgendaPollOptionRepository>();
            container.RegisterType<IMeetingAgendaPollRepository, MeetingAgendaPollRepository>();
            container.RegisterType<ITaskRepository, TaskRepository>();
            container.RegisterType<IDecisionRepository, DecisionRepository>();
            container.RegisterType<IMeetingPollVottingRepository, MeetingPollVottingRepository>();
            container.RegisterType<IAmenityRepository, AmenityRepository>();
            container.RegisterType<INotificationRepository, NotificationRepository>();
        }
    }
}