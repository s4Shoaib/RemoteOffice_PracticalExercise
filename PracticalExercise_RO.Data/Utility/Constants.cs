using System;
using System.Collections.Generic;

namespace PracticalExercise_RO.Data.Utility
{
    public class Constants
    {
        public const string PractitionerKey = "Practitioner";
        public const string AppointmentKey = "Appointment";
    }

    public class Cache
    {
        public static readonly DateTimeOffset CacheExpirationOffset = DateTimeOffset.Now.AddHours(24);
    }

    public class UserPolicy
    {
        public const string AdminUser = "AdminUser";
        public const string AdminUserGroup = "GENMILLS\\CEREBRI_SITE_ADMINS";
        public const string SiteUser = "SiteUser";
        public const string SiteUserGroup = "GENMILLS\\CEREBRI_SITE_USERS";
    }
}
